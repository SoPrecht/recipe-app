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
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ingredients.db");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Ingredient>().Wait();
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
}
