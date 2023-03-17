using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>Interaction logic for BrowseRecipesPage.xaml</summary>
[ExcludeFromCodeCoverage]
public partial class BrowseRecipesPage
{
    #region Properties

    private FoodieViewModel? ViewModel { get; }
    private List<string>? RecipeTypes { get; set; }
    private List<string>? DietTypes { get; set; }
    private List<string>? CuisineTypes { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="BrowseRecipesPage" /> class.</summary>
    /// <param name="viewModel">The view model. This is the main view model that has a single instance so data is persistent</param>
    public BrowseRecipesPage(FoodieViewModel? viewModel)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
        this.navMenu.FoodViewModel = this.ViewModel;
        this.navMenu.Current = this;
        var foodieViewModel = this.ViewModel;
        this.browsePageSetup(foodieViewModel);
    }

    #endregion

    #region Methods

    private void browsePageSetup(FoodieViewModel? foodieViewModel)
    {
        if (foodieViewModel != null)
        {
            foodieViewModel.BrowseRecipes();
            this.recipeListBox.ItemsSource = foodieViewModel.BrowseRecipes();
            this.typeCombobox.ItemsSource = foodieViewModel.GetRecipeTypes();
            this.RecipeTypes = foodieViewModel.GetRecipeTypes();
            this.DietTypes = foodieViewModel.GetDietTypes();
            this.CuisineTypes = foodieViewModel.GetCuisineTypes();
            this.dietCombobox.ItemsSource = foodieViewModel.GetDietTypes();
            this.pageLabel.Text = foodieViewModel.GetPageInfo();
            this.typeList.ItemsSource = this.RecipeTypes;
            this.diestList.ItemsSource = this.DietTypes;
            this.cuisineList.ItemsSource = this.CuisineTypes;
            if (!string.IsNullOrEmpty(foodieViewModel.GetSearchName()))
            {
                this.searchNameTextBox.Text = foodieViewModel.GetSearchName();
                this.clearFilters.Visibility = Visibility.Visible;
            }

            var filters = foodieViewModel.GetFilters();

            if (!string.IsNullOrEmpty(filters.Item1) || !string.IsNullOrEmpty(filters.Item2))
            {
                this.typeCombobox.SelectedItem = filters.Item1;
                this.dietCombobox.SelectedItem = filters.Item2;
                this.clearFilters.Visibility = Visibility.Visible;
            }
        }
    }

    private void ViewDetail_Click(object sender, RoutedEventArgs e)
    {
        if (this.recipeListBox.SelectedItem == null)
        {
            MessageBox.Show("Please select a recipe to view");
            return;
        }

        _ = this.ViewModel!.RecipeDetailNavBrowse(this.recipeListBox.SelectedItem.ToString());
        var navButton = (NavButton)sender;
        this.navigateToPage(navButton.NavUri);
    }

    private void navigateToPage(string navUri)
    {
        if (NavigationService != null)
        {
            var navigate = new PageNavigation(this.ViewModel);
            navigate.NavigateToPage(navUri, NavigationService);
        }
    }

    private void SearchByName_Click(object sender, RoutedEventArgs e)
    {
        this.ViewModel!.SetSearchName(this.searchNameTextBox.Text);
        this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
        this.pageLabel.Text = this.ViewModel.GetPageInfo();
        this.clearFilters.Visibility = Visibility.Visible;
    }

    private void PrevPageButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.ViewModel!.DecrementPage();
        this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
        this.pageLabel.Text = this.ViewModel.GetPageInfo();
    }

    private void NextPageButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.ViewModel!.IncrementPage();
        this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
        this.pageLabel.Text = this.ViewModel.GetPageInfo();
    }

    private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
    {
        var typesToFilter = this.typeList.SelectedItems.Cast<string>().ToList();
        var dietsToFilter = this.diestList.SelectedItems.Cast<string>().ToList();
        var cuisinesToFilter = this.cuisineList.SelectedItems.Cast<string>().ToList();
        var typeString = string.Join(",", typesToFilter.Select(x => x.ToString()).ToArray());
        var dietsString = string.Join(",", dietsToFilter.Select(x => x.ToString()).ToArray());
        var cuisinesString = string.Join(",", cuisinesToFilter.Select(x => x.ToString()).ToArray());
        this.ViewModel!.SetFilters(typeString, dietsString, cuisinesString);
        this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
        this.pageLabel.Text = this.ViewModel.GetPageInfo();
        this.filterPanel.Visibility = Visibility.Hidden;
        this.clearFilters.Visibility = Visibility.Visible;
    }

    private void ClearFilters_OnClick(object sender, RoutedEventArgs e)
    {
        this.ViewModel!.ResetBrowse();
        this.clearFilters.Visibility = Visibility.Hidden;
        this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
        this.pageLabel.Text = this.ViewModel.GetPageInfo();
        this.searchNameTextBox.Text = string.Empty;
        this.typeList.SelectedItems.Clear();
        this.diestList.SelectedItems.Clear();
        this.cuisineList.SelectedItems.Clear();
    }

    private void ShowFilterResults_Click(object sender, RoutedEventArgs e)
    {
        this.filterPanel.Visibility = Visibility.Visible;
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.filterPanel.Visibility = Visibility.Hidden;
    }

    #endregion
}