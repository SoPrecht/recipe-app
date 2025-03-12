using CommunityToolkit.Maui.Views;
using MeinRezeptbuch.ViewModels;

namespace MeinRezeptbuch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupIngredientPage : Popup
    {
        public PopupIngredientPage(IngredientViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            //vm.SetPopupReference(this);
        }
    }
}
