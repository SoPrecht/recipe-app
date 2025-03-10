using MeinRezeptbuch.Views;
using MeinRezeptbuch.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeinRezeptbuch.Models;

namespace MeinRezeptbuch.ViewModels
{
    public partial class RecipesViewModel : ObservableObject
    {
        private readonly RecipeService _recipeService;
        [ObservableProperty]
        private ObservableCollection<Recipe> recipes;
        public RecipesViewModel()
        {
            _recipeService = new RecipeService();
            recipes = new ObservableCollection<Recipe>();
            LoadRecipes();
        }

        /// <summary>
        /// Loads recipes from the database asynchronously and updates the observable collection
        /// </summary>
        private async void LoadRecipes()
        {
            var recipeList = await _recipeService.GetAllRecipesAsync();
            Recipes.Clear();
            foreach (var recipe in recipeList)
            {
                Recipes.Add(recipe);
            }
        }

        /// <summary>
        /// Command to open the NewRecipePage, optionally passing an existing recipe ID for editing
        /// </summary>
        [RelayCommand]
        public async Task OpenNewRecipeAsnyc(int? recipeId = null)
        {
            if (recipeId.HasValue)
            {
                await Shell.Current.GoToAsync($"{nameof(NewRecipePage)}?recipeId={recipeId.Value}");
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(NewRecipePage));
            }
        }
    }
}
