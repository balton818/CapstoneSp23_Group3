using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.Dal;

/// <summary>
///     The HttpClientConnection class
///     this is used to connect the app to the backend.
/// </summary>
public class HttpClientConnection
{
    #region Methods

    /// <summary>Validates the user via the database.</summary>
    /// <param name="username">The username that the user enters.</param>
    /// <param name="password">The password that the user has entered.</param>
    /// <param name="client"></param>
    /// <returns>
    ///     the Users ID if login is successful, -1 if not.
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
    /// <param name="toCreate">The user that will be created on the server side.</param>
    /// <param name="client">the httpclient that facilitates the connection to the server.</param>
    /// <returns>
    ///     the Users ID if login is successful, -1 if not.
    /// </returns>
    public Task<int> RegisterUser(User toCreate, HttpClient client)
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
            return Task.FromResult(int.Parse(result));
        }

        return Task.FromResult(-1);
    }

    /// <summary>Gets the pantry of the current user.</summary>
    /// <param name="userId">The user identifier for the database.</param>
    /// <param name="client">The client for the http connection.</param>
    /// <returns>
    ///     the users list of pantry items if successful null otherwise
    /// </returns>
    public Task<List<PantryItem>> GetPantry(int userId, HttpClient client)
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
            return Task.FromResult(pantry);
        }

        return Task.FromResult<List<PantryItem>>(null);
    }

    /// <summary>Adds the pantry item.</summary>
    /// <param name="toAdd">The pantry item to add to the users pantry.</param>
    /// <param name="client">The client used for the http connection.</param>
    /// <returns>
    ///     the added pantry item if successful, null otherwise
    /// </returns>
    public Task<PantryItem> AddPantryItem(PantryItem toAdd, HttpClient client)
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
            return Task.FromResult(JsonConvert.DeserializeObject<PantryItem>(result));
        }

        return Task.FromResult<PantryItem>(null);
    }

    /// <summary>Edits the pantry item.</summary>
    /// <param name="toEdit">The pantry item being edited.</param>
    /// <param name="client">The client to connect to the backend.</param>
    /// <returns>
    ///     the edited pantry item if successful, null otherwise
    /// </returns>
    public Task<PantryItem> EditPantryItem(PantryItem toEdit, HttpClient client)
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
            return Task.FromResult(JsonConvert.DeserializeObject<PantryItem>(result));
        }

        return Task.FromResult<PantryItem>(null);
    }

    /// <summary>Gets the recipes that match the user pantry.</summary>
    /// <param name="userId">The user id of the logged in user.</param>
    /// <param name="client">The http connection client.</param>
    /// <returns>
    ///     a list of recipes that the user isnt missing ingredients for
    /// </returns>
    public Task<List<Recipe>> GetRecipes(int userId, HttpClient client)
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
            return Task.FromResult(recipes);
        }

        return Task.FromResult<List<Recipe>>(null);
    }

    /// <summary>Gets the recipe detail.</summary>
    /// <param name="recipeId">The recipe id for database use.</param>
    /// <param name="client">The client for connecting.</param>
    /// <returns>
    ///     the recipe detail information if successful, null otherwise
    /// </returns>
    public Task<RecipeInformation> GetRecipeDetail(int recipeId, HttpClient client)
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
            return Task.FromResult(recipe);
        }

        return Task.FromResult<RecipeInformation>(null);
    }

    /// <summary>Removes the pantry item from the users pantry in the database.</summary>
    /// <param name="toRemove">the pantry item to remove.</param>
    /// <param name="client">The client for connecting.</param>
    /// <returns>
    /// true if success false otherwise
    /// </returns>
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

    /// <summary>Gets the recipe types for filtering from the database.</summary>
    /// <param name="client">The client used to connect to the backend.</param>
    /// <returns>
    ///   the list of recipe types or null if unsuccessful
    /// </returns>
    public Task<List<string>> GetRecipeTypes(HttpClient client)
    {
        var query = new Uri("App/meal-types", UriKind.Relative);
        var response = client.GetAsync(query);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            var recipeTypes = JsonConvert.DeserializeObject<List<string>>(result);
            return Task.FromResult(recipeTypes);
        }

        return Task.FromResult<List<string>>(null);
    }

    /// <summary>Gets the diet types for filtering from the data base.</summary>
    /// <param name="client">The client used for connecting to the backend.</param>
    /// <returns>
    ///   the list of diet types if successful null if not
    /// </returns>
    public Task<List<string>> GetDietTypes(HttpClient client)
    {
        var query = new Uri("App/diet", UriKind.Relative);
        var response = client.GetAsync(query);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            var dietTypes = JsonConvert.DeserializeObject<List<string>>(result);
            return Task.FromResult(dietTypes);
        }

        return Task.FromResult<List<string>>(null);
    }

    /// <summary>Browses the recipes available in the database.</summary>
    /// <param name="client">The client used to connect.</param>
    /// <param name="recipeType">Type of the recipe the recipe type used to filter results.</param>
    /// <param name="dietType">Type of the diet used to filter results.</param>
    /// <param name="page">The page of recipes the user is navigating to.</param>
    /// <param name="name">The name the recipe name the user is filtering by.</param>
    /// <returns>
    ///   a list of recipes and their information if successful null otherwise
    /// </returns>
    public Task<JObject> BrowseRecipes(int userId, HttpClient client, string recipeType, string dietType, int page, string name)
    {
        var query = new Uri(
            "Recipe/browse?Type=" + recipeType + "&UserId=" + userId + "&Diet=" + dietType + "&PageNumber=" + page + "&Query=" + name,
            UriKind.Relative);
        var response = client.GetAsync(query);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            var json = JObject.Parse(result);
            return Task.FromResult(json);
        }

        return Task.FromResult<JObject>(null);
    }

    public Task<MealPlan?> GetPlan(int userId, HttpClient client, bool currentWeek)
    {
        Uri query;
        if (currentWeek)
        {
            query = new Uri(
                "MealPlan/get-this-week?userId=" + userId, UriKind.Relative);
        }
        else
        {
            query = new Uri(
                "MealPlan/get-next-week?userId=" + userId, UriKind.Relative);
        }

        var response = client.GetAsync(query);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            var recipe = JsonConvert.DeserializeObject<MealPlan>(result);
            return Task.FromResult(recipe);
        }

        return Task.FromResult<MealPlan>(null);
    }

    #endregion
}