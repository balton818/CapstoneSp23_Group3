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
    #region Data members

    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:7278/api/") };

    #endregion

    #region Methods

    /// <summary>Validates the user.</summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public async Task<int> ValidateUser(string username, string password)
    {
        var query = new Uri($"User/{username},{password}", UriKind.Relative);
        Console.WriteLine(query);
        var response = await Client.GetAsync(query);
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

    public async Task<int> RegisterUser(User toCreate)
    {
        var query = new Uri("User/create", UriKind.Relative);
        var json = JsonConvert.SerializeObject(toCreate);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = Client.PostAsync(query, data);

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

    public async Task<List<PantryItem>> GetPantry(int userId)
    {
        var query = new Uri($"User/get-pantry/{userId}", UriKind.Relative);
        Console.WriteLine(query);
        var json = JsonConvert.SerializeObject(userId);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = Client.PostAsync(query, data);
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

    public async Task<PantryItem> AddPantryItem(PantryItem toAdd)
    {
        var query = new Uri("User/add-pantry-item", UriKind.Relative);
        var json = JsonConvert.SerializeObject(toAdd);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = Client.PostAsync(query, data);

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

    public async Task<PantryItem> EditPantryItem(PantryItem toEdit)
    {
        var query = new Uri("User/update-pantry-item", UriKind.Relative);
        var json = JsonConvert.SerializeObject(toEdit);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = Client.PostAsync(query, data);

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

    public async Task<List<Recipe>> GetRecipes(int userId)
    {
        var query = new Uri($"Recipe/get-by-pantry/{userId}", UriKind.Relative);
        var response = Client.GetAsync(query);
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

    public async Task<RecipeInformation> GetRecipeDetail(int recipeId)
    {
        var query = new Uri($"Recipe/get-recipe-infromation/{recipeId}", UriKind.Relative);
        var response = Client.GetAsync(query);
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

    #endregion
}