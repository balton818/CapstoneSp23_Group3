namespace RecipePlannerApi.Model {
    public class Meal {
        public int? MealId { get; set; }
        public int MealPlanId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public MealType MealType { get; set; }
        public Recipe Recipe { get; set; }
    }
}
