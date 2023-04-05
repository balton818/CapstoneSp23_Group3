using System;

namespace Team3DesktopApp.Model;

/// <summary>
///     Meal class for defining a meal object for the meal planner
/// </summary>
public class Meal
{
    #region Properties

    /// <summary>Gets or sets the meal id</summary>
    /// <value>The meal identifier.</value>
    public int? MealId { get; set; }

    /// <summary>Gets or sets the date.</summary>
    /// <value>The date.</value>
    public DateTime Date { get; set; }

    /// <summary>Gets or sets the day of week.</summary>
    /// <value>The day of week.</value>
    public DayOfWeek DayOfWeek { get; set; }

    /// <summary>Gets or sets the type of the meal.</summary>
    /// <value>The type of the meal.</value>
    public MealType MealType { get; set; }

    /// <summary>Gets or sets the recipe.</summary>
    /// <value>The recipe.</value>
    public Recipe? Recipe { get; set; }

    #endregion
}