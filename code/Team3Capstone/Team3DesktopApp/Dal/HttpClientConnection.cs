using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using Newtonsoft.Json;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.Dal;

public class HttpClientConnection
{

    private static readonly HttpClient client = new HttpClient() { BaseAddress = new Uri($"https://localhost:7278/api/") };


    public async Task<int> ValidateUser(string username, string password)
    {
        Uri query = new Uri($"User/{username},{password}", UriKind.Relative);
        Console.WriteLine(query);
        HttpResponseMessage response = await client.GetAsync(query);
        Console.WriteLine(response);
        if (response.IsSuccessStatusCode)
        {
            var readTask = response.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            return Int32.Parse(result);
        }

        return -1;
    }

    public async Task<int> RegisterUser(User toCreate)
    {
        Uri query = new Uri($"User/create", UriKind.Relative);
        string json = JsonConvert.SerializeObject(toCreate);

        StringContent data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

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

    public async Task<List<PantryItem>> GetPantry(int userId)
    {
        Uri query = new Uri($"User/get-pantry/{userId}", UriKind.Relative);
        Console.WriteLine(query);
        string json = JsonConvert.SerializeObject(userId);
        StringContent data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = client.PostAsync(query, data);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            List<PantryItem> pantry = JsonConvert.DeserializeObject<List<PantryItem>>(result);
            return pantry;
        }

        return null;


    }

    public async Task<PantryItem> AddPantryItem(PantryItem toAdd)
    {
        Uri query = new Uri($"User/add-pantry-item", UriKind.Relative);
        string json = JsonConvert.SerializeObject(toAdd);

        StringContent data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

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

    public async Task<PantryItem> EditPantryItem(PantryItem toEdit)
    {
        Uri query = new Uri($"User/update-pantry-item", UriKind.Relative);
        string json = JsonConvert.SerializeObject(toEdit);

        StringContent data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

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

    public async Task<List<Recipe>> GetRecipes(int userId)
    {
        Uri query = new Uri($"Recipe/get-by-pantry/{userId}", UriKind.Relative);
        var response = client.GetAsync(query);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(result);
            return recipes;
        }

        return null;

    }

    public async Task<RecipeInformation> GetRecipeDetail(int recipeId)
    {
        Uri query = new Uri($"Recipe/get-recipe-infromation/{recipeId}", UriKind.Relative);
        var response = client.GetAsync(query);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            RecipeInformation recipe = JsonConvert.DeserializeObject<RecipeInformation>(result);
            return recipe;
        }

        return null;

    }



}