using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Team3DesktopApp.ViewModel;


namespace Team3DesktopApp.ViewModel;

public class FoodieViewModel
{
    private FoundRecipeViewModel foundRecipeViewModel = new FoundRecipeViewModel();
    private LoginViewModel loginViewModel = new LoginViewModel();
    private RegistrationViewModel registrationViewModel = new RegistrationViewModel();
    private RecipeDetailViewModel recipeDetailViewModel = new RecipeDetailViewModel();

    public void RecipeDetailNav(string recipeName)
    {

        this.recipeDetailViewModel.RecipeDetailNav(recipeName);
    }


}