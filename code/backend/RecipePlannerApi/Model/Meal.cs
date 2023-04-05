using RecipePlannerApi.Dao.Dto;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {
    [ExcludeFromCodeCoverage]
    public class Meal {
        /// <summary>
        /// Gets or sets the meal identifier.
        /// </summary>
        /// <value>The meal identifier.</value>
        public int? MealId { get; set; }

        /// <summary>
        /// Gets or sets the meal plan identifier.
        /// </summary>
        /// <value>The meal plan identifier.</value>
        public int MealPlanId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the day of week.
        /// </summary>
        /// <value>The day of week.</value>
        public DayOfWeek DayOfWeek { get; set; }

        /// <summary>
        /// Gets or sets the type of the meal.
        /// </summary>
        /// <value>The type of the meal.</value>
        public MealType MealType { get; set; }

        /// <summary>
        /// Gets or sets the recipe.
        /// </summary>
        /// <value>The recipe.</value>
        public Recipe Recipe { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meal"/> class.
        /// </summary>
        public Meal() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meal"/> class from a MealDto.
        /// </summary>
        /// <param name="mealDto">The meal dto.</param>
        /// <exception cref="System.ArgumentNullException">a meal did not come back from database</exception>
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
