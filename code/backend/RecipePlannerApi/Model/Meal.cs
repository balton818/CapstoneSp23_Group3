namespace RecipePlannerApi.Model {
    public class Meal {
        public int? mealId { get; set; }
        public int mealPlanId { get; set; }
        public DayOfWeek dayOfWeek { get; set; }
        public string mealType { get; set; }
        public Recipe recipe { get; set; }
    }
}
