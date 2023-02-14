using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>Interaction logic for FoundRecipePage.xaml</summary>
[ExcludeFromCodeCoverage]
public partial class FoundRecipePage : Page
{
    #region Properties

    private FoodieViewModel? ViewModel { get; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="FoundRecipePage" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    public FoundRecipePage(FoodieViewModel viewModel)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
        this.navMenu.FoodViewModel = this.ViewModel;
        this.navMenu.current = this;
        this.recipeListBox.ItemsSource = this.ViewModel.GetRecipes();
    }

    #endregion

    #region Methods

    private void ViewDetail_Click(object sender, RoutedEventArgs e)
    {
        this.ViewModel.RecipeDetailNav(this.recipeListBox.SelectedItem.ToString());
        var navButton = (NavButton)sender;
        this.NavigateToPage(navButton.NavUri);
    }

    private void NavigateToPage(string navUri)
    {
        if (NavigationService != null)
        {
            this.ViewModel.NavigateToPage(navUri, NavigationService);
        }
    }

    #endregion
}