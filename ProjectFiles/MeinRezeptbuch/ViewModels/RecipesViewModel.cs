using MeinRezeptbuch.Views;
using MeinRezeptbuch.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
            Task.Run(async () => await RefreshRecipesAsync());

            // 🔹 Listen for new recipes being added
            WeakReferenceMessenger.Default.Register<RecipeAddedMessage>(this, async (r, m) =>
            {
                await RefreshRecipesAsync();
            });
        }

        /// <summary>
        /// Loads recipes from the database asynchronously and updates the observable collection
        /// </summary>
        public async Task RefreshRecipesAsync()
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
                var navigationParameter = new Dictionary<string, object>
                    {
                        { "recipeId", recipeId }
                    };
                await Shell.Current.GoToAsync(nameof(NewRecipePage), navigationParameter);
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(NewRecipePage));
            }
        }
    }
}
