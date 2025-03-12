using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using MeinRezeptbuch.Services;
using MeinRezeptbuch.Models;

namespace MeinRezeptbuch.Converters
{
    public class AmountUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IngredientEntry entry)
            {
                return $"{entry.Amount} {entry.Unit}";
            }
            return "Invalid Data";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null; // One-way binding only
        }
    }
}