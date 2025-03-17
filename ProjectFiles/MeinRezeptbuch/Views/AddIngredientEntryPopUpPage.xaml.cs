using CommunityToolkit.Maui.Views;
using MeinRezeptbuch.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using MeinRezeptbuch.Models;
using MeinRezeptbuch.Services;

namespace MeinRezeptbuch.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddIngredientEntryPopUpPage : Popup
{
    public AddIngredientEntryPopUpPage(IngredientEntryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        vm.SetPopupReference(this);
    }

    private void OnIngredientNameTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is IngredientEntryViewModel vm)
        {
            vm.SearchIngredientSuggestionsCommand.Execute(null);
        }
    }

}