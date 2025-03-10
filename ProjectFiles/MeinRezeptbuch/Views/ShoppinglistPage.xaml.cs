using MeinRezeptbuch.ViewModels;

namespace MeinRezeptbuch.Views;

public partial class ShoppinglistPage : ContentPage
{
	public ShoppinglistPage(ShoppingListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}