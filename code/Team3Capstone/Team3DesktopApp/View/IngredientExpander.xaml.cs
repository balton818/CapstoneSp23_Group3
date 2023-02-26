using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>Interaction logic for ingredientExpander.xaml</summary>
[ExcludeFromCodeCoverage]
public partial class IngredientExpander : UserControl
{
    #region Properties

    /// <summary>Gets or sets the name of the ingredient.</summary>
    /// <value>The name of the ingredient.</value>
    public string IngredientName { get; set; }

    /// <summary>Gets or sets the ingredient amount.</summary>
    /// <value>The ingredient amount.</value>
    public int IngredientAmount { get; set; }

    public string IngredientUnit { get; set; }

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The view model.</value>
    public FoodieViewModel ViewModel { get; set; }



    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="IngredientExpander" /> class.</summary>
    /// <param name="name">The name.</param>
    /// <param name="amount">The amount.</param>
    /// <param name="viewModel">The view model.</param>
    public IngredientExpander(string name, int amount, string measure, FoodieViewModel viewModel)
    {
        this.InitializeComponent();
        this.IngredientName = name;
        this.IngredientAmount = amount;
        this.IngredientUnit = measure;
        this.ViewModel = viewModel;
    }

    #endregion

    #region Methods

    private async void PlusButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.IngredientAmount++;
        await this.ViewModel.EditIngredient(this.IngredientName, this.IngredientAmount);

        this.navigateToPage("View/PantryPage.xaml");
    }

    private async void MinusButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (this.IngredientAmount > 0)
        {

            this.IngredientAmount--;
            await this.ViewModel.EditIngredient(this.IngredientName, this.IngredientAmount);
            this.navigateToPage("View/PantryPage.xaml");
        }

    }

    private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
    {

        if (MessageBox.Show("Confirm removal of " + this.IngredientName + "?",
                            "Ingredient Removal",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            this.ViewModel.RemoveIngredient(this.IngredientName, this.IngredientAmount);
            this.navigateToPage("View/PantryPage.xaml");
        }



    }

    private void navigateToPage(string navUri)
    {

        PageNavigation navigate = new PageNavigation(this.ViewModel);
        var currentNavigationService = this.current.NavigationService;
        if (currentNavigationService != null)
        {
            navigate.NavigateToPage(navUri, currentNavigationService);
        }

    }

    public Page current { get; set; }

    #endregion
}