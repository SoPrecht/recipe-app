using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using MeinRezeptbuch.Models;
using System.Diagnostics;

namespace MeinRezeptbuch.Services
{
    public class RecipeService
    {
        private readonly SQLiteAsyncConnection _database;
        private readonly IngredientEntryService _ingredientEntryService;

        public RecipeService(IngredientEntryService ingredientEntryService)
        {
            try
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "recipes.db");
                _database = new SQLiteAsyncConnection(dbPath);
                _database.CreateTableAsync<Recipe>().Wait();
                _ingredientEntryService = ingredientEntryService;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Add a new recipe
        public Task<int> AddRecipeAsync(Recipe recipe)
        {
            Debug.WriteLine($"Adding recipe: {recipe.Name} {recipe.Id}");
            return _database.InsertAsync(recipe);
        }

        // Retrieve all recipes
        public Task<List<Recipe>> GetAllRecipesAsync()
        {
            return _database.Table<Recipe>().ToListAsync();
        }

        // Retrieve a recipe by ID and include its ingredients
        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            var recipe = await _database.Table<Recipe>().Where(r => r.Id == id).FirstOrDefaultAsync();
            if (recipe != null)
            {
                recipe.Ingredients = await _ingredientEntryService.GetIngredientEntriesByRecipeIdAsync(id);
            }
            return recipe;
        }

        // Retrieve all ingredient entries for a specific recipe
        public Task<List<IngredientEntry>> GetIngredientEntriesByRecipeIdAsync(int recipeId)
        {
            return _database.Table<IngredientEntry>().Where(i => i.RecipeId == recipeId).ToListAsync();
        }

        // Update an existing recipe
        public Task<int> UpdateRecipeAsync(Recipe recipe)
        {
            Debug.WriteLine($"Updating recipe: {recipe.Id}");
            return _database.UpdateAsync(recipe);
        }

        // Delete a recipe and its associated ingredient entries
        public async Task<int> DeleteRecipeAsync(Recipe recipe)
        {
            await _ingredientEntryService.DeleteIngredientEntriesByRecipeIdAsync( recipe.Id);
            return await _database.DeleteAsync(recipe);
        }

    }
}