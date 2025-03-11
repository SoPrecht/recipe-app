using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeinRezeptbuch.Services;
using MeinRezeptbuch.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;


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

        public int RecipeId { get; private set; }


        public IngredientEntryViewModel(IngredientEntryService ingredientEntryService, IngredientService ingredientService,int? recipeId = null)
        {
            _ingredientService = ingredientService;
            _ingredientEntryService = ingredientEntryService;
            RecipeId = recipeId ?? 0;
            LoadIngredientEntries();
        }

        public void SetPopupReference(Popup popup)
        {
            _popup = popup;
        }

        private async void LoadIngredientEntries()
        {
            var entries = await _ingredientEntryService.GetAllIngredientEntriesAsync();
            IngredientEntries.Clear();
            foreach (var entry in entries)
            {
                IngredientEntries.Add(entry);
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
            var ingredientEntry = new IngredientEntry(RecipeId , ingredient.ID, ingredient.Name, Unit, Amount, Notes);
            await _ingredientEntryService.AddIngredientEntryAsync(ingredientEntry);
            LoadIngredientEntries();
        }

        [RelayCommand]
        public async Task DeleteIngredientEntryAsync(IngredientEntry ingredientEntry)
        {
            await _ingredientEntryService.DeleteIngredientEntryAsync(ingredientEntry);
            LoadIngredientEntries();
        }

        [RelayCommand]
        public void ClosePopup()
        {
            _popup.Close();
        }
    }
}
