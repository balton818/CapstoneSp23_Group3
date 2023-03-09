using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao.Dto
{
    public class MealDto
    {
        public int? MealId { get; set; }
        public int MealPlanId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public MealType MealType { get; set; }

        /// <summary>Gets or sets the identifier for recipe.</summary>
        /// <value>The identifier.</value>
        public int? RecipeId { get; set; }

        public int? ApiId { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>Gets or sets the image.</summary>
        /// <value>The image.</value>
        public string ImageUrl { get; set; }

        /// <summary>Gets or sets the type of the image.</summary>
        /// <value>The type of the image.</value>
        public string ImageType { get; set; }
        public DateTime Date { get; set; }
    }
}
