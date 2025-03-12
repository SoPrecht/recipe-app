using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeinRezeptbuch.Models;
using MeinRezeptbuch.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using MeinRezeptbuch.Views;

namespace MeinRezeptbuch.ViewModels
{
    public partial class IngredientViewModel : ObservableObject
    {
        private readonly IngredientService _ingredientService;
        private Popup? _popup;

        [ObservableProperty]
        private ObservableCollection<Ingredient> ingredients;

        [ObservableProperty]
        private string ingredientName;

        [ObservableProperty]
        private IngredientType selectedIngredientType;

        public ObservableCollection<IngredientType> IngredientTypes { get; } = new(Enum.GetValues<IngredientType>());

        public IngredientViewModel(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
            ingredients = new ObservableCollection<Ingredient>();
            LoadIngredients();
        }

        public void SetPopupReference(Popup popup)
        {
            _popup = popup;
        }

        private async void LoadIngredients()
        {
            try
            {
                var ingredientList = await _ingredientService.GetAllIngredientsAsync();
                Ingredients.Clear();
                Ingredients = new ObservableCollection<Ingredient>(ingredientList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading ingredients: {ex.Message}");
            }
        }

        [RelayCommand]
        public void OpenIngredientPopup()
        {
            _popup = MauiProgram.GetService<PopupIngredientPage>();
            if (_popup != null)
            {
                AppShell.Current.ShowPopup(_popup);
            }
            
        }

        [RelayCommand]
        public async Task SaveIngredient()
        {
            if (!string.IsNullOrWhiteSpace(IngredientName))
            {
                var existingIngredient = Ingredients.FirstOrDefault(i => i.Name == IngredientName);

                if (existingIngredient != null)
                {
                    // Edit existing ingredient
                    existingIngredient.Name = IngredientName;
                    existingIngredient.Type = SelectedIngredientType;
                    await _ingredientService.UpdateIngredientAsync(existingIngredient);
                }
                else
                {
                    // Add new ingredient
                    var ingredient = new Ingredient(IngredientName, SelectedIngredientType);
                    await _ingredientService.AddIngredientAsync(ingredient);
                    Ingredients.Add(ingredient);
                }

                ClosePopup();
            }
        }


        [RelayCommand]
        public async Task DeleteIngredient(Ingredient ingredient)
        {
            if (ingredient != null)
            {
                await _ingredientService.DeleteIngredientAsync(ingredient);
                Ingredients.Remove(ingredient);
            }
        }


        [RelayCommand]
        public void ClosePopup()
        {
            IngredientName = string.Empty;
            SelectedIngredientType = default;
            _popup?.Close();
        }
    }
}
