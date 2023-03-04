namespace RecipePlannerApi.Model {
    public class MealPlan {
        public int? MealPlanId { get; set; }
        public DateTime? MealPlanDate { get; set; }
        public int UserId { get; set; }

        public List<Meal> meals { get; set; }
    }
}
