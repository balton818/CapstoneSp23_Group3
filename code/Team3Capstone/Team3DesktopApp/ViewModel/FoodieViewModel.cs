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

    public void RecipeDetailNav(Uri navButtonNavUri, string recipeName)
    {
        if (navButtonNavUri == null)
        {
            throw new ArgumentNullException(nameof(navButtonNavUri));
        }

        this.foundRecipeViewModel.RecipeDetailNav(navButtonNavUri, recipeName);
    }

    public void NavigatePage(Uri navButtonNavUri)
    {

        NavigationService.Navigate(navButtonNavUri);
    }

}