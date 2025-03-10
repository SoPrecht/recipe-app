using CommunityToolkit.Maui.Views;
using MeinRezeptbuch.Models;
using MeinRezeptbuch.Services;
using MeinRezeptbuch.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MeinRezeptbuch.ViewModels
{
    public partial class NewRecipeViewModel : ObservableObject
    {
        private readonly RecipeService _recipeService;
        private readonly IngredientEntryService _ingredientEntryService;
        private readonly int _recipeId;

        [ObservableProperty]
        public string? name;
        [ObservableProperty]
        public string? description;
        [ObservableProperty]
        public string instructions;
        [ObservableProperty]
        public ObservableCollection<IngredientEntry> ingredients;

        public NewRecipeViewModel()
        {
            _recipeService = new RecipeService();
            _ingredientEntryService = new IngredientEntryService();

            // Create a new recipe in the database immediately
            var newRecipe = new Recipe { Name = "New Recipe" };
            _recipeId = _recipeService.AddRecipeAsync(newRecipe).Result; // Ensure we get the ID immediately

            Name = newRecipe.Name;
            Description = newRecipe.Description;
            Instructions = newRecipe.Instructions;
            ingredients = new ObservableCollection<IngredientEntry>();
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
            await _recipeService.DeleteRecipeAsync(_recipeId); // Delete unsaved recipe
            await AppShell.Current.GoToAsync(nameof(RecipesPage));
        }
    }
}
