﻿using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>Interaction logic for FoundRecipePage.xaml</summary>
[ExcludeFromCodeCoverage]
public partial class FoundRecipePage : Page
{
    #region Properties

    private FoodieViewModel ViewModel { get; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="FoundRecipePage" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    public FoundRecipePage(FoodieViewModel viewModel)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
        this.navMenu.FoodViewModel = this.ViewModel;
        this.navMenu.Current = this;
        this.recipeListBox.ItemsSource = this.ViewModel.GetRecipes();
    }

    #endregion

    #region Methods

    private void ViewDetail_Click(object sender, RoutedEventArgs e)
    {
        if (this.recipeListBox.SelectedItem == null)
        {
            MessageBox.Show("Please select a recipe to view");
            return;
        }
        _ = this.ViewModel.RecipeDetailNavFound(this.recipeListBox.SelectedItem.ToString());
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

    #endregion

    private void BrowseAllRecipes_OnClick(object sender, RoutedEventArgs e)
    {
        var navButton = (NavButton)sender;
        this.ViewModel.ResetBrowse();
        this.navigateToPage(navButton.NavUri);
    }
}