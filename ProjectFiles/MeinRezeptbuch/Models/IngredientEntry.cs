using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeinRezeptbuch.Services;

namespace MeinRezeptbuch.Models
{
    public class IngredientEntry
    {
        public Ingredient ingredient;
        public UnitEnum unit;
        public int amount;
        public string? notes;


        public IngredientEntry(Ingredient ing, UnitEnum u, int a, string? n = null)
        {
            ingredient = ing;
            unit = u;
            amount = a;
            notes = n;
        }
        public IngredientEntry(string name, UnitEnum u, int a, string? n = null)
        {
            ingredient = new Ingredient(name, IngredientType.Undefined);
            unit = u;
            amount = a;
            notes = n;
        }
    }

}
