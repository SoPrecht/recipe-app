using SQLite;
using MeinRezeptbuch.Services;

namespace MeinRezeptbuch.Models
{
    public class IngredientEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public int RecipeId { get; set; } // Foreign key reference to Recipe

        public int IngredientId { get; set; } // Foreign key reference to Ingredient
        public UnitEnum Unit { get; set; }
        public int Amount { get; set; }
        public string? Notes { get; set; }

        public string IngredientName { get; set; } // Populated in ViewModel


        public IngredientEntry() { }

        public IngredientEntry(int recipeId, int ingredientId, string ingredientName, UnitEnum unit, int amount, string? notes = null)
        {
            RecipeId = recipeId;
            IngredientId = ingredientId;
            IngredientName = ingredientName;
            Unit = unit;
            Amount = amount;
            Notes = notes;
        }
    }
}
