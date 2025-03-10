using MeinRezeptbuch.Views;

namespace MeinRezeptbuch
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(RecipesPage), typeof(RecipesPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(ShoppinglistPage), typeof(ShoppinglistPage));
            Routing.RegisterRoute(nameof(WeekPlanerPage), typeof(WeekPlanerPage));

            Routing.RegisterRoute(nameof(NewRecipePage), typeof(NewRecipePage));
        }
    }
}
