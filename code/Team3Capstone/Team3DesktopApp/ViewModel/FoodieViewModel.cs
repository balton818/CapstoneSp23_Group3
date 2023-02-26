using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;


namespace Team3DesktopApp.ViewModel;

/// <summary>
///     the view model class that wraps the other view models
/// </summary>
public class FoodieViewModel
{
    #region Data members
    public HttpClient ClientToSet { get; set; }
    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:7278/api/") };
    private readonly FoundRecipeViewModel foundRecipeViewModel;
    private readonly LoginViewModel loginViewModel;
    private readonly RegistrationViewModel registrationViewModel;
    private readonly RecipeDetailViewModel recipeDetailViewModel;
    private readonly PantryViewModel pantryViewModel;
    private readonly BrowseRecipesViewModel browseRecipesViewModel;


    #endregion

    #region Properties

    /// <summary>Gets or sets the userid.</summary>
    /// <value>The userid.</value>
    public int Userid { get; set; }

    /// <summary>Gets or sets the pantry.</summary>
    /// <value>The pantry.</value>
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



    /// <summary>Logins the specified username.</summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <returns>
    ///     <br />
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

    /// <summary>Gets the pantry.</summary>
    /// <returns>
    ///     <br />
    /// </returns>
    public async Task<List<PantryItem>> GetPantry()
    {
        var recipes = new List<PantryItem>();

        recipes.AddRange(await this.pantryViewModel.GetPantry(this.Userid, this.ClientToSet));
        return recipes;
    }

    /// <summary>Gets the recipes.</summary>
    /// <returns>
    ///     <br />
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
    ///     <br />
    /// </returns>
    public async Task<int> RegisterUser(string username, string password, string email, string firstName,
        string lastName)
    {
        return await this.registrationViewModel.RegisterAsync(username, password, email, firstName, lastName, this.ClientToSet);
    }

    /// <summary>Adds the ingredient.</summary>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
    public async Task<PantryItem> AddIngredient(string name, int quantity, string unit)
    {
        return await this.pantryViewModel.AddIngredient(this.Userid, name, quantity, this.ClientToSet, unit);

    }

    /// <summary>Recipes the detail nav.</summary>
    /// <param name="recipeName">Name of the recipe.</param>
    public async Task<RecipeInformation> RecipeDetailNavFound(string recipeName)
    {
        this.foundRecipeViewModel.SelectedRecipeTitle = recipeName;
        foreach (var recipe in this.foundRecipeViewModel.Recipes)
        {
            if (recipe.Title.Equals(recipeName) && recipe.Id != null)
            {
                await this.recipeDetailViewModel.RecipeDetailNav((int)recipe.Id, this.ClientToSet);
            }
        }
        return this.recipeDetailViewModel.RecipeInfo;
    }

    public async Task<RecipeInformation> RecipeDetailNavBrowse(string recipeName)
    {
        this.browseRecipesViewModel.SelectedRecipeTitle = recipeName;
        foreach (var recipe in this.browseRecipesViewModel.Recipes)
        {
            if (recipe.Title.Equals(recipeName) && recipe.Id != null)
            {
                await this.recipeDetailViewModel.RecipeDetailNav((int)recipe.Id, this.ClientToSet);
            }
        }
        return this.recipeDetailViewModel.RecipeInfo;
    }

    /// <summary>Edits the ingredient.</summary>
    /// <param name="ingredientName">Name of the ingredient.</param>
    /// <param name="ingredientAmount">The ingredient amount.</param>
    public async Task<PantryItem> EditIngredient(string ingredientName, int ingredientAmount)
    {
        return await this.pantryViewModel.EditIngredientAmount(ingredientName, ingredientAmount, this.ClientToSet);
    }

    /// <summary>Gets the recipe ingredients.</summary>
    /// <returns>
    ///     <br />
    /// </returns>
    public List<string> GetRecipeIngredients()
    {
        var ingredients = new List<string>();
        foreach (var ingredient in this.recipeDetailViewModel.RecipeInfo.Ingredients)
        {
            ingredients.Add(ingredient.ingredientName + " " + ingredient.quantity);
        }

        return ingredients;
    }

    /// <summary>Gets the recipe steps.</summary>
    /// <returns>
    ///     <br />
    /// </returns>
    public List<string> GetRecipeSteps()
    {
        var steps = new List<string>();
        foreach (var step in this.recipeDetailViewModel.RecipeInfo.Steps)
        {
            steps.Add(step.stepNumber + ". " + step.instructions);
        }

        return steps;
    }

    /// <summary>Gets the recipe title.</summary>
    /// <returns>
    ///     <br />
    /// </returns>
    public string GetRecipeTitle()
    {
        return this.foundRecipeViewModel.SelectedRecipeTitle;
    }

    #endregion

    public Task<bool> RemoveIngredient(string ingredientName, int ingredientAmount)
    {
        return this.pantryViewModel.RemoveIngredient(ingredientName, ingredientAmount, this.ClientToSet);

    }

    public List<string> BrowseRecipes()
    {
        var recipeNames = new List<string>();
        foreach (var recipe in this.browseRecipesViewModel.BrowseRecipes(this.ClientToSet))
        {
            recipeNames.Add(recipe.Title);
        }

        return recipeNames;
    }
    public void IncrementPage()
    {
        if (this.browseRecipesViewModel.CurrentPage < this.browseRecipesViewModel.NumberOfPages)
        {
            this.browseRecipesViewModel.CurrentPage++;
        }


    }

    public void ResetBrowse()
    {
        this.browseRecipesViewModel.CurrentPage = 0;
        this.browseRecipesViewModel.NumberOfPages = 0;
        this.browseRecipesViewModel.NumberOfRecipes = 0;
    }

    public void DecrementPage()
    {
        if (this.browseRecipesViewModel.CurrentPage > 0)
        {
            this.browseRecipesViewModel.CurrentPage--;
        }

    }

    public List<string> GetRecipeTypes()
    {
        var recipeTypes = new List<string>();
        var connection = new HttpClientConnection();
        var retrieved = connection.GetRecipeTypes(this.ClientToSet);
        recipeTypes.AddRange(retrieved.Result);
        recipeTypes.Add("");
        return recipeTypes;
    }

    public List<string> GetDietTypes()
    {
        var dietTypes = new List<string>();
        var connection = new HttpClientConnection();
        var retrieved = connection.GetDietTypes(this.ClientToSet);
        dietTypes.AddRange(retrieved.Result);
        dietTypes.Add("");
        return dietTypes;
    }

    public void SetFilters(string typeComboboxText, string dietComboboxText)
    {
        this.browseRecipesViewModel.AppliedRecipeType = typeComboboxText;
        this.browseRecipesViewModel.AppliedDietType = dietComboboxText;
    }

    public string GetPageInfo()
    {
        return this.browseRecipesViewModel.CurrentPage + " of " + this.browseRecipesViewModel.NumberOfPages;
    }
}
