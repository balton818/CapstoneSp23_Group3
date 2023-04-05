using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     Interaction logic for RecipeDetailPage.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class RecipeDetailPage
{
    #region Properties

    private FoodieViewModel? ViewModel { get; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="RecipeDetailPage" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    public RecipeDetailPage(FoodieViewModel? viewModel)
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
        var foodieViewModel = this.ViewModel;
        if (foodieViewModel != null)
        {
            this.ingredientList.ItemsSource = foodieViewModel.GetRecipeIngredients();
            this.stepsList.ItemsSource = foodieViewModel.GetRecipeSteps();
            this.recipeTitleTextBlock.Text = foodieViewModel.GetRecipeTitle();
            this.recipeImage.Source = foodieViewModel.GetRecipeImage();
        }
    }

    private void addToPlan_OnClick(object sender, RoutedEventArgs e)
    {
        this.addToPlanePanel.Visibility = Visibility.Visible;
        if (this.ViewModel != null && this.ViewModel.PlanTypeAndDateToAdd != null)
        {
            this.addPanel.SetOptions(this.ViewModel.GetCurrentWeek(), this.ViewModel.PlanTypeAndDateToAdd.Item1,
                this.ViewModel.PlanTypeAndDateToAdd.Item2);
        }
    }

    private void backToPlan_OnClick(object sender, RoutedEventArgs e)
    {
        if (NavigationService != null)
        {
            NavigationService.GoBack();
        }
    }

    #endregion

    private void mealPrepared_OnClick(object sender, RoutedEventArgs e)
    {
        if (StylizedMessageBox.ShowBox(
                "Are you sure you have prepared this meal? This will remove the ingredient quantities from your pantry.",
                "Prepare Meal?") == "1")
        {
            this.ViewModel.PrepareMeal();
        }
    }
}