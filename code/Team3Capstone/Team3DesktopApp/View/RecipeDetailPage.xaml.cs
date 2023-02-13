using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for RecipeDetailPage.xaml
    /// </summary>
    public partial class RecipeDetailPage : Page
    {
        private FoodieViewModel? ViewModel { get; set; }

        public RecipeDetailPage(FoodieViewModel viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
            this.navMenu.FoodViewModel = this.ViewModel;
            this.navMenu.current = this;
            this.setPage();
        }

        private void setPage()
        {
            this.ingredientList.ItemsSource = this.ViewModel.GetRecipeIngredients();
            this.stepsList.ItemsSource = this.ViewModel.GetRecipeSteps();
            this.recipeTitleTextBlock.Text = this.ViewModel.GetRecipeTitle();
        }
    }
}
