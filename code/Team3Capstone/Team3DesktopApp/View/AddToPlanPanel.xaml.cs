using System;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     Interaction logic for AddToPlanPanel.xaml
/// </summary>
public partial class AddToPlanPanel
{
    #region Properties

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The main view model for the app.</value>
    public FoodieViewModel? ViewModel { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="AddToPlanPanel" /> class.</summary>
    public AddToPlanPanel()
    {
        this.InitializeComponent();
    }

    #endregion

    #region Methods

    private void addToPlanClick(object sender, RoutedEventArgs e)
    {
        var mealType = this.parseType();
        var day = this.parseDay();
        var current = this.parseCurrentWeek();
        var messageBoxText = "This recipe is already in your meal plan. Would you like to Overwrite it?";
        var button = MessageBoxButton.YesNo;
        var icon = MessageBoxImage.Warning;
        var caption = "Overwrite?";
        MessageBoxResult result;
        if (mealType == null || day == null)
        {
            MessageBox.Show("Please select a meal type, day, and week.");
        }
        else if (current != null && this.ViewModel!.MealPlanContainsRecipe(mealType.Value, day.Value, current.Value))
        {
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                var typeAndDate = new Tuple<DayOfWeek?, MealType?>(day, mealType);

                this.ViewModel.PlanTypeAndDateToAdd = typeAndDate;
                this.ViewModel.UpdatePlan(current);
                var parentGrid = (Grid)Parent;
                parentGrid.Visibility = Visibility.Hidden;
            }
        }
        else
        {
            var typeAndDate = new Tuple<DayOfWeek?, MealType?>(day, mealType);

            var foodieViewModel = this.ViewModel;
            if (foodieViewModel != null)
            {
                foodieViewModel.PlanTypeAndDateToAdd = typeAndDate;
                foodieViewModel.AddToMealPlan(current);
            }

            var parentGrid = (Grid)Parent;
            parentGrid.Visibility = Visibility.Hidden;
        }
    }

    private bool? parseCurrentWeek()
    {
        if (this.currentWeekButton.IsChecked == true)
        {
            return true;
        }

        if (this.nextWeekButton.IsChecked == true)
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
            return MealType.Breakfast;
        }

        if (this.lunchButton.IsChecked == true)
        {
            return MealType.Lunch;
        }

        if (this.dinnerButton.IsChecked == true)
        {
            return MealType.Dinner;
        }

        return null;
    }

    private void cancelClick(object sender, RoutedEventArgs e)
    {
        var parentGrid = (Grid)Parent;
        parentGrid.Visibility = Visibility.Hidden;
    }

    /// <summary>Sets the options for the add to plan panel if navigated from the planning page.</summary>
    /// <param name="currentWeek">if set to <c>true</c> [current week] else next week.</param>
    /// <param name="day">The day to set in the panel.</param>
    /// <param name="mealType">Type of the meal to set in the panel.</param>
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
            case MealType.Breakfast:
                this.breakfastButton.IsChecked = true;
                break;
            case MealType.Lunch:
                this.lunchButton.IsChecked = true;
                break;
            case MealType.Dinner:
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

    #endregion
}