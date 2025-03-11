﻿using SQLite;
using MeinRezeptbuch.Models;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

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
                // Log or handle the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Add a new ingredient entry
        public Task<int> AddIngredientEntryAsync(IngredientEntry ingredientEntry)
        {
            return _database.InsertAsync(ingredientEntry);
        }

        // Retrieve all ingredient entries
        public Task<List<IngredientEntry>> GetAllIngredientEntriesAsync()
        {
            return _database.Table<IngredientEntry>().ToListAsync();
        }

        // Update an ingredient entry
        public Task<int> UpdateIngredientEntryAsync(IngredientEntry ingredientEntry)
        {
            return _database.UpdateAsync(ingredientEntry);
        }

        // Delete an ingredient entry
        public Task<int> DeleteIngredientEntryAsync(IngredientEntry ingredientEntry)
        {
            return _database.DeleteAsync(ingredientEntry);
        }

        // Retrieve ingredient entries for a specific recipe
        public Task<List<IngredientEntry>> GetIngredientEntriesByRecipeIdAsync(int recipeId)
        {
            return _database.Table<IngredientEntry>().Where(i => i.RecipeId == recipeId).ToListAsync();
        }

        // Delete all ingredient entries for a specific recipe
        public Task<int> DeleteIngredientEntriesByRecipeIdAsync(int recipeId)
        {
            return _database.ExecuteAsync("DELETE FROM IngredientEntry WHERE RecipeId = ?", recipeId);
        }

        // Retrieve an ingredient entry by ID
        public async Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
        {
            return await _ingredientService.GetIngredientByIdAsync(ingredientId);
        }

    }
}
