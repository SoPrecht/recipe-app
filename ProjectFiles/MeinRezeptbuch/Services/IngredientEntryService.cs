using SQLite;
using MeinRezeptbuch.Models;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System.Diagnostics;

namespace MeinRezeptbuch.Services
{
    public class IngredientEntryService
    {
        private readonly SQLiteAsyncConnection _database;
        private readonly IngredientService _ingredientService;

        public IngredientEntryService(IngredientService ingredientService)
        {
            try
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ingredientEntries.db");
                _database = new SQLiteAsyncConnection(dbPath);
                _database.CreateTableAsync<IngredientEntry>().Wait();
                _ingredientService = ingredientService;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Add a new ingredient entry
        public async Task<int> AddIngredientEntryAsync(IngredientEntry ingredientEntry)
        {
            Debug.WriteLine($"Adding ingredient entry: RecipeId={ingredientEntry.RecipeId}, IngredientId={ingredientEntry.IngredientId}");
            return await _database.InsertAsync(ingredientEntry);
        }

        // Retrieve all ingredient entries
        public async Task<List<IngredientEntry>> GetAllIngredientEntriesAsync()
        {
            var entries = await _database.Table<IngredientEntry>().ToListAsync();
            Debug.WriteLine($"Total ingredient entries retrieved: {entries.Count}");
            return entries;
        }

        // Update an ingredient entry
        public async Task<int> UpdateIngredientEntryAsync(IngredientEntry ingredientEntry)
        {
            Debug.WriteLine($"Updating ingredient entry: {ingredientEntry.Id}");
            return await _database.UpdateAsync(ingredientEntry);
        }

        // Delete an ingredient entry
        public async Task<int> DeleteIngredientEntryAsync(IngredientEntry ingredientEntry)
        {
            Debug.WriteLine($"Deleting ingredient entry: {ingredientEntry.Id}");
            return await _database.DeleteAsync(ingredientEntry);
        }

        // Retrieve ingredient entries for a specific recipe
        public async Task<List<IngredientEntry>> GetIngredientEntriesByRecipeIdAsync(int recipeId)
        {
            Debug.WriteLine($"Fetching ingredient entries for RecipeId={recipeId}");
            var entries = await _database.Table<IngredientEntry>().Where(i => i.RecipeId == recipeId).ToListAsync();
            Debug.WriteLine($"Ingredient entries found: {entries.Count}");
            return entries;
        }

        // Delete all ingredient entries for a specific recipe
        public async Task<int> DeleteIngredientEntriesByRecipeIdAsync(int recipeId)
        {
            Debug.WriteLine($"Deleting all ingredient entries for RecipeId={recipeId}");
            return await _database.ExecuteAsync("DELETE FROM IngredientEntry WHERE RecipeId = ?", recipeId);
        }

        // Retrieve an ingredient entry by ID
        public async Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
        {
            Debug.WriteLine($"Fetching ingredient by ID={ingredientId}");
            return await _ingredientService.GetIngredientByIdAsync(ingredientId);
        }
    }
}
