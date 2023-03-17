using System;
using System.Collections.Generic;

namespace Team3DesktopApp.Model;

/// <summary>
///     Meal plan class defines a meal plan for the planner
/// </summary>
public class MealPlan
{
    #region Properties

    /// <summary>Gets or sets the meal plan id.</summary>
    /// <value>The meal plan id.</value>
    public int? MealPlanId { get; set; }

    /// <summary>Gets or sets the meal plan date.</summary>
    /// <value>The meal plan date.</value>
    public DateTime MealPlanDate { get; set; }

    /// <summary>Gets or sets the collection of meals.</summary>
    /// <value>The meals currently planned.</value>
    public Dictionary<DayOfWeek, List<Meal?>> Meals { get; set; } = new();

    #endregion
}