using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.Dal;

public class HttpClientConnection
{


    #region Methods

    /// <summary>Validates the user.</summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <param name="client"></param>
    /// <returns>
    ///   <br />
    /// </returns>
    public async Task<int> ValidateUser(string username, string password, HttpClient client)
    {
        var query = new Uri($"User/{username},{password}", UriKind.Relative);
        Console.WriteLine(query);
        var response = await client.GetAsync(query);
        Console.WriteLine(response);
        if (response.IsSuccessStatusCode)
        {
            var readTask = response.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            return int.Parse(result);
        }

        return -1;
    }

    /// <summary>Registers the user.</summary>
    /// <param name="toCreate">To create.</param>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public async Task<int> RegisterUser(User toCreate, HttpClient client)
    {
        var query = new Uri("User/create", UriKind.Relative);
        var json = JsonConvert.SerializeObject(toCreate);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(query, data);

        Console.WriteLine(response.Result);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();
            var result = readTask.Result;
            return int.Parse(result);
        }

        return -1;
    }

    /// <summary>Gets the pantry.</summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public async Task<List<PantryItem>> GetPantry(int userId, HttpClient client)
    {
        var query = new Uri($"User/get-pantry/{userId}", UriKind.Relative);
        Console.WriteLine(query);
        var json = JsonConvert.SerializeObject(userId);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync(query, data);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            var pantry = JsonConvert.DeserializeObject<List<PantryItem>>(result);
            return pantry;
        }

        return null;
    }

    /// <summary>Adds the pantry item.</summary>
    /// <param name="toAdd">To add.</param>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public async Task<PantryItem> AddPantryItem(PantryItem toAdd, HttpClient client)
    {
        var query = new Uri("User/add-pantry-item", UriKind.Relative);
        var json = JsonConvert.SerializeObject(toAdd);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(query, data);

        Console.WriteLine(response.Result);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            return JsonConvert.DeserializeObject<PantryItem>(result);
        }

        return null;
    }

    /// <summary>Edits the pantry item.</summary>
    /// <param name="toEdit">To edit.</param>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public async Task<PantryItem> EditPantryItem(PantryItem toEdit, HttpClient client)
    {
        var query = new Uri("User/update-pantry-item", UriKind.Relative);
        var json = JsonConvert.SerializeObject(toEdit);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(query, data);

        Console.WriteLine(response.Result);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            return JsonConvert.DeserializeObject<PantryItem>(result);
        }

        return null;
    }

    /// <summary>Gets the recipes.</summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public async Task<List<Recipe>> GetRecipes(int userId, HttpClient client)
    {
        var query = new Uri($"Recipe/get-by-pantry/{userId}", UriKind.Relative);
        var response = client.GetAsync(query);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            var recipes = JsonConvert.DeserializeObject<List<Recipe>>(result);
            return recipes;
        }

        return null;
    }

    /// <summary>Gets the recipe detail.</summary>
    /// <param name="recipeId">The recipe identifier.</param>
    /// <param name="client">The client.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public async Task<RecipeInformation> GetRecipeDetail(int recipeId, HttpClient client)
    {
        var query = new Uri($"Recipe/get-recipe-infromation/{recipeId}", UriKind.Relative);
        var response = client.GetAsync(query);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            var recipe = JsonConvert.DeserializeObject<RecipeInformation>(result);
            return recipe;
        }

        return null;
    }

    /// <summary>Removes the pantry item.</summary>
    /// <param name="toRemove">To remove.</param>
    /// <param name="client">The client.</param>
    public Task<bool> RemovePantryItem(PantryItem toRemove, HttpClient client)
    {
        var pantryID = toRemove.PantryId;
        var query = new Uri("User/remove-pantry-item/" + pantryID, UriKind.Relative);
        var json = JsonConvert.SerializeObject(toRemove);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(query, data);

        Console.WriteLine(response.Result);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            return Task.FromResult(true);
        }

        return Task.FromResult(false);

    }

    #endregion
}