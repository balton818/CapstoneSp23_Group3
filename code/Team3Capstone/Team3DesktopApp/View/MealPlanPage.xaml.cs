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
    /// Interaction logic for MealPlanPage.xaml
    /// </summary>
    public partial class MealPlanPage : Page
    {
        public FoodieViewModel ViewModel { get; set; }

        public bool CurrentWeek { get; private set; } = true;
        private List<MealPlanExpander> expanders;

        public MealPlanPage(FoodieViewModel viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
            this.navMenu.Current = this;
            this.navMenu.FoodViewModel = this.ViewModel;
            this.expanders = new List<MealPlanExpander>();
            this.buildView().ConfigureAwait(true);
            var date = this.ViewModel.getPlanDate(this.CurrentWeek);
            var nextDate = date.AddDays(6);
            this.planHeader.Text = " Meal Plan for " + date.Month + "/" + date.Day + " - " + nextDate.Month + "/" + nextDate.Day; ;

        }

        private async Task buildView()
        {
            await this.ViewModel.GetPlans();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                this.buildExpander(day);
            }

            this.planListBox.ItemsSource = this.expanders;
        }

        private void buildExpander(DayOfWeek day)
        {
            string plannedLabel = "";
            MealPlanExpander expander = new MealPlanExpander(this.ViewModel, day);
            expander.Date = day;
            expander.Current = this;
            Dictionary<MealType, string> titles = this.ViewModel.GetMealPlan(this.CurrentWeek, day);
            foreach (MealType meal in Enum.GetValues(typeof(MealType)))
            {
                if (!string.IsNullOrEmpty(titles[meal]))
                {
                    plannedLabel += meal.ToString().Substring(0, 1) + " ";
                    if (meal.Equals(MealType.BREAKFAST))
                    {
                        expander.BreakfastName = titles[meal];
                        expander.DisableEnableBreakfast(true);
                    }
                    else if (meal.Equals(MealType.LUNCH))
                    {
                        expander.LunchName = titles[meal];
                        expander.DisableEnableLunch(true);
                    }
                    else if (meal.Equals(MealType.DINNER))
                    {
                        expander.DinnerName = titles[meal];
                        expander.DisableEnableDinner(true);
                    }
                }
            }
            expander.PlannedLabel = plannedLabel;
            this.expanders.Add(expander);

        }


        private void viewOtherWeekClick(object sender, RoutedEventArgs e)
        {
            var date = new DateOnly();
            if (this.CurrentWeek)
            {
                this.CurrentWeek = false;
                this.nextWeekButton.Content = "Back to Current Week";
                date = this.ViewModel.getPlanDate(this.CurrentWeek);
            }
            else
            {
                this.CurrentWeek = true;
                this.nextWeekButton.Content = "View Next Week";
                date = this.ViewModel.getPlanDate(this.CurrentWeek);
            }

            var nextDate = date.AddDays(6);
            this.expanders.Clear();
            this.planListBox.ItemsSource = null;
            this.buildView().ConfigureAwait(false);
            this.planHeader.Text = " Meal Plan for " + date.Month + "/" + date.Day + " - " + nextDate.Month + "/" + nextDate.Day;

        }
    }
}
