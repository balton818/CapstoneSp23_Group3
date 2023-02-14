using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Navigation;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Team3DesktopApp.Model;
using Team3DesktopApp.View;

namespace Team3DesktopApp.ViewModel;

public class FoodieViewModel
{
    public int Userid { get; set; }
    public List<PantryItem> Pantry { get; set; }
    private FoundRecipeViewModel foundRecipeViewModel;
    private LoginViewModel loginViewModel;
    private RegistrationViewModel registrationViewModel;
    private RecipeDetailViewModel recipeDetailViewModel;
    private PantryViewModel pantryViewModel;

    private string LoginUri = "/View/LoginPage.xaml";
    private string RegistrationUri = "/View/RegistrationPage.xaml";
    private string FoundRecipeUri = "/View/RecipePage.xaml";
    private string RecipeDetailUri = "/View/RecipeDetailPage.xaml";


    public FoodieViewModel()
    {
        this.recipeDetailViewModel = new RecipeDetailViewModel();
        this.foundRecipeViewModel = new FoundRecipeViewModel();
        this.loginViewModel = new LoginViewModel();
        this.registrationViewModel = new RegistrationViewModel();
        this.pantryViewModel = new PantryViewModel();
    }


    public void NavigateToPage(string navUri, NavigationService navigationService)
    {

        if (navUri.Equals(this.LoginUri))
        {
            LoginPage loginPage = new LoginPage();
            navigationService.Navigate(loginPage);
        }
        else if (navUri.Equals(this.FoundRecipeUri))
        {
            FoundRecipePage foundRecipePage = new FoundRecipePage(this);
            navigationService.Navigate(foundRecipePage);
        }
        else if (navUri.Equals(this.RegistrationUri))
        {
            RegistrationPage registrationPage = new RegistrationPage(this);
            navigationService.Navigate(registrationPage);
        }
        else if (navUri.Equals(this.RecipeDetailUri))
        {
            RecipeDetailPage recipeDetailPage = new RecipeDetailPage(this);

            navigationService.Navigate(recipeDetailPage);
        }
        else
        {
            PantryPage pantryPage = new PantryPage(this);
            navigationService.Navigate(pantryPage);
        }


    }

    public async Task<int> Login(string username, string password)
    {
        int result = await this.loginViewModel.LoginAsync(username, password);
        if (result >= 0)
        {
            this.Userid = result;

        }

        return result;

    }

    public List<PantryItem> getPantry()
    {
        List<PantryItem> recipes = new List<PantryItem>();

        recipes.AddRange(this.pantryViewModel.getPantry(this.Userid));
        return recipes;
    }

    public List<string> GetRecipes()
    {
        List<string> recipeNames = new List<string>();
        foreach (var recipe in this.foundRecipeViewModel.GetRecipes(this.Userid))
        {
            recipeNames.Add(recipe.Title);
        }

        return recipeNames;
    }

    public async Task<int> RegisterUser(string username, string password, string email, string firstName, string lastName)
    {
        return await this.registrationViewModel.RegisterAsync(username, password, email, firstName, lastName);
    }

    public async void AddIngredient(string name, int quantity)
    {
        await this.pantryViewModel.addIngredient(this.Userid, name, quantity);

    }
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
    public async void EditIngredient(string ingredientName, int ingredientAmount)
    {
        await this.pantryViewModel.editIngredientAmount(ingredientName, ingredientAmount);
    }

    public List<string> GetRecipeIngredients()
    {
        List<string> ingredients = new List<string>();
        foreach (var ingredient in this.recipeDetailViewModel.RecipeInfo.Ingredients)
        {
            ingredients.Add(ingredient.name + " " + ingredient.quanitiy);
        }

        return ingredients;

    }

    public List<string> GetRecipeSteps()
    {
        List<string> steps = new List<string>();
        foreach (var step in this.recipeDetailViewModel.RecipeInfo.Steps)
        {
            steps.Add(step.stepNumber + ". " + step.instructions);

        }

        return steps;
    }


    public string GetRecipeTitle()
    {
        return this.foundRecipeViewModel.SelectedRecipeTitle;
    }
}