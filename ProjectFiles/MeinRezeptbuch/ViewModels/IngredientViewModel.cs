using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeinRezeptbuch.Services;
using MeinRezeptbuch.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MeinRezeptbuch.ViewModels
{
    public partial class IngredientViewModel : ObservableObject
    {
        private readonly IngredientService _ingredientService;

        public ObservableCollection<Ingredient> Ingredients { get; } = new();

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private IngredientType ingtype;

        [ObservableProperty]
        private string description;

        public IngredientViewModel()
        {
            _ingredientService = new IngredientService();
            LoadIngredients();
        }

        private async void LoadIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            Ingredients.Clear();
            foreach (var ingredient in ingredients)
            {
                Ingredients.Add(ingredient);
            }
        }

        [RelayCommand]
        public async Task AddIngredientAsync()
        {
            var ingredient = new Ingredient(Name, Ingtype, Description);
            await _ingredientService.AddIngredientAsync(ingredient);
            LoadIngredients();
        }

        [RelayCommand]
        public async Task DeleteIngredientAsync(Ingredient ingredient)
        {
            await _ingredientService.DeleteIngredientAsync(ingredient);
            LoadIngredients();
        }
    }
}
