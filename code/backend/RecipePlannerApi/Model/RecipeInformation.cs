using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {
    [ExcludeFromCodeCoverage]
    public class RecipeInformation {
        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the type of the image.
        /// </summary>
        /// <value>The type of the image.</value>
        public string ImageType { get; set; }

        /// <summary>
        /// Gets or sets the ingredients.
        /// </summary>
        /// <value>The ingredients.</value>
        public List<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        /// <value>The steps.</value>
        public List<RecipesStep> Steps { get; set; }
    }
}
