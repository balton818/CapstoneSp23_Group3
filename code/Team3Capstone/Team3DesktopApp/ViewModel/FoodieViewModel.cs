using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Team3DesktopApp.Model;
using Team3DesktopApp.View;

namespace Team3DesktopApp.ViewModel;

/// <summary>
///     <br />
/// </summary>
public class FoodieViewModel
{
    #region Data members

    private readonly FoundRecipeViewModel foundRecipeViewModel;
    private readonly LoginViewModel loginViewModel;
    private readonly RegistrationViewModel registrationViewModel;
    private readonly RecipeDetailViewModel recipeDetailViewModel;
    private readonly PantryViewModel pantryViewModel;

    private readonly string LoginUri = "/View/LoginPage.xaml";
    private readonly string RegistrationUri = "/View/RegistrationPage.xaml";
    private readonly string FoundRecipeUri = "/View/RecipePage.xaml";
    private readonly string RecipeDetailUri = "/View/RecipeDetailPage.xaml";

    #endregion

    #region Properties

    /// <summary>Gets or sets the userid.</summary>
    /// <value>The userid.</value>
    public int Userid { get; set; }

    /// <summary>Gets or sets the pantry.</summary>
    /// <value>The pantry.</value>
    public List<PantryItem> Pantry { get; set; }

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
    }

    #endregion

    #region Methods

    /// <summary>Navigates to page.</summary>
    /// <param name="navUri">The nav URI.</param>
    /// <param name="navigationService">The navigation service.</param>
    public void NavigateToPage(string navUri, NavigationService navigationService)
    {
        if (navUri.Equals(this.LoginUri))
        {
            var loginPage = new LoginPage();
            navigationService.Navigate(loginPage);
        }
        else if (navUri.Equals(this.FoundRecipeUri))
        {
            var foundRecipePage = new FoundRecipePage(this);
            navigationService.Navigate(foundRecipePage);
        }
        else if (navUri.Equals(this.RegistrationUri))
        {
            var registrationPage = new RegistrationPage(this);
            navigationService.Navigate(registrationPage);
        }
        else if (navUri.Equals(this.RecipeDetailUri))
        {
            var recipeDetailPage = new RecipeDetailPage(this);

            navigationService.Navigate(recipeDetailPage);
        }
        else
        {
            var pantryPage = new PantryPage(this);
            navigationService.Navigate(pantryPage);
        }
    }

    /// <summary>Logins the specified username.</summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <returns>
    ///     <br />
    /// </returns>
    public async Task<int> Login(string username, string password)
    {
        var result = await this.loginViewModel.LoginAsync(username, password);
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
    public List<PantryItem> getPantry()
    {
        var recipes = new List<PantryItem>();

        recipes.AddRange(this.pantryViewModel.getPantry(this.Userid));
        return recipes;
    }

    /// <summary>Gets the recipes.</summary>
    /// <returns>
    ///     <br />
    /// </returns>
    public List<string> GetRecipes()
    {
        var recipeNames = new List<string>();
        foreach (var recipe in this.foundRecipeViewModel.GetRecipes(this.Userid))
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
        return await this.registrationViewModel.RegisterAsync(username, password, email, firstName, lastName);
    }

    /// <summary>Adds the ingredient.</summary>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
    public async void AddIngredient(string name, int quantity)
    {
        await this.pantryViewModel.addIngredient(this.Userid, name, quantity);
    }

    /// <summary>Recipes the detail nav.</summary>
    /// <param name="recipeName">Name of the recipe.</param>
    public void RecipeDetailNav(string recipeName)
    {
        this.foundRecipeViewModel.SelectedRecipeTitle = recipeName;
        foreach (var recipe in this.foundRecipeViewModel.Recipes)
        {
            if (recipe.Title.Equals(recipeName) && recipe.Id != null)
            {
                this.recipeDetailViewModel.RecipeDetailNav((int)recipe.Id);
            }
        }
    }

    /// <summary>Edits the ingredient.</summary>
    /// <param name="ingredientName">Name of the ingredient.</param>
    /// <param name="ingredientAmount">The ingredient amount.</param>
    public async void EditIngredient(string ingredientName, int ingredientAmount)
    {
        await this.pantryViewModel.editIngredientAmount(ingredientName, ingredientAmount);
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
            ingredients.Add(ingredient.name + " " + ingredient.quanitiy);
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

    public void RemoveIngredient(string ingredientName, int ingredientAmount)
    {
        this.pantryViewModel.RemoveIngredient(ingredientName, ingredientAmount);

    }
}