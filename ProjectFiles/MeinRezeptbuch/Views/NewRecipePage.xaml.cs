using MeinRezeptbuch.ViewModels;

namespace MeinRezeptbuch.Views;

public partial class NewRecipePage : ContentPage, IQueryAttributable
{

    private readonly NewRecipeViewModel _viewModel;
    public NewRecipePage(NewRecipeViewModel vm)
	{
        InitializeComponent();
        _viewModel = vm;
        BindingContext = vm;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _viewModel.ApplyQueryAttributes(query);
    }
}