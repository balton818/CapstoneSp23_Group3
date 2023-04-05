using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Api.Requests {
    [ExcludeFromCodeCoverage]
    public class BrowseRecipeRequest {
        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
        /// <summary>Gets or sets the search query.</summary>
        /// <value>The search query.</value>
        public string Query { get; set; }
        /// <summary>Gets or sets the cuisine filter.</summary>
        /// <value>The cuisine filter.</value>
        public string Cuisine { get; set; }
        /// <summary>Gets or sets the diet filter.</summary>
        /// <value>The diet filter.</value>
        public string Diet { get; set; }
        /// <summary>Gets or sets the meal type filter.</summary>
        /// <value>The meal type filter.</value>
        public string Type { get; set; }
        /// <summary>Gets or sets the page number.</summary>
        /// <value>The page number.</value>
        public int PageNumber { get; set; }
    }
}
