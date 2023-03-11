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

        public MealPlanPage(FoodieViewModel viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
            this.navMenu.Current = this;
            this.navMenu.FoodViewModel = this.ViewModel;
            this.buildView().ConfigureAwait(true);
        }

        private async Task buildView()
        {
            List<MealPlanExpander> expanders = new List<MealPlanExpander>();
            await this.ViewModel.GetPlans();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                this.buildExpander(day, expanders);
            }


        }

        private void buildExpander(DayOfWeek day, List<MealPlanExpander> expanders)
        {
            string plannedLabel = "";
            MealPlanExpander expander = new MealPlanExpander(this.ViewModel, day);
            expander.Date = day.ToString();
            Dictionary<MealType, string> titles = this.ViewModel.GetMealPlan(this.CurrentWeek, day);
            foreach (MealType meal in Enum.GetValues(typeof(MealType)))
            {
                if (!string.IsNullOrEmpty(titles[meal]))
                {
                    plannedLabel += meal.ToString().Substring(0, 1) + " / ";
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
            expanders.Add(expander);
            this.planListBox.ItemsSource = expanders;
        }


        private async void viewOtherWeekClick(object sender, RoutedEventArgs e)
        {
            if (this.CurrentWeek)
            {
                this.CurrentWeek = false;
                this.nextWeekButton.Content = "Back to Current Week";
            }
            else
            {
                this.CurrentWeek = true;
            }

            await this.buildView();
        }
    }
}
