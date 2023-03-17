using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>Interaction logic for ingredientExpander.xaml</summary>
[ExcludeFromCodeCoverage]
public partial class IngredientExpander
{
    #region Properties

    /// <summary>Gets or sets the name of the ingredient.</summary>
    /// <value>The name of the ingredient.</value>
    public string? IngredientName { get; set; }

    /// <summary>Gets or sets the ingredient amount.</summary>
    /// <value>The ingredient amount.</value>
    public int IngredientAmount { get; set; }

    /// <summary>Gets or sets the ingredient unit.</summary>
    /// <value>The measurement unit for the ingredient.</value>
    public string IngredientUnit { get; set; }

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The view model.</value>
    public FoodieViewModel? ViewModel { get; set; }

    /// <summary>Gets or sets the current Page.</summary>
    /// <value>The current Page the expander exists on.</value>
    public Page? Current { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="IngredientExpander" /> class.</summary>
    /// <param name="name">The name of the ingredient.</param>
    /// <param name="amount">The amount of the ingredient.</param>
    /// <param name="measure"> The unit of measure for the ingredient</param>
    /// <param name="viewModel">The view model.</param>
    public IngredientExpander(string? name, int amount, string measure, FoodieViewModel? viewModel)
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
        var foodieViewModel = this.ViewModel;
        if (foodieViewModel != null)
        {
            await foodieViewModel.EditIngredient(this.IngredientName, this.IngredientAmount);
        }

        this.navigateToPage("View/PantryPage.xaml");
    }

    private async void MinusButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (this.IngredientAmount > 0)
        {
            this.IngredientAmount--;
            var foodieViewModel = this.ViewModel;
            if (foodieViewModel != null)
            {
                await foodieViewModel.EditIngredient(this.IngredientName, this.IngredientAmount);
            }

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
            if (this.ViewModel != null)
            {
                this.ViewModel.RemoveIngredient(this.IngredientName, this.IngredientAmount);
                this.navigateToPage("View/PantryPage.xaml");
            }
        }
    }

    private void navigateToPage(string navUri)
    {
        var navigate = new PageNavigation(this.ViewModel);
        var currentNavigationService = this.Current!.NavigationService;
        if (currentNavigationService != null)
        {
            navigate.NavigateToPage(navUri, currentNavigationService);
        }
    }

    #endregion
}