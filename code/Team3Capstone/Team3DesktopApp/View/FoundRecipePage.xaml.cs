using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for FoundRecipePage.xaml
    /// </summary>
    public partial class FoundRecipePage : Page
    {
        private FoodieViewModel? ViewModel { get; set; }

        public FoundRecipePage(FoodieViewModel viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
            this.navMenu.FoodViewModel = this.ViewModel;
            this.navMenu.current = this;
            this.recipeListBox.ItemsSource = this.ViewModel.GetRecipes();

        }

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.RecipeDetailNav(this.recipeListBox.SelectedItem.ToString());
            NavButton navButton = (NavButton)sender;
            this.NavigateToPage(navButton.NavUri);

        }

        private void NavigateToPage(string navUri)
        {
            if (NavigationService != null)
            {
                this.ViewModel.NavigateToPage(navUri, NavigationService);
            }
        }

    }
}
