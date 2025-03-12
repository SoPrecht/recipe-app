using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MeinRezeptbuch.Services;
using MeinRezeptbuch.Views;
using MeinRezeptbuch.ViewModels;


namespace MeinRezeptbuch
{
    public static class MauiProgram
    {
        private static IServiceProvider _services;
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
            builder.Services.AddTransient<PopupIngredientPage>();

            builder.Services.AddTransient<AddIngredientEntryPopUpPage>();
            builder.Services.AddTransient<IngredientEntryViewModel>();
            //builder.Services.AddSingleton<AddIngredientEntryPopUpPage>();
            //builder.Services.AddSingleton<AddIngredientEntryPopUpPage>();

            // Register Services as Singleton
            builder.Services.AddSingleton<RecipeService>();
            builder.Services.AddSingleton<IngredientService>();
            builder.Services.AddSingleton<IngredientEntryService>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            _services = app.Services;

            return app;
        }

        // Method to get services globally
        public static T GetService<T>() where T : class
        {
            if (_services == null)
            {
                throw new InvalidOperationException("Dependency Injection container is not initialized.");
            }

            var service = _services.GetService<T>();

            if (service == null)
            {
                throw new InvalidOperationException($"Service {typeof(T).Name} is not registered in the DI container.");
            }

            return service;
        }
    }
}
