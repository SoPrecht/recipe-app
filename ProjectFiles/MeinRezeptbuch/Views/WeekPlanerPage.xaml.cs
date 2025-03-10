using MeinRezeptbuch.ViewModels;

namespace MeinRezeptbuch.Views;

public partial class WeekPlanerPage : ContentPage
{
	public WeekPlanerPage(WeekPlanerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}