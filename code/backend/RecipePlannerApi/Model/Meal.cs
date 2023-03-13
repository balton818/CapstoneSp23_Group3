using RecipePlannerApi.Dao.Dto;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {
    [ExcludeFromCodeCoverage]
    public class Meal {
        public int? MealId { get; set; }
        public int MealPlanId { get; set; }
        public DateTime Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public MealType MealType { get; set; }
        public Recipe Recipe { get; set; }

        public Meal() { }
        
        public Meal(MealDto mealDto) {
            if (mealDto == null) {
                throw new ArgumentNullException("a meal did not come back from database");
            }
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
