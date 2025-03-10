using Microsoft.Maui.Storage;
using System.Globalization;

namespace MeinRezeptbuch.Services
{
    public class PreferencesService
    {
        private const string LanguageKey = "AppLanguage";

        public string GetPreferredLanguageOrDefault()
        {
            // Retrieve the saved language or fallback to system's language
            return Preferences.Get(LanguageKey, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
        }

        public void SavePreferredLanguage(string languageCode)
        {
            Preferences.Set(LanguageKey, languageCode);
        }
    }
}
