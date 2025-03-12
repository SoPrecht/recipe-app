using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeinRezeptbuch.Models;
using CommunityToolkit.Maui.Views;
using ZXing.Net.Maui;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace MeinRezeptbuch.Services
{
    public class QRCodeTransferService
    {
        private readonly RecipeService _recipeService;
        private readonly IngredientService _ingredientService;
        private readonly IngredientEntryService _ingredientEntryService;

        public QRCodeTransferService(RecipeService recipeService, IngredientService ingredientService, IngredientEntryService ingredientEntryService)
        {
            _recipeService = recipeService;
            _ingredientService = ingredientService;
            _ingredientEntryService = ingredientEntryService;
        }

        public async Task<string> GenerateQRCodeAsync()
        {
            var data = new
            {
                Recipes = await _recipeService.GetAllRecipesAsync(),
                Ingredients = await _ingredientService.GetAllIngredientsAsync(),
                IngredientEntries = await _ingredientEntryService.GetAllIngredientEntriesAsync()
            };

            string jsonData = JsonConvert.SerializeObject(data);
            string filePath = Path.Combine(FileSystem.CacheDirectory, "recipe_export.json");
            await File.WriteAllTextAsync(filePath, jsonData);

            return filePath;
        }

        public async Task<bool> ImportQRCodeDataAsync(string jsonData)
        {
            try
            {
                var importedData = JsonConvert.DeserializeObject<dynamic>(jsonData);

                foreach (var recipe in importedData.Recipes)
                {
                    await _recipeService.AddRecipeAsync(JsonConvert.DeserializeObject<Recipe>(recipe.ToString()));
                }
                foreach (var ingredient in importedData.Ingredients)
                {
                    await _ingredientService.AddIngredientAsync(JsonConvert.DeserializeObject<Ingredient>(ingredient.ToString()));
                }
                foreach (var ingredientEntry in importedData.IngredientEntries)
                {
                    await _ingredientEntryService.AddIngredientEntryAsync(JsonConvert.DeserializeObject<IngredientEntry>(ingredientEntry.ToString()));
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error importing QR code data: " + ex.Message);
                return false;
            }
        }
    }
}
