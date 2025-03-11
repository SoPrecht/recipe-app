using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MeinRezeptbuch.Models;
using MeinRezeptbuch.Services;
using MeinRezeptbuch.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MeinRezeptbuch.ViewModels
{
    public partial class NewRecipeViewModel : ObservableObject, IQueryAttributable
    {
        private readonly RecipeService _recipeService;
        private readonly IngredientEntryService _ingredientEntryService;
        private int _recipeId;
        private bool isExisting;
        private Recipe _recipe;

        [ObservableProperty]
        public string? name;
        [ObservableProperty]
        public string? description;
        [ObservableProperty]
        public string instructions;
        [ObservableProperty]
        public ObservableCollection<IngredientEntry> ingredients;

        public NewRecipeViewModel(int? recipeId = null)
        {
            _recipeService = new RecipeService();
            _ingredientEntryService = new IngredientEntryService();
            ingredients = new ObservableCollection<IngredientEntry>();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("recipeId") && query["recipeId"] is int id)
            {
                _recipeId = id;

                isExisting = true;
                LoadRecipe(_recipeId);

            }
            else
            {
                isExisting = false;
                CreateNewRecipe();
            }
            
        }

        /// <summary>
        /// Loads an existing recipe from the database.
        /// </summary>
        private async void LoadRecipe(int recipeId)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(recipeId);
            if (recipe != null)
            {
                _recipe = recipe;
                Name = _recipe.Name;
                Description = _recipe.Description;
                Instructions = _recipe.Instructions;

                // Load ingredient entries
                var ingredientEntries = await _ingredientEntryService.GetIngredientEntriesByRecipeIdAsync (recipeId);
                foreach (var entry in ingredientEntries)
                {
                    var ingredient = await _ingredientEntryService.GetIngredientByIdAsync(entry.IngredientId);
                    entry.IngredientName = ingredient?.Name ?? "Unknown";
                    Ingredients.Add(entry);
                }
            }
        }

        private async void CreateNewRecipe()
        {
            _recipe = new Recipe { Name = "New Recipe" };
            await _recipeService.AddRecipeAsync(_recipe); // 🔹 Save to DB first
            _recipeId = _recipe.Id;  // 🔹 Store the new ID

            // Update UI properties
            Name = _recipe.Name;
            Description = _recipe.Description;
            Instructions = _recipe.Instructions;
        }

        [RelayCommand]
        public async Task SaveRecipe()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Instructions))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            _recipe.Name = Name;
            _recipe.Description = Description;
            _recipe.Instructions = Instructions;

            await _recipeService.UpdateRecipeAsync(_recipe);

            foreach (var ingredient in Ingredients)
            {
                ingredient.RecipeId = _recipeId;
                await _ingredientEntryService.AddIngredientEntryAsync(ingredient);
            }

            // 🔹 Notify RecipesViewModel to refresh the list
            WeakReferenceMessenger.Default.Send(new RecipeAddedMessage());

            await Application.Current.MainPage.DisplayAlert("Success", "Recipe saved successfully!", "OK");
            await AppShell.Current.GoToAsync(nameof(RecipesPage));
        }

        [RelayCommand]
        public async Task AddIngredient()
        {
            var popup = new AddIngredientEntryPopUpPage(new IngredientEntryViewModel(_recipeId));
            var entry = await AppShell.Current.ShowPopupAsync(popup);
            if (entry != null)
            {
                Ingredients.Add((IngredientEntry)entry);
            }
        }

        [RelayCommand]
        public async Task Cancel_Button_Clicked()
        {
            if (!isExisting)
            {
                await _recipeService.DeleteRecipeAsync(_recipe); // Delete unsaved recipe
            }
            await AppShell.Current.GoToAsync(nameof(RecipesPage));
        }
    }
}
