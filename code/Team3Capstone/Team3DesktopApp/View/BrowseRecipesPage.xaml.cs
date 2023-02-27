using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{
    /// <summary>Interaction logic for BrowseRecipesPage.xaml</summary>
    public partial class BrowseRecipesPage : Page
    {
        private FoodieViewModel ViewModel { get; }

        /// <summary>Initializes a new instance of the <see cref="BrowseRecipesPage" /> class.</summary>
        /// <param name="viewModel">The view model. This is the main view model that has a single instance so data is persistent</param>
        public BrowseRecipesPage(FoodieViewModel viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
            this.navMenu.FoodViewModel = this.ViewModel;
            this.navMenu.Current = this;
            this.ViewModel.BrowseRecipes();
            this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
            this.typeCombobox.ItemsSource = this.ViewModel.GetRecipeTypes();
            this.dietCombobox.ItemsSource = this.ViewModel.GetDietTypes();
            this.pageLabel.Text = this.ViewModel.GetPageInfo();
            if (!string.IsNullOrEmpty(this.ViewModel.GetSearchName()))
            {
                this.searchNameTextBox.Text = this.ViewModel.GetSearchName();
                this.clearFilters.Visibility = Visibility.Visible;
            }
            var filters = this.ViewModel.GetFilters();

            if (!string.IsNullOrEmpty(filters.Item1) || !string.IsNullOrEmpty(filters.Item2))
            {
                this.typeCombobox.SelectedItem = filters.Item1;
                this.dietCombobox.SelectedItem = filters.Item2;
                this.clearFilters.Visibility = Visibility.Visible;
            }


        }

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (this.recipeListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a recipe to view");
                return;
            }
            _ = this.ViewModel.RecipeDetailNavBrowse(this.recipeListBox.SelectedItem.ToString());
            var navButton = (NavButton)sender;
            this.navigateToPage(navButton.NavUri);
        }

        private void navigateToPage(string navUri)
        {
            if (NavigationService != null)
            {
                PageNavigation navigate = new PageNavigation(this.ViewModel);
                navigate.NavigateToPage(navUri, NavigationService);
            }
        }


        private void SearchByName_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.SetSearchName(this.searchNameTextBox.Text);
            this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
            this.pageLabel.Text = this.ViewModel.GetPageInfo();
            this.clearFilters.Visibility = Visibility.Visible;
        }

        private void PrevPageButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.DecrementPage();
            this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
            this.pageLabel.Text = this.ViewModel.GetPageInfo();
        }

        private void NextPageButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.IncrementPage();
            this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
            this.pageLabel.Text = this.ViewModel.GetPageInfo();
        }

        private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.SetFilters(this.typeCombobox.Text, this.dietCombobox.Text);
            this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
            this.pageLabel.Text = this.ViewModel.GetPageInfo();
            this.clearFilters.Visibility = Visibility.Visible;
        }

        private void ClearFilters_OnClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.ResetBrowse();
            this.clearFilters.Visibility = Visibility.Hidden;
            this.recipeListBox.ItemsSource = this.ViewModel.BrowseRecipes();
            this.pageLabel.Text = this.ViewModel.GetPageInfo();
            this.searchNameTextBox.Text = string.Empty;
            this.typeCombobox.SelectedItem = null;
            this.dietCombobox.SelectedItem = null;

        }
    }
}
