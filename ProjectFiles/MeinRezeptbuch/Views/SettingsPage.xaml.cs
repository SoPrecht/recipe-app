using MeinRezeptbuch.ViewModels;

namespace MeinRezeptbuch.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}