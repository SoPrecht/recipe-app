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
    private readonly NewRecipeViewModel vm;
	public AddIngredientEntryPopUpPage(NewRecipeViewModel vm)
	{
		InitializeComponent();
        this.vm = vm;

	}

    private void Save_Button_Clicked(object sender, EventArgs e)
    {
        IngredientEntry entry = new IngredientEntry(
            ingredientNameEntry.Text,
            (UnitEnum)unitPicker.SelectedItem,
            ingredientTypePicker.SelectedIndex,
            amountEntry.Text
            );

        Close(entry);
    }

    private void Cancel_Button_Clicked(object sender, EventArgs e)
    {
        Close();
    }

}