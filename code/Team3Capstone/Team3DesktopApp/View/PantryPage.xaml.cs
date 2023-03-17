using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     Interaction logic for PantryPage.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class PantryPage
{
    #region Properties

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The view model.</value>
    public FoodieViewModel? ViewModel { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="PantryPage" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    public PantryPage(FoodieViewModel? viewModel)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
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
        if (!string.IsNullOrEmpty(this.ingredientNameTextBox.Text) && !string.IsNullOrEmpty(this.quantityTextBox.Text))
        {
            if (MessageBox.Show(
                    "Confirm addition of " + this.ingredientNameTextBox.Text + " " + this.quantityTextBox.Text + " " +
                    this.measurementCombo.Text + "?",
                    "Ingredient Addition",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var foodieViewModel = this.ViewModel;
                if (foodieViewModel != null)
                {
                    await foodieViewModel.AddIngredient(this.ingredientNameTextBox.Text,
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
        var pantry = new List<IngredientExpander>();
        if (this.ViewModel != null)
        {
            var currentContents = this.ViewModel.GetPantry().Result;
            foreach (var current in currentContents)
            {
                var ingredientExpander = new IngredientExpander(current.IngredientName, current.Quantity,
                    current.UnitId.ToString(), this.ViewModel);
                ingredientExpander.Current = this;
                pantry.Add(ingredientExpander);
            }
        }

        this.pantryListBox.ItemsSource = pantry;
    }

    #endregion
}