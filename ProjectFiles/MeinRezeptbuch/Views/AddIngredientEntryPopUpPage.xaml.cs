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
}