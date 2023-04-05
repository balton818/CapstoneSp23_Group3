using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {
    [ExcludeFromCodeCoverage]
    public class MealPlan {
        /// <summary>
        /// Gets or sets the meal plan identifier.
        /// </summary>
        /// <value>The meal plan identifier.</value>
        public int? MealPlanId { get; set; }
        /// <summary>
        /// Gets or sets the meal plan date.
        /// </summary>
        /// <value>The meal plan date.</value>
        public DateTime? MealPlanDate { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the meals.
        /// </summary>
        /// <value>The meals.</value>
        public Dictionary<DayOfWeek, List<Meal>> meals { get; set; } = new Dictionary<DayOfWeek, List<Meal>>();

        public List<int> Recipes { get; set; }
    }
}
