using SQLite;
using System;
using System.Collections.Generic;

namespace MeinRezeptbuch.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }

        // List of ingredients (not stored in SQLite directly, handled via RecipeIngredient relationship)
        [Ignore]
        public List<IngredientEntry> Ingredients { get; set; } = new();

        public string Category { get; set; } // Optional: Example "Dessert", "Main Course"
        public int CookingTime { get; set; } // In minutes
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Parameterless constructor for SQLite
        public Recipe() { }

        public Recipe(string name, string description, string instructions, string category, int cookingTime)
        {
            Name = name;
            Description = description;
            Instructions = instructions;
            Category = category;
            CookingTime = cookingTime;
        }
    }
}
