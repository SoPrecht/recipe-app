using MeinRezeptbuch.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinRezeptbuch.ViewModels
{
    public partial class RecipesViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<String> recipes;

        public RecipesViewModel() 
        {
            recipes = new List<String>();
            recipes.Add("Pancake");
            recipes.Add("Potato");
            recipes.Add("gure");
            recipes.Add("qwert");
            recipes.Add("cvbnmnmb");
            recipes.Add("kjsefbdffdf42234");


        }

        [RelayCommand]
        public async Task OpenNewRecipe()
        {
            await Shell.Current.GoToAsync(nameof(NewRecipePage));
        }
    }
}
