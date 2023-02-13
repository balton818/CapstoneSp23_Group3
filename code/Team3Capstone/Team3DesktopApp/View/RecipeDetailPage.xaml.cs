using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;
[ExcludeFromCodeCoverage]
/// <summary>
///     Interaction logic for RecipeDetailPage.xaml
/// </summary>
public partial class RecipeDetailPage : Page
{
    #region Properties

    private FoodieViewModel? ViewModel { get; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="RecipeDetailPage" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    public RecipeDetailPage(FoodieViewModel viewModel)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
        this.navMenu.FoodViewModel = this.ViewModel;
        this.navMenu.current = this;
        this.setPage();
    }

    #endregion

    #region Methods

    private void setPage()
    {
        this.ingredientList.ItemsSource = this.ViewModel.GetRecipeIngredients();
        this.stepsList.ItemsSource = this.ViewModel.GetRecipeSteps();
        this.recipeTitleTextBlock.Text = this.ViewModel.GetRecipeTitle();
    }

    #endregion
}