using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MeinRezeptbuch.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        // Observable property for RecipeItems
        [ObservableProperty]
        protected RecipeItem day1;
        [ObservableProperty]
        private RecipeItem day2;
        [ObservableProperty]
        private RecipeItem day3;
        [ObservableProperty]
        private RecipeItem day4;
        [ObservableProperty]
        private RecipeItem day5;
        [ObservableProperty]
        private RecipeItem day6;
        [ObservableProperty]
        private RecipeItem day7;
        [ObservableProperty]
        private RecipeItem day8;
        [ObservableProperty]
        private RecipeItem day9;
        [ObservableProperty]
        private RecipeItem recipe1;
        [ObservableProperty]
        private RecipeItem recipe2;
        [ObservableProperty]
        private RecipeItem recipe3;
        [ObservableProperty]
        private RecipeItem recipe4;
        [ObservableProperty]
        private RecipeItem recipe5;
        [ObservableProperty]
        private RecipeItem recipe6;
        [ObservableProperty]
        private RecipeItem recipe7;
        [ObservableProperty]
        private RecipeItem recipe8;
        [ObservableProperty]
        private RecipeItem recipe9;

        private String colourNoPic = "Gray";
        private String colourNoRecipe = "#BDBDBD";
        private Brush colourBreakfast = new SolidColorBrush(Color.FromArgb("#E6C229"));
        private Brush colourLunch = new SolidColorBrush(Color.FromArgb("#E08E45"));
        private Brush colourDinner = new SolidColorBrush(Color.FromArgb("#388E3C"));

        public HomeViewModel()
        {
            // sample data
            day1 = new RecipeItem { IsDayLabel = true, Day = "Mon" };
            day2 = new RecipeItem { IsDayLabel = true, Day = "" };
            day3 = new RecipeItem { IsDayLabel = true, Day = "Tue" };
            day4 = new RecipeItem { IsDayLabel = true, Day = "" };
            day5 = new RecipeItem { IsDayLabel = true, Day = "" };
            day6 = new RecipeItem { IsDayLabel = true, Day = "Wed" };
            day7 = new RecipeItem { IsDayLabel = true, Day = "" };
            day8 = new RecipeItem { IsDayLabel = true, Day = "" };
            day9 = new RecipeItem { IsDayLabel = true, Day = "Thu" };
            recipe1 = new RecipeItem { IsDayLabel = false, MealTypeColor = colourLunch, RecipeName = "test1", BackgroundColor = colourNoPic};
            recipe2 = new RecipeItem { IsDayLabel = false, MealTypeColor = colourDinner, RecipeName = "test2", BackgroundColor = colourNoPic };
            recipe3 = new RecipeItem { IsDayLabel = false, MealTypeColor = colourBreakfast, RecipeName = "test3", BackgroundColor = colourNoPic };
            recipe4 = new RecipeItem { IsDayLabel = false, MealTypeColor = colourLunch, RecipeName = "test4", BackgroundColor = colourNoPic };
            recipe5 = new RecipeItem { IsDayLabel = false, MealTypeColor = colourDinner, RecipeName = "test5", BackgroundColor = colourNoPic };
            recipe6 = new RecipeItem { IsDayLabel = false, MealTypeColor = colourBreakfast, RecipeName = "test6", BackgroundColor = colourNoPic };
            recipe7 = new RecipeItem { IsDayLabel = false, MealTypeColor = colourLunch, RecipeName = "test7", BackgroundColor = colourNoPic };
            recipe8 = new RecipeItem { IsDayLabel = false, MealTypeColor = colourDinner, RecipeName = "test8", BackgroundColor = colourNoPic };
            recipe9 = new RecipeItem { IsDayLabel = false, MealTypeColor = colourBreakfast, RecipeName = "test9", BackgroundColor = colourNoPic };

        }
    }

    public partial class RecipeItem
    {
        public bool IsDayLabel { get; set; }
        public string? Day { get; set; }
        public string? RecipeName { get; set; }
        public Brush? MealTypeColor { get; set; }
        public String? BackgroundColor { get; set; }
    }
}