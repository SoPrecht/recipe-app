using System;


namespace MeinRezeptbuch.Services
{
    public static class EnumHelper
    {
        public static Array IngredientTypes => Enum.GetValues(typeof(IngredientType));
        public static Array UnitEnums => Enum.GetValues(typeof(UnitEnum));
    }
}
