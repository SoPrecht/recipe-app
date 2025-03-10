using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MeinRezeptbuch.Services;
using MeinRezeptbuch.Views;
using MeinRezeptbuch.ViewModels;


namespace MeinRezeptbuch
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register PreferencesService
            builder.Services.AddSingleton<PreferencesService>();
            builder.Services.AddSingleton<AppShell>();

            //Pages
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<HomeViewModel>();

            builder.Services.AddSingleton<RecipesPage>();
            builder.Services.AddSingleton<RecipesViewModel>();

            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<SettingsViewModel>();

            builder.Services.AddSingleton<ShoppinglistPage>();
            builder.Services.AddSingleton<ShoppingListViewModel>();

            builder.Services.AddSingleton<WeekPlanerPage>();
            builder.Services.AddSingleton<WeekPlanerViewModel>();

            builder.Services.AddTransient<NewRecipePage>();
            builder.Services.AddTransient<NewRecipeViewModel>();

            builder.Services.AddSingleton<IngredientsPage>();
            builder.Services.AddSingleton<IngredientViewModel>();

            builder.Services.AddSingleton<AddIngredientEntryPopUpPage>();
            builder.Services.AddTransient<IngredientEntryViewModel>();
            //builder.Services.AddSingleton<AddIngredientEntryPopUpPage>();
            //builder.Services.AddSingleton<AddIngredientEntryPopUpPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
