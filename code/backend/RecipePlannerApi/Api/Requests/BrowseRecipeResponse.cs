using RecipePlannerApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Api.Requests {
    [ExcludeFromCodeCoverage]
    public class BrowseRecipeResponse {
        /// <summary>Gets or sets the list recipes.</summary>
        /// <value>The list recipes.</value>
        public List<Recipe> recipes { get; set; }
        /// <summary>Gets or sets the page.</summary>
        /// <value>The page.</value>
        public int? page { get; set; }
        /// <summary>Gets or sets the total number of recipes.</summary>
        /// <value>The total number of recipes.</value>
        public int? totalNumberOfRecipes { get; set; }
    }
}
