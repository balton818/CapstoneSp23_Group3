using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;
[ExcludeFromCodeCoverage]
/// <summary>
///     Interaction logic for PantryPage.xaml
/// </summary>
public partial class PantryPage : Page, INotifyPropertyChanged
{
    #region Data members

    private IngredientExpander? expander;
    private int selectedQuantity;
    private string selectedName;

    #endregion

    #region Properties

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The view model.</value>
    public FoodieViewModel ViewModel { get; set; }

    /// <summary>Gets or sets the expander.</summary>
    /// <value>The expander.</value>
    public IngredientExpander? Expander
    {
        get => (IngredientExpander)this.pantryListBox.SelectedItem;
        set
        {
            this.expander = this.Expander;
            this.selectedQuantity = this.expander.IngredientAmount;
            if (this.selectedName.Equals(this.expander.IngredientName) &&
                this.selectedQuantity != this.expander.IngredientAmount)
            {
                this.OnPropertyChanged();
                this.selectedQuantity = this.expander.IngredientAmount;
                this.selectedName = this.expander.IngredientName;
            }
            else
            {
                this.selectedName = this.expander.IngredientName;
                this.selectedQuantity = this.expander.IngredientAmount;
            }
        }
    }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="PantryPage" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    public PantryPage(FoodieViewModel viewModel)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
        this.navMenu.FoodViewModel = this.ViewModel;
        this.navMenu.current = this;
        this.buildView();
    }

    #endregion

    #region Methods

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>Handles the OnClick event of the AddIngredientButton control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private void AddIngredientButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(this.ingredientNameTextBox.Text) && !string.IsNullOrEmpty(this.quantityTextBox.Text))
        {
            this.ViewModel.AddIngredient(this.ingredientNameTextBox.Text, int.Parse(this.quantityTextBox.Text));
            this.buildView();
            this.errorText.Visibility = Visibility.Hidden;
            this.ingredientNameTextBox.Text = "";
            this.quantityTextBox.Text = "";
        }
        else
        {
            this.errorText.Visibility = Visibility.Visible;
        }
    }

    protected void OnPropertyChanged()
    {
        this.buildView();
    }

    private void buildView()
    {
        var pantry = new List<IngredientExpander>();
        foreach (var current in this.ViewModel.getPantry())
        {
            var ingredientExpander = new IngredientExpander(current.IngredientName, current.Quantity, this.ViewModel);
            ingredientExpander.current = this;
            pantry.Add(ingredientExpander);
        }

        this.pantryListBox.ItemsSource = pantry;
    }

    #endregion
}