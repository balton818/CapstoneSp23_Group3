using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Linq;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for ingredientExpander.xaml
    /// </summary>
    public partial class IngredientExpander : UserControl
    {

        public string IngredientName
        {
            get;
            set;
        }
        public int IngredientAmount
        {
            get;
            set;
        }

        public FoodieViewModel ViewModel { get; set; }

        public IngredientExpander(String name, int amount, FoodieViewModel viewModel)
        {
            this.InitializeComponent();
            this.IngredientName = name;
            this.IngredientAmount = amount;
            this.ViewModel = viewModel;



        }

        private void PlusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.IngredientAmount++; ;
            this.ViewModel.EditIngredient(this.IngredientName, this.IngredientAmount);

        }

        private void MinusButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.IngredientAmount--;
            this.ViewModel.EditIngredient(this.IngredientName, this.IngredientAmount);
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
