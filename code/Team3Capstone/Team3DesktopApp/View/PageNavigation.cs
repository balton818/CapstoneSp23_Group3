using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{

    /// <summary>
    ///   <br />
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PageNavigation
    {
        /// <summary>Gets or sets the view model.</summary>
        /// <value>The Main view model.</value>
        public FoodieViewModel ViewModel { get; set; }

        private readonly string LoginUri = "/View/LoginPage.xaml";
        private readonly string RegistrationUri = "/View/RegistrationPage.xaml";
        private readonly string FoundRecipeUri = "/View/RecipePage.xaml";
        private readonly string RecipeDetailUri = "/View/RecipeDetailPage.xaml";
        private readonly string BrowseRecipesUri = "/View/BrowseRecipesPage.xaml";

        /// <summary>Initializes a new instance of the <see cref="PageNavigation" /> class.</summary>
        /// <param name="viewModel">The view model.</param>
        public PageNavigation(FoodieViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }
        /// <summary>Navigates to page This is the logic that the custom navButton uses to move to each page.</summary>
        /// <param name="navUri">The nav URI for the page.</param>
        /// <param name="navigationService">The navigation service of the current page.</param>
        public void NavigateToPage(string navUri, NavigationService navigationService)
        {
            if (navUri.Equals(this.LoginUri))
            {
                var loginPage = new LoginPage();
                navigationService.Navigate(loginPage);
            }
            else if (navUri.Equals(this.FoundRecipeUri))
            {
                var foundRecipePage = new FoundRecipePage(this.ViewModel);
                navigationService.Navigate(foundRecipePage);
            }
            else if (navUri.Equals(this.RegistrationUri))
            {
                var registrationPage = new RegistrationPage(this.ViewModel);
                navigationService.Navigate(registrationPage);
            }
            else if (navUri.Equals(this.RecipeDetailUri))
            {
                var recipeDetailPage = new RecipeDetailPage(this.ViewModel);

                navigationService.Navigate(recipeDetailPage);
            }
            else if (navUri.Equals(this.BrowseRecipesUri))
            {
                var browseRecipesPage = new BrowseRecipesPage(this.ViewModel);
                navigationService.Navigate(browseRecipesPage);
            }
            else
            {
                var pantryPage = new PantryPage(this.ViewModel);
                navigationService.Navigate(pantryPage);
            }
        }
    }
}
