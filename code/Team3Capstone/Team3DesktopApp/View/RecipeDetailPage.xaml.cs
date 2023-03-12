using System.Diagnostics.CodeAnalysis;
using System.Windows;
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
        this.navMenu.Current = this;
        this.addPanel.ViewModel = this.ViewModel;
        this.setPage();
    }

    #endregion

    #region Methods

    private void setPage()
    {
        this.ingredientList.ItemsSource = this.ViewModel.GetRecipeIngredients();
        this.stepsList.ItemsSource = this.ViewModel.GetRecipeSteps();
        this.recipeTitleTextBlock.Text = this.ViewModel.GetRecipeTitle();
        this.recipeImage.Source = this.ViewModel.GetRecipeImage();

    }

    #endregion

    private void addToPlan_OnClick(object sender, RoutedEventArgs e)
    {
        this.addToPlanePanel.Visibility = Visibility.Visible;
        if (this.ViewModel.PlanTypeAndDateToAdd != null)
        {
            this.addPanel.SetOptions(this.ViewModel.GetCurrentWeek(), this.ViewModel.PlanTypeAndDateToAdd.Item1, this.ViewModel.PlanTypeAndDateToAdd.Item2);

        }
    }

    private void backToPlan_OnClick(object sender, RoutedEventArgs e)
    {
        if (NavigationService != null)
        {
            NavigationService.GoBack();
        }
    }
}