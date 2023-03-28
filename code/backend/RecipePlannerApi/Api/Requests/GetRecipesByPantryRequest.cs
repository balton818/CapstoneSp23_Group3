using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Api.Requests {
    /// <summary>Request class for getting recipes based on a user's pantry</summary>
    [ExcludeFromCodeCoverage]
    public class GetRecipesByPantryRequest {
        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public int userId { get; set; }
    }
}
