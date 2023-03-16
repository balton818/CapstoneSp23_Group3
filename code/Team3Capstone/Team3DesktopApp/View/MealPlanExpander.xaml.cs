using System;
using System.Windows;
using System.Windows.Controls;
using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopApp.View;

/// <summary>
///     Interaction logic for MealPlanExpander.xaml
/// </summary>
public partial class MealPlanExpander
{
    #region Properties

    /// <summary>Gets or sets the name of the breakfast recipe.</summary>
    /// <value>The name of the breakfast recipe.</value>
    public string? BreakfastName { get; set; }

    /// <summary>Gets or sets the current page.</summary>
    /// <value>The current page the expander is instantiated on.</value>
    public Page? Current { get; set; }

    /// <summary>Gets or sets the name of the breakfast recipe.</summary>
    /// <value>The name of the breakfast recipe.</value>
    public string? LunchName { get; set; }

    /// <summary>Gets or sets the name of the breakfast recipe.</summary>
    /// <value>The name of the breakfast recipe.</value>
    public string? DinnerName { get; set; }

    /// <summary>Gets or sets the day of the week selected.</summary>
    /// <value>The day of the week selected.</value>
    public DayOfWeek Date { get; set; }

    /// <summary>Gets or sets the view model.</summary>
    /// <value>The view model.</value>
    public FoodieViewModel? ViewModel { get; set; }

    /// <summary>Gets or sets the planned label.</summary>
    /// <value>
    ///     The planned label indicated what meals types have been planned
    ///     B for breakfast
    ///     L for Lunch
    ///     D for Dinner.
    /// </value>
    public string PlannedLabel { get; set; } = "Planned - ";

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="MealPlanExpander" /> class.</summary>
    /// <param name="viewModel">The view model.</param>
    /// <param name="day">The day.</param>
    public MealPlanExpander(FoodieViewModel? viewModel, DayOfWeek day)
    {
        this.InitializeComponent();
        this.ViewModel = viewModel;
        DataContext = this;
        this.Date = day;
    }

    #endregion

    #region Methods

    private void navigateToPage(string navUri)
    {
        var navigate = new PageNavigation(this.ViewModel);
        var current = this.Current;
        if (current != null)
        {
            var currentNavigationService = current.NavigationService;
            if (currentNavigationService != null)
            {
                navigate.NavigateToPage(navUri, currentNavigationService);
            }
        }
    }

    private void addMealClick(object sender, RoutedEventArgs e)
    {
        var buttonPressed = sender as NavButton;
        if (buttonPressed != null)
        {
            this.checkMealTypeToAdd(buttonPressed);

            this.navigateToPage(buttonPressed.NavUri);
        }
    }

    private void checkMealTypeToAdd(NavButton? buttonPressed)
    {
        if (this.ViewModel != null)
        {
            if (buttonPressed!.Name.Equals(this.addBreakfastButton.Name))
            {
                this.ViewModel.PlanTypeAndDateToAdd = new Tuple<DayOfWeek?, MealType?>(this.Date, MealType.Breakfast);
            }
            else if (buttonPressed.Name.Equals(this.addLunchButton.Name))
            {
                this.ViewModel.PlanTypeAndDateToAdd = new Tuple<DayOfWeek?, MealType?>(this.Date, MealType.Lunch);
            }
            else if (buttonPressed.Name.Equals(this.addDinnerButton.Name))
            {
                this.ViewModel.PlanTypeAndDateToAdd = new Tuple<DayOfWeek?, MealType?>(this.Date, MealType.Dinner);
            }
        }
    }

    /// <summary>Disables the enables the breakfast aspects of the meal plan.</summary>
    /// <param name="active">
    ///     if set to <c>true</c> [active] displays the breakfast info
    ///     otherwise if false displays the add meal option.
    /// </param>
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

    /// <summary>Disables the enables the lunch aspects of the meal plan.</summary>
    /// <param name="active">
    ///     if set to <c>true</c> [active] displays the lunch info
    ///     otherwise if false displays the add meal option.
    /// </param>
    public void DisableEnableLunch(bool active)
    {
        if (active)
        {
            this.addLunchButton.Visibility = Visibility.Hidden;
            this.lunchDetailButton.Visibility = Visibility.Visible;
            this.removeLunchButton.Visibility = Visibility.Visible;
        }
        else
        {
            this.addLunchButton.Visibility = Visibility.Visible;
            this.lunchDetailButton.Visibility = Visibility.Hidden;
            this.removeLunchButton.Visibility = Visibility.Hidden;
        }
    }

    /// <summary>Disables the enables the dinner aspects of the meal plan.</summary>
    /// <param name="active">
    ///     if set to <c>true</c> [active] displays the dinner info
    ///     otherwise if false displays the add meal option.
    /// </param>
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

    private void removeMealClick(object sender, RoutedEventArgs e)
    {

        if (sender.Equals(this.breakfastRemoveButton))
        {
            this.removalRoutine(MealType.Breakfast, this.BreakfastName!);
        }

        else if (sender.Equals(this.removeLunchButton))
        {

            this.removalRoutine(MealType.Lunch, this.LunchName!);

        }

        else if (sender.Equals(this.removeDinnerButton))

        {
            this.removalRoutine(MealType.Dinner, this.DinnerName!);
        }
    }

    private void removalRoutine(MealType type, string name)
    {
        var messageBoxText = "Confirm removal of " + type + " recipe?";
        var button = MessageBoxButton.YesNo;
        var icon = MessageBoxImage.Warning;
        var caption = "Remove?";
        var result = MessageBox.Show(messageBoxText, caption, button, icon);
        if (result == MessageBoxResult.Yes)
        {
            this.ViewModel!.RemoveMealFromPlan(name, this.Date, type);
            this.navigateToPage("/View/MealPlanPage.xaml");
        }
    }

    private void mealDetailClick(object sender, RoutedEventArgs e)
    {
        if (sender.Equals(this.breakfastDetailButton))
        {
            this.ViewModel?.RecipeDetailNavPlan(this.Date, MealType.Breakfast);
        }

        else if (sender.Equals(this.lunchDetailButton))
        {
            this.ViewModel?.RecipeDetailNavPlan(this.Date, MealType.Lunch);
        }

        else if (sender.Equals(this.dinnerDetailButton))

        {
            this.ViewModel?.RecipeDetailNavPlan(this.Date, MealType.Dinner);
        }

        this.navigateToPage("/View/RecipeDetailPage.xaml");
    }

    #endregion
}