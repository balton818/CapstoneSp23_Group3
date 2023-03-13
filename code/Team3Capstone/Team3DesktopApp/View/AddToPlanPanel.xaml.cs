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
    /// Interaction logic for AddToPlanPanel.xaml
    /// </summary>
    public partial class AddToPlanPanel : UserControl
    {
        public FoodieViewModel ViewModel { get; set; }
        public AddToPlanPanel()
        {
            this.InitializeComponent();
        }

        private void addToPlanClick(object sender, RoutedEventArgs e)
        {
            var mealType = this.parseType();
            var day = this.parseDay();
            var current = this.parseCurrentWeek();
            string messageBoxText = "This recipe is already in your meal plan. Would you like to Overwrite it?";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            string caption = "Overwrite?";
            MessageBoxResult result;
            if (mealType == null || day == null)
            {
                MessageBox.Show("Please select a meal type, day, and week.");
            }
            else if (this.ViewModel.MealPlanContainsRecipe(mealType.Value, day.Value, current.Value))
            {

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    var typeAndDate = new Tuple<DayOfWeek?, MealType?>(day, mealType);

                    this.ViewModel.PlanTypeAndDateToAdd = typeAndDate;
                    this.ViewModel.UpdatePlan(current);
                    Grid parentGrid = (Grid)this.Parent;
                    parentGrid.Visibility = Visibility.Hidden;
                }

            }
            else
            {
                var typeAndDate = new Tuple<DayOfWeek?, MealType?>(day, mealType);

                this.ViewModel.PlanTypeAndDateToAdd = typeAndDate;
                this.ViewModel.AddToMealPlan(current);
                Grid parentGrid = (Grid)this.Parent;
                parentGrid.Visibility = Visibility.Hidden;
            }
        }

        private bool? parseCurrentWeek()
        {
            if (this.currentWeekButton.IsChecked == true)
            {
                return true;
            }
            else if (this.nextWeekButton.IsChecked == true)
            {
                return false;
            }

            return null;
        }

        private DayOfWeek? parseDay()
        {
            if (this.mondayButton.IsChecked == true)
            {
                return DayOfWeek.Monday;
            }
            if (this.tuesdayButton.IsChecked == true)
            {
                return DayOfWeek.Tuesday;
            }
            if (this.wednesdayButton.IsChecked == true)
            {
                return DayOfWeek.Wednesday;
            }
            if (this.thursdayButton.IsChecked == true)
            {
                return DayOfWeek.Thursday;
            }
            if (this.fridayButton.IsChecked == true)
            {
                return DayOfWeek.Friday;
            }
            if (this.saturdayButton.IsChecked == true)
            {
                return DayOfWeek.Saturday;
            }
            if (this.sundayButton.IsChecked == true)
            {
                return DayOfWeek.Sunday;
            }

            return null;

        }

        private MealType? parseType()
        {

            if (this.breakfastButton.IsChecked == true)
            {
                return MealType.BREAKFAST;
            }
            if (this.lunchButton.IsChecked == true)
            {
                return MealType.LUNCH;
            }
            if (this.dinnerButton.IsChecked == true)
            {
                return MealType.DINNER;
            }

            return null;
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            Grid parentGrid = (Grid)this.Parent;
            parentGrid.Visibility = Visibility.Hidden;
        }

        public void SetOptions(bool currentWeek, DayOfWeek? day, MealType? mealType)
        {
            if (currentWeek)
            {
                this.currentWeekButton.IsChecked = true;
            }
            else
            {
                this.nextWeekButton.IsChecked = true;
            }
            this.setDay(day);
            this.setMealType(mealType);
        }

        private void setMealType(MealType? mealType)
        {
            switch (mealType)
            {
                case MealType.BREAKFAST:
                    this.breakfastButton.IsChecked = true;
                    break;
                case MealType.LUNCH:
                    this.lunchButton.IsChecked = true;
                    break;
                case MealType.DINNER:
                    this.dinnerButton.IsChecked = true;
                    break;
            }
        }

        private void setDay(DayOfWeek? day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    this.mondayButton.IsChecked = true;
                    break;
                case DayOfWeek.Tuesday:
                    this.tuesdayButton.IsChecked = true;
                    break;
                case DayOfWeek.Wednesday:
                    this.wednesdayButton.IsChecked = true;
                    break;
                case DayOfWeek.Thursday:
                    this.thursdayButton.IsChecked = true;
                    break;
                case DayOfWeek.Friday:
                    this.fridayButton.IsChecked = true;
                    break;
                case DayOfWeek.Saturday:
                    this.saturdayButton.IsChecked = true;
                    break;
                case DayOfWeek.Sunday:
                    this.sundayButton.IsChecked = true;
                    break;
            }
        }
    }
}
