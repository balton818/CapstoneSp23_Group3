using System.ComponentModel;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

    /// <summary>Indicates if expander item is selected for purchase.</summary>
    /// <value>
    ///   <c>true</c> if [selected for purchase]; otherwise, <c>false</c>.</value>
    public bool SelectedForPurchase { get; set; }

    /// <summary>Indicates if expander contains grocery item or pantry item.</summary>
    /// <value>
    ///   <c>true</c> if this instance is grocery item; otherwise, <c>false</c>.</value>
    public bool IsGrocery { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="IngredientExpander" /> class.</summary>
    /// <param name="name">The name of the ingredient.</param>
    /// <param name="amount">The amount of the ingredient.</param>
    /// <param name="measure">The unit of measure for the ingredient</param>
    /// <param name="viewModel">The view model.</param>
    /// <param name="isGrocery"> indicates if expander is for grocery item</param>
    public IngredientExpander(string? name, int amount, string measure, FoodieViewModel? viewModel, bool isGrocery)
    {
        this.InitializeComponent();
        this.IngredientName = name;
        this.IngredientAmount = amount;
        this.IngredientUnit = measure;
        this.ViewModel = viewModel;
        this.IsGrocery = isGrocery;
    }

    #endregion

    #region Methods

    private async void PlusButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.IngredientAmount++;
        var foodieViewModel = this.ViewModel;
        if (foodieViewModel != null && !this.IsGrocery)
        {
            await foodieViewModel.EditPantryIngredient(this.IngredientName, this.IngredientAmount);
        }
        else
        {
            await foodieViewModel.EditGroceryIngredient(this.IngredientName, this.IngredientAmount);
        }

        this.expanderNavigation();
    }

    private void expanderNavigation()
    {
        if (!this.IsGrocery)
        {
            this.navigateToPage("View/PantryPage.xaml");
        }
        else
        {
            this.navigateToPage("Grocery");
        }
    }

    private async void MinusButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (this.IngredientAmount > 0)
        {
            this.IngredientAmount--;
            var foodieViewModel = this.ViewModel;
            if (foodieViewModel != null && !this.IsGrocery)
            {
                await foodieViewModel.EditPantryIngredient(this.IngredientName, this.IngredientAmount);
            }
            else
            {
                await foodieViewModel.EditGroceryIngredient(this.IngredientName, this.IngredientAmount);
            }

            this.expanderNavigation();
        }
    }

    private async void RemoveButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (StylizedMessageBox.ShowBox(
                "Confirm removal of " + this.IngredientName + " ? ",
                "Ingredient Removal") == "1")
        {
            if (this.ViewModel != null)
            {
                await this.ViewModel.RemoveIngredient(this.IngredientName, this.IngredientAmount, !this.IsGrocery);
                this.expanderNavigation();
            }
        }
    }

    private void quantity_TextChanged(object sender, EventArgs e)
    {
        TextBox boxChanged = (TextBox)sender;
        var foodieViewModel = this.ViewModel;
        TextBox quantity = (TextBox)sender;
        int parsed;
        bool isNumeric = int.TryParse(quantity.Text, out parsed);

        if (!isNumeric && quantity.Text != string.Empty)
        {
            StylizedMessageBox.ShowBox(
                "Error, you have entered a non numeric quantity.",
                "Error");
            quantity.Background = Brushes.Red;
        }
        else if (boxChanged.IsFocused)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = (Color)ColorConverter.ConvertFromString("#30323d");
            quantity.Background = brush;
            this.IngredientAmount = parsed;
            if (foodieViewModel != null && !this.IsGrocery)
            {
                foodieViewModel.EditPantryIngredient(this.IngredientName, this.IngredientAmount);
            }
            else
            {
                foodieViewModel.EditGroceryIngredient(this.IngredientName, this.IngredientAmount);
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