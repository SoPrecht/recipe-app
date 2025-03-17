using SQLite;
using MeinRezeptbuch.Models;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

public class IngredientService
{
    private readonly SQLiteAsyncConnection _database;

    public IngredientService()
    {
        try
        {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ingredients.db");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Ingredient>().Wait();
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Add a new ingredient
    public Task<int> AddIngredientAsync(Ingredient ingredient)
    {
        return _database.InsertAsync(ingredient);
    }

    // Retrieve all ingredients
    public Task<List<Ingredient>> GetAllIngredientsAsync()
    {
        return _database.Table<Ingredient>().ToListAsync();
    }

    // Retrieve ingredient by name
    public Task<Ingredient> GetIngredientByNameAsync(string name)
    {
        return _database.Table<Ingredient>().Where(i => i.Name == name).FirstOrDefaultAsync();
    }

    // Update an existing ingredient
    public Task<int> UpdateIngredientAsync(Ingredient ingredient)
    {
        return _database.UpdateAsync(ingredient);
    }

    // Delete an ingredient
    public Task<int> DeleteIngredientAsync(Ingredient ingredient)
    {
        return _database.DeleteAsync(ingredient);
    }

    // Get ingredient by Id
    public Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
    {
        return _database.Table<Ingredient>().Where(i => i.ID == ingredientId).FirstOrDefaultAsync();
    }

    public async Task<List<Ingredient>> GetIngredientsByPartialNameAsync(string partialName)
    {
        if (string.IsNullOrWhiteSpace(partialName))
            return new List<Ingredient>();

        return await _database.Table<Ingredient>()
            .Where(i => i.Name.Contains(partialName))
            .ToListAsync();
    }

}
