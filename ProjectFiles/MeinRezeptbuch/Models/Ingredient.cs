using MeinRezeptbuch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinRezeptbuch.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public IngredientType Type { get; set; }
        public Ingredient(string name, IngredientType type = IngredientType.Undefined, string? description = null) 
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
