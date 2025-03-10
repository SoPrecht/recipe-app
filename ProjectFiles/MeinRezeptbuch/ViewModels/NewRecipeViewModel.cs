using CommunityToolkit.Maui.Views;
using MeinRezeptbuch.Models;
using MeinRezeptbuch.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinRezeptbuch.ViewModels
{
    public partial class NewRecipeViewModel : ObservableObject
    {
        [ObservableProperty]
        public string? name = String.Empty;
        [ObservableProperty]
        public string? description = String.Empty;
        [ObservableProperty]
        public string instructions = String.Empty;

        public List<IngredientEntry> ingredients = new List<IngredientEntry>();

        [RelayCommand]
        public async Task SaveRecipe()
        {
            // Logic to save the recipe
            //await Application.Current.MainPage.DisplayAlert("Save", "Recipe saved successfully!", "OK");
        }

        [RelayCommand]
        public async Task AddIngredient()
        {
            var popup = new AddIngredientEntryPopUpPage(this);
            var entry = await AppShell.Current.ShowPopupAsync(popup);
            if (entry != null)
            {
                ingredients.Add((IngredientEntry)entry);
            }

        }

        [RelayCommand]
        public async Task Cancel_Button_Clicked()
        {
            await AppShell.Current.GoToAsync(nameof(RecipesPage));
        }

    }
}
