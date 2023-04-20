using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     Interaction logic for PantryPage.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class ExpanderListPage
{
    #region Data members

    private List<PantryItem> pantryList = null!;
    private List<GroceryListItem> groceryList = null!;

    #endregion

    #region Properties

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The view model.</value>
    public FoodieViewModel? ViewModel { get; set; }

    /// <summary>Gets or sets a value indicating whether this instance is a grocery list</summary>
    /// <value>
    ///   <c>true</c> if this instance is a grocery list; otherwise, <c>false</c>.</value>
    public bool IsGrocery { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="ExpanderListPage" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    /// <param name="isPantry"> indicates if this page is for a pantry or grocery list</param>
    public ExpanderListPage(FoodieViewModel? viewModel, bool isPantry)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
        this.IsGrocery = !isPantry;
        this.navMenu.FoodViewModel = this.ViewModel;
        this.navMenu.Current = this;
        this.buildView();
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Handles the OnClick event of the AddIngredientButton control.
    ///     Adds an ingredient to the pantry.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private async void AddIngredientButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(this.ingredientNameTextBox.Text) &&
            !string.IsNullOrEmpty(this.quantityTextBox.Text) && this.quantityTextBox.Text.All(char.IsDigit))
        {
            if (this.ingredientOnList())
            {
                StylizedMessageBox.ShowBox(
                    this.ingredientNameTextBox.Text + " already on list edit the quantity that exists.",
                    "Ingredient Addition");
            }
            else if (StylizedMessageBox.ShowBox(
                    "Confirm addition of " + this.ingredientNameTextBox.Text + " " + this.quantityTextBox.Text + " " +
                    this.measurementCombo.Text + "?",
                    "Ingredient Addition") == "1")
            {
                var foodieViewModel = this.ViewModel;
                if (foodieViewModel != null && !this.IsGrocery)
                {
                    await foodieViewModel.AddPantryIngredient(this.ingredientNameTextBox.Text,
                        int.Parse(this.quantityTextBox.Text), this.measurementCombo.Text);
                }
                else
                {
                    await foodieViewModel.AddGroceryIngredient(this.ingredientNameTextBox.Text,
                        int.Parse(this.quantityTextBox.Text), this.measurementCombo.Text);
                }

                this.buildView();
                this.amountErrorText.Visibility = Visibility.Hidden;
                this.nameErrorText.Visibility = Visibility.Hidden;
            }
        }
        if (string.IsNullOrEmpty(this.ingredientNameTextBox.Text))
        {
            this.nameErrorText.Visibility = Visibility.Visible;
        }
        if (string.IsNullOrEmpty(this.quantityTextBox.Text) || !this.quantityTextBox.Text.All(char.IsDigit))
        {
            this.amountErrorText.Visibility = Visibility.Visible;
        }
        this.ingredientNameTextBox.Text = "";
        this.quantityTextBox.Text = "";
    }

    private bool ingredientOnList()
    {
        if (this.IsGrocery)
        {
            return this.groceryList.Any(ingredient => ingredient.IngredientName == this.ingredientNameTextBox.Text);
        }
        return this.pantryList.Any(ingredient => ingredient.IngredientName == this.ingredientNameTextBox.Text);
    }

    private void buildView()
    {
        var expanders = new List<IngredientExpander>();
        if (this.ViewModel != null)
        {
            if (!this.IsGrocery)
            {
                this.selectAllButton.Visibility = Visibility.Hidden;
                this.headerTitle.Text = "My Pantry";
                this.purchaseSelectedButton.Visibility = Visibility.Hidden;
                this.clearGroceryListButton.Visibility = Visibility.Hidden;
                this.buildPantryExpanders(expanders);
            }
            else
            {
                this.selectAllButton.Visibility = Visibility.Visible;
                this.headerTitle.Text = "Groceries";
                this.purchaseSelectedButton.Visibility = Visibility.Visible;
                this.clearGroceryListButton.Visibility = Visibility.Visible;
                this.buildGroceryExpanders(expanders);
            }
        }

        this.expanderListBox.ItemsSource = expanders;
    }

    private void buildGroceryExpanders(List<IngredientExpander> expanders)
    {
        this.groceryList = this.ViewModel.GetGroceryList().Result;
        foreach (var current in this.groceryList)
        {
            var ingredientExpander = new IngredientExpander(current.IngredientName, current.Quantity,
                current.UnitId.ToString(), this.ViewModel, true);
            ingredientExpander.Current = this;
            expanders.Add(ingredientExpander);
        }
    }

    private void buildPantryExpanders(List<IngredientExpander> expanders)
    {
        this.pantryList = this.ViewModel.GetPantry().Result;
        foreach (var current in this.pantryList)
        {
            var ingredientExpander = new IngredientExpander(current.IngredientName, current.Quantity,
                current.UnitId.ToString(), this.ViewModel, false)
            {
                Current = this
            };
            expanders.Add(ingredientExpander);
        }
    }

    private void PurchaseSelectedButton_OnClick(object sender, RoutedEventArgs e)
    {
        var purchasedItems = new Dictionary<string, int>();
        foreach (IngredientExpander expander in this.expanderListBox.Items)
        {
            if (expander.SelectedForPurchase)
            {
                purchasedItems.Add(expander.IngredientName!, expander.IngredientAmount);
            }
        }
        var result = StylizedMessageBox.ShowBox("Confirm purchase of selected groceries?",
            "Purchase " + purchasedItems.Count + " Ingredients");
        if (result == "1")
        {
            this.ViewModel.PurchaseIngredients(purchasedItems);
            this.navigateToPage("Pantry");
        }
    }

    private void navigateToPage(string navUri)
    {
        if (NavigationService != null)
        {
            var navigate = new PageNavigation(this.ViewModel);
            navigate.NavigateToPage(navUri, NavigationService);
        }
    }

    private void ClearGroceryListButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (StylizedMessageBox.ShowBox("Are you Sure you want to Clear the Grocery List?", "Grocery List Clear") == "1")
        {
            this.ViewModel.ClearGroceryList();
            this.navigateToPage("Grocery");
        }
    }

    private void SelectAllButton_OnClick(object sender, RoutedEventArgs e)
    {
        foreach (IngredientExpander expander in this.expanderListBox.Items)
        {
            expander.SelectedForPurchaseBox();

        }
    }

    #endregion


}