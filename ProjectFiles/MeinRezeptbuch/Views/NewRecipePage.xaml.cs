using MeinRezeptbuch.ViewModels;

namespace MeinRezeptbuch.Views;

public partial class NewRecipePage : ContentPage
{
	public NewRecipePage(NewRecipeViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}