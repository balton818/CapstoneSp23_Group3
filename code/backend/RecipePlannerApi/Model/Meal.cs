using RecipePlannerApi.Dao.Dto;

namespace RecipePlannerApi.Model {
    public class Meal {
        public int? MealId { get; set; }
        public int MealPlanId { get; set; }
        public DateTime Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public MealType MealType { get; set; }
        public Recipe Recipe { get; set; }

        public Meal() { }
        
        public Meal(MealDto mealDto) {
            this.MealId = mealDto.MealId;
            this.MealPlanId = mealDto.MealPlanId;
            this.DayOfWeek = mealDto.DayOfWeek;
            this.MealType = mealDto.MealType;
            this.Date = mealDto.Date;
            if(mealDto.RecipeId != null || mealDto.ApiId != null) {
                this.Recipe = new Recipe() {
                    RecipeId = mealDto.RecipeId,
                    ApiId = mealDto.ApiId,
                    Title = mealDto.Title,
                    Image = mealDto.ImageUrl,
                    ImageType = mealDto.ImageType
                };
            }
            
        }
    }
}
