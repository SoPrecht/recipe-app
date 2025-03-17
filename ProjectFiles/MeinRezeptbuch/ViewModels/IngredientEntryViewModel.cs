using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeinRezeptbuch.Services;
using MeinRezeptbuch.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using System.Linq;

namespace MeinRezeptbuch.ViewModels
{
    public partial class IngredientEntryViewModel : ObservableObject
    {
        private readonly IngredientService _ingredientService;
        private readonly IngredientEntryService _ingredientEntryService;
        private Popup? _popup;

        public ObservableCollection<IngredientEntry> IngredientEntries { get; } = new();
        public ObservableCollection<IngredientType> IngredientTypes { get; } = new(Enum.GetValues<IngredientType>());
        public ObservableCollection<UnitEnum> Units { get; } = new(Enum.GetValues<UnitEnum>());
        public ObservableCollection<string> IngredientSuggestions { get; } = new();

        [ObservableProperty]
        public string ingredientName;

        [ObservableProperty]
        public IngredientType selectedIngredientType;

        [ObservableProperty]
        public UnitEnum unit;

        [ObservableProperty]
        public int amount;

        [ObservableProperty]
        public string? notes;

        private int _recipeId;

        public IngredientEntryViewModel(IngredientEntryService ingredientEntryService, IngredientService ingredientService, int? recipeId = null)
        {
            _ingredientService = ingredientService;
            _ingredientEntryService = ingredientEntryService;
            _recipeId = recipeId ?? 0;
            LoadIngredientEntries();
        }

        public void SetPopupReference(Popup popup)
        {
            _popup = popup;
        }

        private async void LoadIngredientEntries()
        {
            Debug.WriteLine("LoadIngredientEntries is being called!!!!!");
            IngredientEntries.Clear();

            var ingredientEntries = await _ingredientEntryService.GetIngredientEntriesByRecipeIdAsync(_recipeId);

            foreach (var entry in ingredientEntries)
            {
                Debug.WriteLine($"Processing entry with ID: {entry.Id}, IngredientId: {entry.IngredientId}");

                var ingredient = await _ingredientService.GetIngredientByIdAsync(entry.IngredientId);
                if (ingredient != null)
                {
                    entry.IngredientName = ingredient.Name;
                }

                IngredientEntries.Add(entry);
            }
        }

        [RelayCommand]
        public async Task SearchIngredientSuggestionsAsync()
        {
            if (string.IsNullOrWhiteSpace(IngredientName))
            {
                IngredientSuggestions.Clear();
                return;
            }

            var ingredients = await _ingredientService.GetIngredientsByPartialNameAsync(IngredientName);
            IngredientSuggestions.Clear();
            foreach (var ingredient in ingredients)
            {
                IngredientSuggestions.Add(ingredient.Name);
            }
            Debug.WriteLine($"IngredientSuggestions now contains {IngredientSuggestions.Count} items.");
        }

        [RelayCommand]
        public async Task SelectIngredientAsync(string selectedIngredientName)
        {
            var ingredient = await _ingredientService.GetIngredientByNameAsync(selectedIngredientName);
            if (ingredient != null)
            {
                IngredientName = ingredient.Name;
                SelectedIngredientType = ingredient.Type;
            }
        }

        [RelayCommand]
        public async Task AddIngredientEntryAsync()
        {
            // Check if ingredient exists
            var ingredient = await _ingredientService.GetIngredientByNameAsync(IngredientName);
            if (ingredient == null)
            {
                // Create new ingredient
                ingredient = new Ingredient(IngredientName, SelectedIngredientType);
                await _ingredientService.AddIngredientAsync(ingredient);
            }

            // Create new ingredient entry
            var ingredientEntry = new IngredientEntry(_recipeId, ingredient.ID, ingredient.Name, Unit, Amount, Notes);
            await _ingredientEntryService.AddIngredientEntryAsync(ingredientEntry);

            MainThread.BeginInvokeOnMainThread(() => {
                IngredientEntries.Add(ingredientEntry);
            });

            ClosePopup();
        }

        [RelayCommand]
        public async Task DeleteIngredientEntryAsync(IngredientEntry ingredientEntry)
        {
            await _ingredientEntryService.DeleteIngredientEntryAsync(ingredientEntry);

            MainThread.BeginInvokeOnMainThread(() => {
                IngredientEntries.Remove(ingredientEntry);
            });
        }

        [RelayCommand]
        public void ClosePopup()
        {
            IngredientName = string.Empty;
            SelectedIngredientType = default;
            Unit = default;
            Amount = 0;
            Notes = string.Empty;
            IngredientSuggestions.Clear();

            _popup?.Close();
        }

        public void SetRecipeId(int recipeId)
        {
            _recipeId = recipeId;
        }
    }
}
