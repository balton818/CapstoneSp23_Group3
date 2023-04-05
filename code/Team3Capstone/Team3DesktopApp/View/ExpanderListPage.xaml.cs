using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     Interaction logic for PantryPage.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class ExpanderListPage
{
    #region Properties

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The view model.</value>
    public FoodieViewModel? ViewModel { get; set; }

    public bool IsGrocery { get; set; }

    private List<PantryItem> pantryList;
    private List<GroceryListItem> groceryList;

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="PantryPage" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    public ExpanderListPage(FoodieViewModel? viewModel, bool isPantry)
    {
        this.InitializeComponent();
        //this.measurementCombo.SelectedIndex = 0;
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
            !string.IsNullOrEmpty(this.quantityTextBox.Text) && this.quantityTextBox.Text.All(Char.IsDigit))
        {
            if (StylizedMessageBox.ShowBox(
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
                this.errorText.Visibility = Visibility.Hidden;
                this.ingredientNameTextBox.Text = "";
                this.quantityTextBox.Text = "";
            }
        }
        else
        {
            this.errorText.Visibility = Visibility.Visible;
        }
    }

    private void buildView()
    {
        var expanders = new List<IngredientExpander>();
        if (this.ViewModel != null)
        {
            if (!this.IsGrocery)
            {
                this.headerTitle.Text = "My Pantry";
                this.purchaseSelectedButton.Visibility = Visibility.Hidden;
                this.buildPantryExpanders(expanders);
            }
            else
            {
                this.headerTitle.Text = "Grocery List";
                this.purchaseSelectedButton.Visibility = Visibility.Visible;
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
                current.UnitId.ToString(), this.ViewModel, false);
            ingredientExpander.Current = this;
            expanders.Add(ingredientExpander);
        }
    }

    #endregion

    private void PurchaseSelectedButton_OnClick(object sender, RoutedEventArgs e)
    {
        List<string> purchasedItems = new List<string>();
        foreach (IngredientExpander expander in this.expanderListBox.Items)
        {

            if (expander.SelectedForPurchase)
            {
                purchasedItems.Add(expander.IngredientName!);
            }
        }
        this.ViewModel.PurchaseIngredients(purchasedItems);
        this.navigateToPage("Grocery");
    }

    private void navigateToPage(string navUri)
    {
        if (NavigationService != null)
        {
            var navigate = new PageNavigation(this.ViewModel);
            navigate.NavigateToPage(navUri, NavigationService);
        }
    }
}

