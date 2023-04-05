using System.Diagnostics.CodeAnalysis;
using System.Windows.Navigation;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     The page navigation class dictates how page navigation is handled
/// </summary>
[ExcludeFromCodeCoverage]
public class PageNavigation
{
    #region Data members

    private readonly string loginUri = "/View/LoginPage.xaml";
    private readonly string registrationUri = "/View/RegistrationPage.xaml";
    private readonly string foundRecipeUri = "/View/RecipePage.xaml";
    private readonly string recipeDetailUri = "/View/RecipeDetailPage.xaml";
    private readonly string browseRecipesUri = "/View/BrowseRecipesPage.xaml";
    private readonly string mealPlanUri = "/View/MealPlanPage.xaml";
    private readonly string groceryUri = "Grocery";

    #endregion

    #region Properties

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The Main view model.</value>
    public FoodieViewModel? ViewModel { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="PageNavigation" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    public PageNavigation(FoodieViewModel? viewModel)
    {
        this.ViewModel = viewModel;
    }

    #endregion

    #region Methods

    /// <summary>Navigates to page This is the logic that the custom navButton uses to move to each page.</summary>
    /// <param name="navUri">The nav URI for the page.</param>
    /// <param name="navigationService">The navigation service of the current page.</param>
    public void NavigateToPage(string navUri, NavigationService navigationService)
    {
        if (navUri.Equals(this.loginUri))
        {
            var loginPage = new LoginPage();
            navigationService.Navigate(loginPage);
        }
        else if (navUri.Equals(this.foundRecipeUri) && this.ViewModel != null)
        {
            this.ViewModel.PlanTypeAndDateToAdd = null;
            var foundRecipePage = new FoundRecipePage(this.ViewModel);
            navigationService.Navigate(foundRecipePage);
        }
        else if (navUri.Equals(this.registrationUri))
        {
            var registrationPage = new RegistrationPage(this.ViewModel);
            navigationService.Navigate(registrationPage);
        }
        else if (navUri.Equals(this.recipeDetailUri))
        {
            var recipeDetailPage = new RecipeDetailPage(this.ViewModel);

            navigationService.Navigate(recipeDetailPage);
        }
        else if (navUri.Equals(this.browseRecipesUri))
        {
            var browseRecipesPage = new BrowseRecipesPage(this.ViewModel);
            navigationService.Navigate(browseRecipesPage);
        }
        else if (navUri.Equals(this.mealPlanUri) && this.ViewModel != null)
        {
            var mealPlanPage = new MealPlanPage(this.ViewModel);
            this.ViewModel.PlanTypeAndDateToAdd = null;
            navigationService.Navigate(mealPlanPage);
        }
        else if (navUri.Equals(this.groceryUri) && this.ViewModel != null)
        {
            var groceryPage = new ExpanderListPage(this.ViewModel, false);

            this.ViewModel.PlanTypeAndDateToAdd = null;

            navigationService.Navigate(groceryPage);
        }
        else if (this.ViewModel != null)
        {
            var pantryPage = new ExpanderListPage(this.ViewModel, true);
            this.ViewModel.PlanTypeAndDateToAdd = null;

            navigationService.Navigate(pantryPage);
        }
    }

    #endregion
}