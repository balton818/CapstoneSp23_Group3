using RecipePlannerApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Dao.Dto
{
    [ExcludeFromCodeCoverage]
    public class MealDto
    {
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
        /// Gets or sets the identifier for recipe.
        /// </summary>
        /// <value>The identifier.</value>
        public int? RecipeId { get; set; }

        /// <summary>
        /// Gets or sets the API identifier.
        /// </summary>
        /// <value>The API identifier.</value>
        public int? ApiId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of the image.
        /// </summary>
        /// <value>The type of the image.</value>
        public string ImageType { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }
    }
}
