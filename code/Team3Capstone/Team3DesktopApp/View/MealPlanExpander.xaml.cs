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
using Team3DesktopApp.Model;
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

        public MealPlanExpander(FoodieViewModel viewModel, DayOfWeek day)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
            this.DataContext = this;
            this.Date = day.ToString();
        }

        public MealPlanExpander()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Date = "Monday";
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

        public void DisableEnableBreakfast(bool active)
        {
            if (active)
            {
                this.addBreakfastButton.Visibility = Visibility.Hidden;
                this.breakfastDetailButton.Visibility = Visibility.Visible;
                this.breakfastRemoveButton.Visibility = Visibility.Visible;
            }
            else
            {
                this.addBreakfastButton.Visibility = Visibility.Visible;
                this.breakfastDetailButton.Visibility = Visibility.Hidden;
                this.breakfastRemoveButton.Visibility = Visibility.Hidden;
            }

        }

        public void DisableEnableLunch(bool active)
        {
            if (active)
            {
                this.addLunchButton.Visibility = Visibility.Hidden;
                this.LunchDetailButton.Visibility = Visibility.Visible;
                this.removeLunchButton.Visibility = Visibility.Visible;
            }
            else
            {
                this.addLunchButton.Visibility = Visibility.Visible;
                this.LunchDetailButton.Visibility = Visibility.Hidden;
                this.removeLunchButton.Visibility = Visibility.Hidden;
            }

        }

        public void DisableEnableDinner(bool active)
        {
            if (active)
            {
                this.addDinnerButton.Visibility = Visibility.Hidden;
                this.dinnerDetailButton.Visibility = Visibility.Visible;
                this.removeDinnerButton.Visibility = Visibility.Visible;
            }
            else
            {
                this.addDinnerButton.Visibility = Visibility.Visible;
                this.dinnerDetailButton.Visibility = Visibility.Hidden;
                this.removeDinnerButton.Visibility = Visibility.Hidden;
            }
        }
    }
}
