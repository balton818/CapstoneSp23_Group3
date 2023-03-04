using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {

    [ExcludeFromCodeCoverage]
    public class Recipe {

        /// <summary>Gets or sets the identifier for recipe.</summary>
        /// <value>The identifier.</value>
        public int? Id { get; set; }

        public int? ApiId { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>Gets or sets the image.</summary>
        /// <value>The image.</value>
        public string Image { get; set; }

        /// <summary>Gets or sets the type of the image.</summary>
        /// <value>The type of the image.</value>
        public string ImageType { get; set; }
    }
}
