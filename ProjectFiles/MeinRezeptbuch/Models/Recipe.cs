using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinRezeptbuch.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }

        public int portions { get; set; }

        public List<IngredientEntry> ingredients { get; set; }
       
    }
}
