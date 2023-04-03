using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     Interaction logic for MealPlanPage.xaml
/// </summary>
[ExcludeFromCodeCoverage]
public partial class MealPlanPage
{
    #region Data members

    private readonly List<MealPlanExpander> mealPlanExpanderPanes;

    #endregion

    #region Properties

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The main view model for the app.</value>
    public FoodieViewModel? ViewModel { get; set; }

    /// <summary>Gets a value indicating whether current week or next week is selected.</summary>
    /// <value>
    ///     <c>true</c> if [current week]; otherwise, next week and <c>false</c>.
    /// </value>
    public bool CurrentWeek { get; private set; } = true;

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="MealPlanPage" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    public MealPlanPage(FoodieViewModel? viewModel)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
        this.navMenu.Current = this;
        this.navMenu.FoodViewModel = this.ViewModel;
        this.mealPlanExpanderPanes = new List<MealPlanExpander>();
        this.buildView().ConfigureAwait(true);
        var foodieViewModel = this.ViewModel;
        if (foodieViewModel != null)
        {
            var date = foodieViewModel.GetPlanDate(this.CurrentWeek);
            var nextDate = date.AddDays(6);
            this.planHeader.Text = " Meal Plan for " + date.Month + "/" + date.Day + " - " + nextDate.Month + "/" +
                                   nextDate.Day;
        }
    }

    #endregion

    #region Methods

    private async Task buildView()
    {
        var foodieViewModel = this.ViewModel;
        if (foodieViewModel != null)
        {
            await foodieViewModel.GetPlans();
        }

        foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        {
            this.buildExpander(day);
        }

        this.planListBox.ItemsSource = this.mealPlanExpanderPanes;
    }

    private void buildExpander(DayOfWeek day)
    {
        var plannedLabel = "";
        var expander = new MealPlanExpander(this.ViewModel, day);
        expander.Date = day;
        expander.Current = this;
        var foodieViewModel = this.ViewModel;
        if (foodieViewModel != null)
        {
            var titles = foodieViewModel.GetMealPlan(this.CurrentWeek, day);
            foreach (MealType meal in Enum.GetValues(typeof(MealType)))
            {
                if (!string.IsNullOrEmpty(titles[meal]))
                {
                    plannedLabel += meal.ToString().Substring(0, 1) + " ";
                    if (meal.Equals(MealType.Breakfast))
                    {
                        expander.BreakfastName = titles[meal];
                        expander.DisableEnableBreakfast(true);
                    }
                    else if (meal.Equals(MealType.Lunch))
                    {
                        expander.LunchName = titles[meal];
                        expander.DisableEnableLunch(true);
                    }
                    else if (meal.Equals(MealType.Dinner))
                    {
                        expander.DinnerName = titles[meal];
                        expander.DisableEnableDinner(true);
                    }
                }
            }
        }

        expander.PlannedLabel = plannedLabel;
        this.mealPlanExpanderPanes.Add(expander);
    }

    private void viewOtherWeekClick(object sender, RoutedEventArgs e)
    {
        DateOnly date;
        if (this.CurrentWeek)
        {
            this.CurrentWeek = false;
            this.nextWeekButton.Content = "Back to Current Week";
            date = this.ViewModel!.GetPlanDate(this.CurrentWeek);
        }
        else
        {
            this.CurrentWeek = true;
            this.nextWeekButton.Content = "View Next Week";
            date = this.ViewModel!.GetPlanDate(this.CurrentWeek);
        }

        var nextDate = date.AddDays(6);
        this.mealPlanExpanderPanes.Clear();
        this.planListBox.ItemsSource = null;
        this.buildView().ConfigureAwait(false);
        this.planHeader.Text = " Meal Plan for " + date.Month + "/" + date.Day + " - " + nextDate.Month + "/" +
                               nextDate.Day;
    }

    private void navigateToPage(string navUri)
    {
        if (NavigationService != null)
        {
            var navigate = new PageNavigation(this.ViewModel);
            navigate.NavigateToPage(navUri, NavigationService);
        }
    }

    #endregion

    private void groceryListClick(object sender, RoutedEventArgs e)
    {
        NavButton clicked = (NavButton)sender;
        this.navigateToPage(clicked.NavUri);
    }

    private void addGroceriesNeeded(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}