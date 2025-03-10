using System;
using System.Globalization;
using System.Resources;
using Microsoft.Extensions.Logging;

public static class LocalizationManager
{
    private static readonly ResourceManager _resourceManager =
        new ResourceManager("MeinRezeptBuch.Resources.AppResources", typeof(LocalizationManager).Assembly);

    private static readonly object _cultureLock = new object();

    private static readonly string DefaultCulture = "en";

    public static string GetString(string key)
    {
        try
        {
            string? value = _resourceManager.GetString(key, CultureInfo.CurrentCulture);
            if (string.IsNullOrEmpty(value))
            {
                // Optional: Log missing keys for debugging
                Console.WriteLine($"[LocalizationManager] Missing localization for key: '{key}'");
                return key; // Return the key as fallback
            }
            return value!; // Ensure non-null by handling empty/null earlier
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LocalizationManager] Error retrieving key '{key}': {ex.Message}");
            return key; // Return the key as fallback
        }
    }

    public static void ChangeCulture(string cultureCode)
    {
        try
        {
            var culture = new CultureInfo(cultureCode);
            lock (_cultureLock)
            {
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
        }
        catch (CultureNotFoundException ex)
        {
            Console.WriteLine($"[LocalizationManager] Invalid culture code '{cultureCode}': {ex.Message}");

            // Fallback to default culture
            SetDefaultCulture();
        }
    }

    private static void SetDefaultCulture()
    {
        lock (_cultureLock)
        {
            var culture = new CultureInfo(DefaultCulture);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            Console.WriteLine($"[LocalizationManager] Default culture '{DefaultCulture}' has been set.");
        }
    }

    public static bool IsCultureSupported(string cultureCode)
    {
        try
        {
            var culture = new CultureInfo(cultureCode);
            return true;
        }
        catch (CultureNotFoundException)
        {
            return false;
        }
    }
}