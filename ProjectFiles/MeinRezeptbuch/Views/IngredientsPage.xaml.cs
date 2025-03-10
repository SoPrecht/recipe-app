using MeinRezeptbuch.ViewModels;

namespace MeinRezeptbuch.Views;

public partial class IngredientsPage : ContentPage
{
	public IngredientsPage(IngredientViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}