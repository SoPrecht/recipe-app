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

            if (recipeId.HasValue && recipeId.Value > 0)
            {
                // Load existing recipe
                LoadRecipe(recipeId.Value);
                _recipeId = recipeId.Value;
            }
            else
            {
                // Create a new recipe and store the generated ID
                var newRecipe = new Recipe { Name = "New Recipe" };
                _recipeId = _recipeService.AddRecipeAsync(newRecipe).Result; // Get the ID immediately

                Name = newRecipe.Name;
                Description = newRecipe.Description;
                Instructions = newRecipe.Instructions;
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("recipeId") && query["recipeId"] is int id)
            {
                _recipeId = id;

                if (_recipeId > 0)
                {
                    isExisting = true;
                    LoadRecipe(_recipeId);
                }
                else
                {
                    isExisting = false;
                    CreateNewRecipe();
                }
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
                Name = recipe.Name;
                Description = recipe.Description;
                Instructions = recipe.Instructions;

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
            var newRecipe = new Recipe { Name = "New Recipe" };
            await _recipeService.AddRecipeAsync(newRecipe);

            _recipeId = newRecipe.Id;  // Store the generated ID
            Name = newRecipe.Name;
            Description = newRecipe.Description;
            Instructions = newRecipe.Instructions;
        }

        [RelayCommand]
        public async Task SaveRecipe()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Instructions))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            var updatedRecipe = new Recipe
            {
                Id = _recipeId,
                Name = this.Name,
                Description = this.Description,
                Instructions = this.Instructions
            };

            await _recipeService.UpdateRecipeAsync(updatedRecipe);

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
                await _recipeService.DeleteRecipeAsync(_recipeId); // Delete unsaved recipe
            }
            await AppShell.Current.GoToAsync(nameof(RecipesPage));
        }
    }
}
