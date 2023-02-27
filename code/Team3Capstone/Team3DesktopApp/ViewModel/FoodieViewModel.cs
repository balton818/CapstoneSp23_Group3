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

    #endregion

    #region Properties

    public HttpClient ClientToSet { get; set; }

    /// <summary>Gets or sets the userid.</summary>
    /// <value>The currently logged in users ID.</value>
    public int Userid { get; set; }

    /// <summary>Gets or sets the pantry.</summary>
    /// <value>The currently logged in user's pantry.</value>
    public List<PantryItem>? Pantry { get; set; }

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
        this.ClientToSet = Client;
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

        recipes.AddRange(await this.pantryViewModel.GetPantry(this.Userid, this.ClientToSet));
        return recipes;
    }

    /// <summary>Gets the recipes.</summary>
    /// <returns>
    ///     A list of recipes that the user can currently cook based on their pantry ingredients
    /// </returns>
    public List<string> GetRecipes()
    {
        var recipeNames = new List<string>();
        foreach (var recipe in this.foundRecipeViewModel.GetRecipes(this.Userid, this.ClientToSet))
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
    public async Task<PantryItem> AddIngredient(string name, int quantity, string unit)
    {
        return await this.pantryViewModel.AddIngredient(this.Userid, name, quantity, this.ClientToSet, unit);
    }

    /// <summary>The logic to display the recipe details upon user navigation from the Found recipes page</summary>
    /// <param name="recipeName">Name of the recipe the user wants details for.</param>
    /// <returns>
    ///     The recipe information if successful ie the steps, ingredients etc. null otherwise
    /// </returns>
    public async Task<RecipeInformation> RecipeDetailNavFound(string recipeName)
    {
        this.foundRecipeViewModel.SelectedRecipeTitle = recipeName;
        return await this.recipeInformation(recipeName, this.foundRecipeViewModel.Recipes);
    }

    /// <summary>The logic to display the recipe details upon user navigation from the Browse recipes page</summary>
    /// <param name="recipeName">Name of the recipe the user wants details for.</param>
    /// <returns>
    ///     The recipe information if successful ie the steps, ingredients etc. null otherwise
    /// </returns>
    public async Task<RecipeInformation> RecipeDetailNavBrowse(string recipeName)
    {
        this.browseRecipesViewModel.SelectedRecipeTitle = recipeName;
        return await this.recipeInformation(recipeName, this.browseRecipesViewModel.Recipes);
    }

    private async Task<RecipeInformation> recipeInformation(string recipeName, List<Recipe> recipes)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.Title.Equals(recipeName) && recipe.Id != null)
            {
                await this.recipeDetailViewModel.RecipeDetailNav((int)recipe.Id, this.ClientToSet);
            }
        }

        return this.recipeDetailViewModel.RecipeInfo;
    }

    /// <summary>Edits the Pantry ingredient in the users pantry.</summary>
    /// <param name="ingredientName">Name of the ingredient.</param>
    /// <param name="ingredientAmount">The ingredient amount.</param>
    /// <returns> the edited item if successful null otherwise </returns>
    public async Task<PantryItem> EditIngredient(string ingredientName, int ingredientAmount)
    {
        return await this.pantryViewModel.EditIngredientAmount(ingredientName, ingredientAmount, this.ClientToSet);
    }

    /// <summary>Gets the recipe ingredients form the recipe information.</summary>
    /// <returns>
    ///     A list of the ingredients required to make a recipe includes quantity name and unit of measure
    /// </returns>
    public List<string> GetRecipeIngredients()
    {
        var ingredients = new List<string>();
        foreach (var ingredient in this.recipeDetailViewModel.RecipeInfo.Ingredients)
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
        if (this.recipeDetailViewModel.RecipeInfo.Steps.Count == 1)
        {
            return this.splitSteps();
        }

        foreach (var step in this.recipeDetailViewModel.RecipeInfo.Steps)
        {
            steps.Add(step.stepNumber + ". " + step.instructions);
        }

        return steps;
    }

    private List<string> splitSteps()
    {
        var count = 1;
        var splitToSteps = new List<string>();
        var steps = new List<string>();
        splitToSteps.AddRange(this.recipeDetailViewModel.RecipeInfo.Steps[0].instructions.Split('.'));
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
    public ImageSource GetRecipeImage()
    {
        return this.recipeDetailViewModel.RecipeInfo.Image;
    }

    /// <summary>Gets the recipe title.</summary>
    /// <returns>
    ///     the recipe titile
    /// </returns>
    public string GetRecipeTitle()
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
    /// <returns>
    ///     true if successful false otherwise
    /// </returns>
    public Task<bool> RemoveIngredient(string ingredientName, int ingredientAmount)
    {
        return this.pantryViewModel.RemoveIngredient(ingredientName, ingredientAmount, this.ClientToSet);
    }

    /// <summary>Gets a list of recipes agnostic of users pantry items.</summary>
    /// <returns>
    ///     a list of recipe names that match the search and filtering criteria
    /// </returns>
    public List<string> BrowseRecipes()
    {
        var recipeNames = new List<string>();
        foreach (var recipe in this.browseRecipesViewModel.BrowseRecipes(this.ClientToSet, this.Userid))
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
    public void SetFilters(string typeComboboxText, string dietComboboxText)
    {
        this.browseRecipesViewModel.AppliedRecipeType = typeComboboxText;
        this.browseRecipesViewModel.AppliedDietType = dietComboboxText;
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

    #endregion
}