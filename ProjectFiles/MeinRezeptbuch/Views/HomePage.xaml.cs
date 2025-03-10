using MeinRezeptbuch.ViewModels;

namespace MeinRezeptbuch.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage(HomeViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
