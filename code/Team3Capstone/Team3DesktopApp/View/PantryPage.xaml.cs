using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for PantryPage.xaml
    /// </summary>
    public partial class PantryPage : Page, INotifyPropertyChanged
    {
        public FoodieViewModel ViewModel { get; set; }
        private IngredientExpander? expander;
        private int selectedQuantity;
        private string selectedName;
        public IngredientExpander? Expander
        {
            get => (IngredientExpander)this.pantryListBox.SelectedItem;
            set
            {
                this.expander = this.Expander;
                this.selectedQuantity = this.expander.IngredientAmount;
                if (this.selectedName.Equals(this.expander.IngredientName) && this.selectedQuantity != this.expander.IngredientAmount)
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

        public PantryPage(FoodieViewModel viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
            this.navMenu.FoodViewModel = this.ViewModel;
            this.navMenu.current = this;
            this.buildView();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void AddIngredientButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ingredientNameTextBox.Text) && !string.IsNullOrEmpty(this.quantityTextBox.Text))
            {
                this.ViewModel.AddIngredient(this.ingredientNameTextBox.Text, Int32.Parse(this.quantityTextBox.Text));
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

            List<IngredientExpander> pantry = new List<IngredientExpander>();
            foreach (PantryItem current in this.ViewModel.getPantry())
            {
                IngredientExpander ingredientExpander = new IngredientExpander(current.IngredientName, current.Quantity, this.ViewModel);
                pantry.Add(ingredientExpander);

            }
            this.pantryListBox.ItemsSource = pantry;
        }
    }
}
