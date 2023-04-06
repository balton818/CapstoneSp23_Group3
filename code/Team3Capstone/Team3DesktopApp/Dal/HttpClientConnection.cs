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
            return Task.FromResult(pantry)!;
        }

        return Task.FromResult<List<PantryItem>>(null!);
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
            return Task.FromResult(JsonConvert.DeserializeObject<PantryItem>(result))!;
        }

        return Task.FromResult<PantryItem>(null!);
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
            return Task.FromResult(JsonConvert.DeserializeObject<PantryItem>(result))!;
        }

        return Task.FromResult<PantryItem>(null!);
    }

    /// <summary>Gets the recipes that match the user pantry.</summary>
    /// <param name="userId">The user id of the logged in user.</param>
    /// <param name="client">The http connection client.</param>
    /// <returns>
    ///     a list of recipes that the user isn't missing ingredients for
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
            return Task.FromResult(recipes)!;
        }

        return Task.FromResult<List<Recipe>>(null!);
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
            return Task.FromResult(recipe)!;
        }

        return Task.FromResult<RecipeInformation>(null!);
    }

    /// <summary>Removes the pantry item from the users pantry in the database.</summary>
    /// <param name="toRemove">the pantry item to remove.</param>
    /// <param name="client">The client for connecting.</param>
    /// <returns>
    ///     true if success false otherwise
    /// </returns>
    public Task<bool> RemovePantryItem(PantryItem toRemove, HttpClient client)
    {
        var pantryId = toRemove.PantryId;
        var query = new Uri("User/remove-pantry-item/" + pantryId, UriKind.Relative);
        var json = JsonConvert.SerializeObject(toRemove);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(query, data);

        Console.WriteLine(response.Result);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    /// <summary>Gets the recipe types for filtering from the database.</summary>
    /// <param name="client">The client used to connect to the backend.</param>
    /// <returns>
    ///     the list of recipe types or null if unsuccessful
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
            return Task.FromResult(recipeTypes)!;
        }

        return Task.FromResult<List<string>>(null!);
    }

    /// <summary>Gets the diet types for filtering from the data base.</summary>
    /// <param name="client">The client used for connecting to the backend.</param>
    /// <returns>
    ///     the list of diet types if successful null if not
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
            return Task.FromResult(dietTypes)!;
        }

        return Task.FromResult<List<string>>(null!);
    }

    /// <summary>Browses the recipes available in the database.</summary>
    /// <param name="userId"></param>
    /// <param name="client">The client used to connect.</param>
    /// <param name="recipeType">Type of the recipe the recipe type used to filter results.</param>
    /// <param name="dietType">Type of the diet used to filter results.</param>
    /// <param name="page">The page of recipes the user is navigating to.</param>
    /// <param name="name">The name the recipe name the user is filtering by.</param>
    /// <param name="appliedCuisineType"></param>
    /// <returns>
    ///     a list of recipes and their information if successful null otherwise
    /// </returns>
    public Task<JObject> BrowseRecipes(int userId, HttpClient client, string recipeType, string dietType, int page,
        string name, string? appliedCuisineType)
    {
        if (appliedCuisineType == null)
        {
            appliedCuisineType = "";
        }

        var query = new Uri(
            "Recipe/browse?Type=" + recipeType + "&UserId=" + userId + "&Diet=" + dietType + "&PageNumber=" + page +
            "&Query=" + name + "&Cuisine=" + appliedCuisineType,
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

        return Task.FromResult<JObject>(null!);
    }

    /// <summary>Gets the meal plan for the passed in information.</summary>
    /// <param name="userId">The user id the plan belongs to.</param>
    /// <param name="client">The client to connect to the backend with.</param>
    /// <param name="currentWeek">if set to <c>true</c> get plan for[current week] else next week.</param>
    /// <returns>
    ///     the meal plan if successful null otherwise
    /// </returns>
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

        return Task.FromResult<MealPlan>(null!)!;
    }

    /// <summary>Adds to plan.</summary>
    /// <param name="planId">The plan identifier.</param>
    /// <param name="client">The client.</param>
    /// <param name="meal">The meal.</param>
    /// <returns>
    ///     <br />
    /// </returns>
    public Task<MealPlan?> AddToPlan(int planId, HttpClient client, Meal meal)
    {
        var query = new Uri(
            "MealPlan/add-meal?MealPlanId=" + planId + "&DayOfWeek=" + meal.DayOfWeek + "&MealType=" + meal.MealType +
            "&Recipe.ApiId=" + meal.Recipe!.ApiId + "&Recipe.Title=" + meal.Recipe.Title + "&Recipe.Image=" +
            meal.Recipe.Image + "&Recipe.ImageType=" + meal.Recipe.ImageType, UriKind.Relative);

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

    /// <summary>Updates the plan when a meal already is planned.</summary>
    /// <param name="mealId">The meal id for the new meal.</param>
    /// <param name="client">The client to connect to the backend.</param>
    /// <param name="meal">The meal to be added to the plan.</param>
    /// <returns>
    ///     the recipe added
    /// </returns>
    public Task<Meal> UpdatePlan(int? mealId, HttpClient client, Meal meal)
    {
        var query = new Uri(
            "MealPlan/update-meal?MealId=" + mealId + "&DayOfWeek=" + meal.DayOfWeek + "&MealType=" + meal.MealType +
            "&Recipe.ApiId=" + meal.Recipe.ApiId + "&Recipe.Title=" + meal.Recipe.Title + "&Recipe.Image=" +
            meal.Recipe.Image + "&Recipe.ImageType=" + meal.Recipe.ImageType, UriKind.Relative);

        var response = client.GetAsync(query);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            var recipe = JsonConvert.DeserializeObject<Meal>(result);
            return Task.FromResult(recipe)!;
        }

        return Task.FromResult<Meal>(null);
    }

    /// <summary>Removes the meal from the plan.</summary>
    /// <param name="mealId">The meal Id for the back end.</param>
    /// <param name="client">The client to connect to the backend.</param>
    /// <returns>
    ///     true if meal is removed false otherwise
    /// </returns>
    public Task<bool> RemoveMeal(int? mealId, HttpClient client)
    {
        var query = new Uri(
            "MealPlan/remove-meal?MealId=" + mealId, UriKind.Relative);
        var response = client.GetAsync(query);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    /// <summary>Gets the cuisine types.</summary>
    /// <param name="client">The client to connect to the back end.</param>
    /// <returns>
    ///     the collection of cuisine types that can be applied as filters
    /// </returns>
    public Task<List<string>> GetCuisineTypes(HttpClient client)
    {
        var query = new Uri("App/cuisines", UriKind.Relative);
        var response = client.GetAsync(query);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            var cuisineTypes = JsonConvert.DeserializeObject<List<string>>(result);
            return Task.FromResult(cuisineTypes)!;
        }

        return Task.FromResult<List<string>>(null);
    }

    /// <summary>Gets the grocery list for the current user.</summary>
    /// <param name="userId">The user identifier for the database.</param>
    /// <param name="client">The client for the http connection.</param>
    /// <returns>
    ///     the users list of grocery items if successful null otherwise
    /// </returns>
    public Task<List<GroceryListItem>> GetGroceryList(int userId, HttpClient client)
    {
        var query = new Uri($"User/get-shopping-list/{userId}", UriKind.Relative);
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
            var groceryList = JsonConvert.DeserializeObject<List<GroceryListItem>>(result);
            return Task.FromResult(groceryList)!;
        }

        return Task.FromResult<List<GroceryListItem>>(null!);
    }

    /// <summary>Adds the grocery item.</summary>
    /// <param name="toAdd">The grocery item to add to the users pantry.</param>
    /// <param name="client">The client used for the http connection.</param>
    /// <returns>
    ///     the added grocery item if successful, null otherwise
    /// </returns>
    public Task<GroceryListItem> AddGroceryItem(GroceryListItem toAdd, HttpClient client)
    {
        var query = new Uri("User/add-shopping-list-ingredient", UriKind.Relative);
        var json = JsonConvert.SerializeObject(toAdd);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(query, data);

        Console.WriteLine(response.Result);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            return Task.FromResult(JsonConvert.DeserializeObject<GroceryListItem>(result))!;
        }

        return Task.FromResult<GroceryListItem>(null!);
    }

    /// <summary>Edits the grocery item.</summary>
    /// <param name="toEdit">The grocery item being edited.</param>
    /// <param name="client">The client to connect to the backend.</param>
    /// <returns>
    ///     the edited grocery item if successful, null otherwise
    /// </returns>
    public Task<GroceryListItem> EditGroceryItem(GroceryListItem toEdit, HttpClient client)
    {
        var query = new Uri("User/update-shopping-list-ingredient", UriKind.Relative);
        var json = JsonConvert.SerializeObject(toEdit);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(query, data);

        Console.WriteLine(response.Result);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            return Task.FromResult(JsonConvert.DeserializeObject<GroceryListItem>(result))!;
        }

        return Task.FromResult<GroceryListItem>(null!);
    }

    /// <summary>Removes the grocery item from the users grocery list in the database.</summary>
    /// <param name="toRemove">the grocery item to remove.</param>
    /// <param name="client">The client for connecting.</param>
    /// <returns>
    ///     true if success false otherwise
    /// </returns>
    public Task<bool> RemoveGroceryItem(GroceryListItem toRemove, HttpClient client)
    {
        var groceryListId = toRemove.ShoppingListId;
        var query = new Uri("User/remove-shopping-list-ingredient/" + groceryListId, UriKind.Relative);
        var json = JsonConvert.SerializeObject(toRemove);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = client.PostAsync(query, data);

        Console.WriteLine(response.Result);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    /// <summary>Adds ingredients to grocery list by recipe id.</summary>
    /// <param name="recipeIds">The recipe ids for the recipes in the users meal plan.</param>
    /// <param name="userId">The currently logged in user's id</param>
    /// <param name="client">The client to connect to hte backend.</param>
    /// <returns>
    ///   The user's new grocery list made of items they need in their pantry to complete the planned recipes
    /// </returns>
    public Task<List<GroceryListItem>> AddToGroceryListByRecipeId(List<int> recipeIds, int userId, HttpClient client)
    {
        var query = new Uri("Recipe/add-to-shopping-list-by-recipe-ids?userId=" + userId, UriKind.Relative);
        var json = JsonConvert.SerializeObject(recipeIds);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync(query, data);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            var groceryList = JsonConvert.DeserializeObject<List<GroceryListItem>>(result);
            return Task.FromResult(groceryList)!;
        }

        return Task.FromResult<List<GroceryListItem>>(null);
    }

    /// <summary>Buys the ingredients from the grocery list.</summary>
    /// <param name="ingredients">The ingredients to be purchased.</param>
    /// <param name="userId">The currently logged in user.</param>
    /// <param name="client">The client to connect to the backend.</param>
    /// <returns>
    ///   the users new pantry which includes the purchased ingredients and their quantities
    /// </returns>
    public Task<List<PantryItem>> BuyIngredientsFromList(List<GroceryListItem> ingredients, int userId,
        HttpClient client)
    {
        var query = new Uri("Recipe/buy-ingredients?userId=" + userId, UriKind.Relative);
        var json = JsonConvert.SerializeObject(ingredients);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync(query, data);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            var groceryList = JsonConvert.DeserializeObject<List<PantryItem>>(result);
            return Task.FromResult(groceryList)!;
        }

        return Task.FromResult<List<PantryItem>>(null);
    }

    /// <summary>removes the ingredients from the users pantry for the meal that has been prepared.</summary>
    /// <param name="ingredients">The ingredients that have been used.</param>
    /// <param name="userId">The currently logged in user.</param>
    /// <param name="client">The client to connect to the backend.</param>
    /// <returns>
    ///   the users new pantry collection
    /// </returns>
    public Task<List<PantryItem>> UseIngredientsFromList(List<PantryItem> ingredients, int userId, HttpClient client)
    {
        var query = new Uri("Recipe/use-ingredients?userId=" + userId, UriKind.Relative);
        var json = JsonConvert.SerializeObject(ingredients);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync(query, data);
        Console.WriteLine(response);
        if (response.Result.IsSuccessStatusCode)
        {
            var readTask = response.Result.Content.ReadAsStringAsync();
            readTask.Wait();

            var result = readTask.Result;
            Console.WriteLine(result);
            var groceryList = JsonConvert.DeserializeObject<List<PantryItem>>(result);
            return Task.FromResult(groceryList)!;
        }

        return Task.FromResult<List<PantryItem>>(null);
    }

    #endregion
}