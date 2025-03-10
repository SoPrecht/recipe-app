using System.Globalization;
using MeinRezeptbuch.Services;
using Microsoft.Maui.Controls;

namespace MeinRezeptbuch
{
    public partial class App : Application
    {
        public App(PreferencesService preferencesService)
        {
            // Initialize components for the application
            InitializeComponent();

            // Load preferred language or fallback to system default
            var language = preferencesService.GetPreferredLanguageOrDefault();

            // Set the culture using LocalizationManager
            LocalizationManager.ChangeCulture(language);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}