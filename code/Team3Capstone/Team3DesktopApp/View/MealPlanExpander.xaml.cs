using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View
{
    /// <summary>
    /// Interaction logic for MealPlanExpander.xaml
    /// </summary>
    public partial class MealPlanExpander : UserControl
    {
        /// <summary>Gets or sets the name of the breakfast recipe.</summary>
        /// <value>The name of the breakfast recipe.</value>
        public string BreakfastName { get; set; }

        public Page Current { get; set; }
        /// <summary>Gets or sets the name of the breakfast recipe.</summary>
        /// <value>The name of the breakfast recipe.</value>
        public string LunchName { get; set; }

        /// <summary>Gets or sets the name of the breakfast recipe.</summary>
        /// <value>The name of the breakfast recipe.</value>
        public string DinnerName { get; set; }

        public string Date { get; set; }

        /// <summary>Gets or sets the view model.</summary>
        /// <value>The view model.</value>
        public FoodieViewModel ViewModel { get; set; }

        public string PlannedLabel { get; set; } = "Planned - ";

        public MealPlanExpander(FoodieViewModel viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
            this.DataContext = this;
        }

        private void navigateToPage(string navUri)
        {

            PageNavigation navigate = new PageNavigation(this.ViewModel);
            var currentNavigationService = this.Current.NavigationService;
            if (currentNavigationService != null)
            {
                navigate.NavigateToPage(navUri, currentNavigationService);
            }

        }

        private void addMealClick(object sender, RoutedEventArgs e)
        {
            NavButton? buttonPressed = sender as NavButton;
            if (buttonPressed!.Name.Equals(this.addBreakfastButton.Name))
            {
                this.ViewModel.PlanTypeAndDateToAdd = new Tuple<string, string>(this.Date, "Breakfast");
            }
            else if (buttonPressed.Name.Equals(this.addLunchButton.Name))
            {
                this.ViewModel.PlanTypeAndDateToAdd = new Tuple<string, string>(this.Date, "Lunch");
            }
            else if (buttonPressed.Name.Equals(this.addDinnerButton.Name))
            {
                this.ViewModel.PlanTypeAndDateToAdd = new Tuple<string, string>(this.Date, "Dinner");
            }


            this.navigateToPage(buttonPressed.NavUri);
        }
    }
}
