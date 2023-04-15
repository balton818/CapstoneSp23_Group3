using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

/// <summary>
///     the view model class that wraps the other view models
/// </summary>
public class FoodieViewModel
{
    #region Data members

    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:7278/api/") };
    private readonly FoundRecipeViewModel foundRecipeViewModel;
    private readonly LoginViewModel loginViewModel;
    private readonly RegistrationViewModel registrationViewModel;
    private readonly RecipeDetailViewModel recipeDetailViewModel;
    private readonly PantryViewModel pantryViewModel;
    private readonly BrowseRecipesViewModel browseRecipesViewModel;
    private readonly MealPlanViewModel mealPlanViewModel;
    private readonly GroceryListViewModel groceryListViewModel;

    #endregion

    #region Properties

    /// <summary>Gets or sets the client to set.</summary>
    /// <value>The client to set.</value>
    public HttpClient ClientToSet { get; set; }

    /// <summary>Gets or sets the userid.</summary>
    /// <value>The currently logged in users ID.</value>
    public int Userid { get; set; }

    /// <summary>Gets or sets the plan type and date to add.</summary>
    /// <value>The plan type and date to add.</value>
    public Tuple<DayOfWeek?, MealType?>? PlanTypeAndDateToAdd { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="FoodieViewModel" /> class.</summary>
    public FoodieViewModel()
    {
        this.recipeDetailViewModel = new RecipeDetailViewModel();
        this.foundRecipeViewModel = new FoundRecipeViewModel();
        this.loginViewModel = new LoginViewModel();
        this.registrationViewModel = new RegistrationViewModel();
        this.pantryViewModel = new PantryViewModel();
        this.browseRecipesViewModel = new BrowseRecipesViewModel();
        this.mealPlanViewModel = new MealPlanViewModel();
        this.groceryListViewModel = new GroceryListViewModel();
        this.ClientToSet = Client;
        this.mealPlanViewModel.GetMealPlans(this.Userid, this.ClientToSet).ConfigureAwait(true);
    }

    #endregion

    #region Methods

    /// <summary>Verifies the login info inputted by the user.</summary>
    /// <param name="username">The username entered.</param>
    /// <param name="password">The password entered.</param>
    /// <returns>
    ///     the user id of the user if the login was successful, -1 if the login was unsuccessful
    /// </returns>
    public async Task<int> Login(string username, string password)
    {
        var result = await this.loginViewModel.LoginAsync(username, password, this.ClientToSet);
        if (result >= 0)
        {
            this.Userid = result;
        }

        return result;
    }

    /// <summary>Gets the pantry for the current user.</summary>
    /// <returns>
    ///     a list of ingredients and their quantities that the user had previously entered into the system
    /// </returns>
    public async Task<List<PantryItem>> GetPantry()
    {
        var recipes = new List<PantryItem>();

        recipes.AddRange((await this.pantryViewModel.GetPantry(this.Userid, this.ClientToSet))!);
        return recipes;
    }

    /// <summary>Gets the recipes.</summary>
    /// <returns>
    ///     A list of recipes that the user can currently cook based on their pantry ingredients
    /// </returns>
    public List<string?> GetRecipes()
    {
        var recipeNames = new List<string?>();
        foreach (var recipe in this.foundRecipeViewModel.GetRecipes(this.Userid, this.ClientToSet)!)
        {
            recipeNames.Add(recipe.Title);
        }

        return recipeNames;
    }

    /// <summary>Registers the user.</summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <returns>
    ///     the new user's id if successful -1 otherwise.
    /// </returns>
    public async Task<int> RegisterUser(string username, string password, string email, string firstName,
        string lastName)
    {
        return await this.registrationViewModel.RegisterAsync(username, password, email, firstName, lastName,
            this.ClientToSet);
    }

    /// <summary>Adds an ingredient to the user's pantry.</summary>
    /// <param name="name">The name of the ingredient being added.</param>
    /// <param name="quantity">The quantity being added</param>
    /// <param name="unit">The unit of measurement for the quantity being added.</param>
    /// <returns>
    ///     the added pantryItem or null if unsuccessful
    /// </returns>
    public async Task<PantryItem> AddPantryIngredient(string? name, int quantity, string unit)
    {
        return await this.pantryViewModel.AddIngredient(this.Userid, name, quantity, this.ClientToSet, unit);
    }

    /// <summary>Adds an ingredient to the user's grocery list.</summary>
    /// <param name="name">The name of the ingredient being added.</param>
    /// <param name="quantity">The quantity being added</param>
    /// <param name="unit">The unit of measurement for the quantity being added.</param>
    /// <returns>
    ///     the added pantryItem or null if unsuccessful
    /// </returns>
    public async Task<GroceryListItem> AddGroceryIngredient(string? name, int quantity, string unit)
    {
        return await this.groceryListViewModel.AddIngredient(this.Userid, name, quantity, this.ClientToSet, unit);
    }

    /// <summary>The logic to display the recipe details upon user navigation from the Found recipes page</summary>
    /// <param name="recipeName">Name of the recipe the user wants details for.</param>
    /// <returns>
    ///     The recipe information if successful ie the steps, ingredients etc. null otherwise
    /// </returns>
    public async Task<RecipeInformation> RecipeDetailNavFound(string? recipeName)
    {
        this.foundRecipeViewModel.SelectedRecipeTitle = recipeName;
        return await this.recipeInformation(recipeName, this.foundRecipeViewModel.Recipes);
    }

    /// <summary>The logic to display the recipe details upon user navigation from the Browse recipes page</summary>
    /// <param name="recipeName">Name of the recipe the user wants details for.</param>
    /// <returns>
    ///     The recipe information if successful ie the steps, ingredients etc. null otherwise
    /// </returns>
    public async Task<RecipeInformation> RecipeDetailNavBrowse(string? recipeName)
    {
        this.browseRecipesViewModel.SelectedRecipeTitle = recipeName;
        return await this.recipeInformation(recipeName, this.browseRecipesViewModel.Recipes);
    }

    private async Task<RecipeInformation> recipeInformation(string? recipeName, List<Recipe>? recipes)
    {
        if (recipes != null)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.Title!.Equals(recipeName) && recipe.ApiId != null)
                {
                    this.recipeDetailViewModel.CurrentRecipe = recipe;
                    await this.recipeDetailViewModel.RecipeDetailNav((int)recipe.ApiId, this.ClientToSet);
                }
            }
        }

        return this.recipeDetailViewModel.RecipeInfo!;
    }

    /// <summary>Allows for navigation to recipe detail from the meal planner.</summary>
    /// <param name="day">The day the recipe exists on.</param>
    /// <param name="type">The type of meal the recipe is.</param>
    /// <returns>
    ///     the recipe info for the given plan
    /// </returns>
    public async Task<RecipeInformation> RecipeDetailNavPlan(DayOfWeek day, MealType type)
    {
        var recipe = this.mealPlanViewModel.GetRecipe(day, type);
        if (recipe.ApiId != null)
        {
            await this.recipeDetailViewModel.RecipeDetailNav((int)recipe.ApiId, this.ClientToSet);
        }

        return this.recipeDetailViewModel.RecipeInfo!;
    }

    /// <summary>Edits the Pantry ingredient in the users pantry.</summary>
    /// <param name="ingredientName">Name of the ingredient.</param>
    /// <param name="ingredientAmount">The ingredient amount.</param>
    /// <returns> the edited item if successful null otherwise </returns>
    public async Task<PantryItem> EditPantryIngredient(string? ingredientName, int ingredientAmount)
    {
        return await this.pantryViewModel.EditIngredientAmount(ingredientName, ingredientAmount, this.ClientToSet);
    }

    /// <summary>Edits the grocery ingredient in the users grocery list.</summary>
    /// <param name="ingredientName">Name of the ingredient.</param>
    /// <param name="ingredientAmount">The ingredient amount.</param>
    /// <returns> the edited item if successful null otherwise </returns>
    public async Task<GroceryListItem> EditGroceryIngredient(string? ingredientName, int ingredientAmount)
    {
        return await this.groceryListViewModel.EditIngredientAmount(ingredientName, ingredientAmount, this.ClientToSet);
    }

    /// <summary>Gets the recipe ingredients form the recipe information.</summary>
    /// <returns>
    ///     A list of the ingredients required to make a recipe includes quantity name and unit of measure
    /// </returns>
    public List<string> GetRecipeIngredients()
    {
        var ingredients = new List<string>();
        foreach (var ingredient in this.recipeDetailViewModel.RecipeInfo!.Ingredients!)
        {
            ingredients.Add(ingredient.IngredientName + " " + ingredient.Quantity + " " + ingredient.Unit);
        }

        return ingredients;
    }

    /// <summary>Gets the recipe steps.</summary>
    /// <returns>
    ///     An ordered list of steps that instruct how to prepare a recipe
    /// </returns>
    public List<string> GetRecipeSteps()
    {
        var steps = new List<string>();
        if (this.recipeDetailViewModel.RecipeInfo!.Steps!.Count == 1)
        {
            return this.splitSteps();
        }

        foreach (var step in this.recipeDetailViewModel.RecipeInfo.Steps)
        {
            steps.Add(step.StepNumber + ". " + step.Instructions);
        }

        return steps;
    }

    private List<string> splitSteps()
    {
        var count = 1;
        var splitToSteps = new List<string>();
        var steps = new List<string>();
        splitToSteps.AddRange(this.recipeDetailViewModel.RecipeInfo.Steps[0].Instructions.Split('.'));
        foreach (var step in splitToSteps)
        {
            if (!string.IsNullOrEmpty(step))
            {
                steps.Add(count + ". " + step);
                count++;
            }
        }

        return steps;
    }

    /// <summary>Gets the recipe image.</summary>
    /// <returns>
    ///     the image source for the selected recipe
    /// </returns>
    public ImageSource? GetRecipeImage()
    {
        return this.recipeDetailViewModel.RecipeInfo!.Image;
    }

    /// <summary>Gets the recipe title.</summary>
    /// <returns>
    ///     the recipe titile
    /// </returns>
    public string? GetRecipeTitle()
    {
        if (string.IsNullOrEmpty(this.foundRecipeViewModel.SelectedRecipeTitle))
        {
            return this.browseRecipesViewModel.SelectedRecipeTitle;
        }

        return this.foundRecipeViewModel.SelectedRecipeTitle;
    }

    /// <summary>Removes an ingredient from a user's pantry.</summary>
    /// <param name="ingredientName">Name of the ingredient.</param>
    /// <param name="ingredientAmount">The ingredient amount.</param>
    /// <param name="isPantry"> indicates if the ingredient is a pantry item or grocery item</param>
    /// <returns>true if successful false otherwise</returns>
    public Task<bool> RemoveIngredient(string? ingredientName, int ingredientAmount, bool isPantry)
    {
        if (isPantry)
        {
            return this.pantryViewModel.RemoveIngredient(ingredientName, ingredientAmount, this.ClientToSet);
        }

        return this.groceryListViewModel.RemoveIngredient(ingredientName, ingredientAmount, this.ClientToSet);
    }

    /// <summary>Gets a list of recipes agnostic of users pantry items.</summary>
    /// <returns>
    ///     a list of recipe names that match the search and filtering criteria
    /// </returns>
    public List<string?> BrowseRecipes()
    {
        var recipeNames = new List<string?>();
        foreach (var recipe in this.browseRecipesViewModel.BrowseRecipes(this.ClientToSet, this.Userid)!)
        {
            recipeNames.Add(recipe.Title);
        }

        return recipeNames;
    }

    /// <summary>Increments the users browsing page.</summary>
    public void IncrementPage()
    {
        if (this.browseRecipesViewModel.CurrentPage < this.browseRecipesViewModel.NumberOfPages)
        {
            this.browseRecipesViewModel.CurrentPage++;
        }
    }

    /// <summary>Resets the browse Page fields and properties so the display is accurate.</summary>
    public void ResetBrowse()
    {
        this.browseRecipesViewModel.CurrentPage = 0;
        this.browseRecipesViewModel.NumberOfPages = 0;
        this.browseRecipesViewModel.NumberOfRecipes = 0;
        this.browseRecipesViewModel.AppliedDietType = "";
        this.browseRecipesViewModel.AppliedRecipeType = "";
        this.browseRecipesViewModel.AppliedCuisineType = "";
        this.browseRecipesViewModel.SearchName = "";
    }

    /// <summary>Decrements the current browsing page.</summary>
    public void DecrementPage()
    {
        if (this.browseRecipesViewModel.CurrentPage > 0)
        {
            this.browseRecipesViewModel.CurrentPage--;
        }
    }

    /// <summary>Gets the recipe types from the database.</summary>
    /// <returns>
    ///     a collection of meal types that user can filter recipes with
    /// </returns>
    public List<string> GetRecipeTypes()
    {
        var recipeTypes = new List<string>();
        var connection = new HttpClientConnection();
        var retrieved = connection.GetRecipeTypes(this.ClientToSet);
        recipeTypes.AddRange(retrieved.Result);
        recipeTypes.Add("");
        return recipeTypes;
    }

    /// <summary>Gets the diet types from the database.</summary>
    /// <returns>
    ///     a collection of diet types that user can filter recipes with
    /// </returns>
    public List<string> GetDietTypes()
    {
        var dietTypes = new List<string>();
        var connection = new HttpClientConnection();
        var retrieved = connection.GetDietTypes(this.ClientToSet);
        dietTypes.AddRange(retrieved.Result);
        dietTypes.Add("");
        return dietTypes;
    }

    /// <summary>Sets the filters to browse recipes with.</summary>
    /// <param name="typeComboboxText">The recipe type combobox text.</param>
    /// <param name="dietComboboxText">The diet combobox text.</param>
    /// <param name="cuisinesString"></param>
    public void SetFilters(string typeComboboxText, string dietComboboxText, string? cuisinesString)
    {
        this.browseRecipesViewModel.AppliedRecipeType = typeComboboxText;
        this.browseRecipesViewModel.AppliedDietType = dietComboboxText;
        this.browseRecipesViewModel.AppliedCuisineType = cuisinesString;
    }

    /// <summary>Gets the browsing page information.</summary>
    /// <returns>
    ///     the current page and the total number of pages
    /// </returns>
    public string GetPageInfo()
    {
        var numberOfPages = this.browseRecipesViewModel.NumberOfPages + 1;
        return this.browseRecipesViewModel.CurrentPage + 1 + " of " + numberOfPages;
    }

    /// <summary>Sets the name the user is searching with.</summary>
    /// <param name="name">The name entered by the user.</param>
    public void SetSearchName(string name)
    {
        this.browseRecipesViewModel.SearchName = name;
    }

    /// <summary>Gets the name the user searched with.</summary>
    /// <returns>
    ///     The name entered by the user
    /// </returns>
    public string? GetSearchName()
    {
        return this.browseRecipesViewModel.SearchName;
    }

    /// <summary>Gets the filters the user applied.</summary>
    /// <returns>
    ///     A tuple holding strings that indicate the filters used by the user when browsing
    /// </returns>
    public Tuple<string, string> GetFilters()
    {
        return new Tuple<string, string>(this.browseRecipesViewModel.AppliedRecipeType,
            this.browseRecipesViewModel.AppliedDietType);
    }

    /// <summary>Gets the plans.</summary>
    public async Task GetPlans()
    {
        await this.mealPlanViewModel.GetMealPlans(this.Userid, this.ClientToSet);
    }

    /// <summary>Gets the meals planned on the current day for the current week.</summary>
    /// <param name="currentWeek">if set to <c>true</c> [current week] else next week.</param>
    /// <param name="dayOfWeek">The day of week to get the meals for.</param>
    /// <returns>
    ///     a dictionary of meal types and recipe titles.
    /// </returns>
    public Dictionary<MealType, string?> GetMealPlan(bool currentWeek, DayOfWeek dayOfWeek)
    {
        var mealTitles = new Dictionary<MealType, string?>();
        var meals = this.mealPlanViewModel.GetMealForDay(dayOfWeek, currentWeek);

        mealTitles.Add(MealType.Breakfast, "");
        mealTitles.Add(MealType.Lunch, "");
        mealTitles.Add(MealType.Dinner, "");

        foreach (var meal in meals)
        {
            if (meal!.Recipe != null)
            {
                mealTitles[meal.MealType] = meal.Recipe.Title;
            }
        }

        return mealTitles;
    }

    /// <summary>Gets the current week bool.</summary>
    /// <returns>
    ///     true if user has current week selected, false otherwise
    /// </returns>
    public bool GetCurrentWeek()
    {
        return this.mealPlanViewModel.CurrentWeek;
    }

    /// <summary>Adds recipe to meal plan.</summary>
    /// <param name="current">indicates if the current week is selected.</param>
    public void AddToMealPlan(bool? current)
    {
        this.mealPlanViewModel.AddToPlan(this.recipeDetailViewModel.CurrentRecipe!, this.PlanTypeAndDateToAdd!.Item1,
            this.PlanTypeAndDateToAdd.Item2, this.ClientToSet, current);
    }

    /// <summary>Removes the meal from plan.</summary>
    /// <param name="mealToRemove">The name of the meal to remove.</param>
    /// <param name="dayOfWeek">The day of week.</param>
    /// <param name="mealType">Type of the meal.</param>
    public void RemoveMealFromPlan(string? mealToRemove, DayOfWeek dayOfWeek, MealType mealType)
    {
        this.mealPlanViewModel.RemoveMealFromPlan(this.ClientToSet, mealToRemove, dayOfWeek, mealType);
    }

    /// <summary>Gets the date range for the current week.</summary>
    /// <param name="currentWeek">if set to <c>true</c> [current week] false next week.</param>
    /// <returns>
    ///     the sunday-saturday date range
    /// </returns>
    public DateOnly GetPlanDate(bool currentWeek)
    {
        return this.mealPlanViewModel.GetDate(currentWeek);
    }

    /// <summary>Checks if theres a recipe planned for the given week type and day</summary>
    /// <param name="mealType">Type of the meal.</param>
    /// <param name="day">The day.</param>
    /// <param name="current">if set to <c>true</c> [current].</param>
    /// <returns>
    ///     true if a recipe exists for the given info false otherwise
    /// </returns>
    public bool MealPlanContainsRecipe(MealType mealType, DayOfWeek day, bool current)
    {
        this.GetPlans().ConfigureAwait(true);
        return this.mealPlanViewModel.CheckForRecipe(mealType, day);
    }

    /// <summary>Updates the plan if it is being overwritten.</summary>
    /// <param name="current">indicates the current week selected.</param>
    public void UpdatePlan(bool? current)
    {
        if (this.recipeDetailViewModel.CurrentRecipe != null && this.PlanTypeAndDateToAdd != null)
        {
            this.mealPlanViewModel.UpdatePlan(this.recipeDetailViewModel.CurrentRecipe, this.PlanTypeAndDateToAdd.Item1,
                this.PlanTypeAndDateToAdd.Item2, this.ClientToSet, current);
        }
    }

    /// <summary>Gets the cuisine types from the data base to filter with.</summary>
    /// <returns>
    ///     a collection of cuisine types
    /// </returns>
    public List<string>? GetCuisineTypes()
    {
        var cuisineTypes = new List<string>();
        var connection = new HttpClientConnection();
        var retrieved = connection.GetCuisineTypes(this.ClientToSet);
        cuisineTypes.AddRange(retrieved.Result);
        cuisineTypes.Add("");
        return cuisineTypes;
    }

    /// <summary>Gets the users grocery list.</summary>
    /// <returns>
    ///   a collection of groceryListItems to display to user
    /// </returns>
    public async Task<List<GroceryListItem>> GetGroceryList()
    {
        var groceryList = new List<GroceryListItem>();

        groceryList.AddRange((await this.groceryListViewModel.GetGroceryList(this.Userid, this.ClientToSet))!);
        return groceryList;
    }

    /// <summary>
    /// Adds the needed ingredients to complete the users meal plan to the grocery list.
    /// </summary>
    public async Task AddedNeededGroceries()
    {
        var recipeIds = new List<int>();
        recipeIds.AddRange(this.mealPlanViewModel.NextWeekPlan!.Recipes);
        recipeIds.AddRange(this.mealPlanViewModel.FirstWeekPlan!.Recipes);
        await this.groceryListViewModel.AddNeededGroceriesToList(recipeIds, this.Userid,
            this.ClientToSet);
    }

    /// <summary>Purchases the ingredients the user has selected.</summary>
    /// <param name="purchasedItems">The items the user has marked as purchased and their quantities.</param>
    public void PurchaseIngredients(Dictionary<string, int> purchasedItems)
    {
        this.groceryListViewModel.BuyGroceryItems(purchasedItems, this.Userid, this.ClientToSet);
    }

    /// <summary>
    /// Prepares the meal by adjusting the ingredients the user has in pantry.
    /// Deducts the ingredient quantities required to complete the recipe selected
    /// </summary>
    public void PrepareMeal()
    {
        var connection = new HttpClientConnection();
        var toRemove = this.recipeDetailViewModel.RecipeInfo!.Ingredients!;
        connection.UseIngredientsFromList(toRemove, this.Userid, this.ClientToSet);
    }

    /// <summary>Clears the users grocery list.</summary>
    public async Task ClearGroceryList()
    {
        await this.groceryListViewModel.ClearGroceryList(this.Userid, this.ClientToSet);
    }

    #endregion
}