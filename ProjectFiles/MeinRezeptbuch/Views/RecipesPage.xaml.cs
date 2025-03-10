using MeinRezeptbuch.ViewModels;

namespace MeinRezeptbuch.Views;

public partial class RecipesPage : ContentPage
{
	public RecipesPage(RecipesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}