using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MeinRezeptbuch.Models;
using MeinRezeptbuch.Services;
using MeinRezeptbuch.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private bool _isQueryApplied = false;

        public NewRecipeViewModel(RecipeService recipeService, IngredientEntryService ingredientEntryService, int? recipeId = null)
        {
            _recipeService = recipeService;
            _ingredientEntryService = ingredientEntryService;
            ingredients = new ObservableCollection<IngredientEntry>();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            //safety check to prevent multiple calls
            if (_isQueryApplied) return;
            _isQueryApplied = true;

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
                Ingredients.Clear();
                Ingredients = new ObservableCollection<IngredientEntry>(_recipe.Ingredients);
            }
        }

        // ex async
        private async void CreateNewRecipe()
        {
            _recipe = new Recipe { Name = "New Recipe" };
            await _recipeService.AddRecipeAsync(_recipe);
            _recipeId = _recipe.Id;
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

            WeakReferenceMessenger.Default.Send(new RecipeAddedMessage());
            await Application.Current.MainPage.DisplayAlert("Success", "Recipe saved successfully!", "OK");
            await AppShell.Current.GoToAsync(nameof(RecipesPage));
        }

        [RelayCommand]
        public async Task AddIngredient()
        {
            var popup = MauiProgram.GetService<AddIngredientEntryPopUpPage>();
            if (popup != null)
            {
                var viewModel = popup.BindingContext as IngredientEntryViewModel;
                if (viewModel != null)
                {
                    viewModel.SetRecipeId(_recipeId);
                }
                var entry = await AppShell.Current.ShowPopupAsync(popup);
                if (entry != null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Ingredients.Add((IngredientEntry)entry); // Ensure UI updates immediately
                    });
                }
            }
        }

        [RelayCommand]
        public async Task Cancel_Button_Clicked()
        {
            if (!isExisting)
            {
                await _recipeService.DeleteRecipeAsync(_recipe);
            }
            await AppShell.Current.GoToAsync(nameof(RecipesPage));
        }
    }
}
