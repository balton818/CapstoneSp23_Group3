using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Api.Requests {

    /// <summary>Request class for searching recipes by ingredients</summary>
    [ExcludeFromCodeCoverage]
    public class SearchRecipesByIngredientsRequest {

        /// <summary>Gets or sets the ingredients.</summary>
        /// <value>The ingredients.</value>
        public string ingredients { get; set; }

        /// <summary>Gets or sets the number.</summary>
        /// <value>The number.</value>
        public int? number { get; set; }

        /// <summary>Gets or sets a value indicating whether api should limit license. defaults to false</summary>
        /// <value>
        ///   <c>true</c> if [limit license]; otherwise, <c>false</c>.</value>
        public bool limitLicense { get; set; } = false;

        /// <summary>Gets or sets the ranking.</summary>
        /// <value>The ranking.</value>
        public decimal? ranking { get; set; }

        /// <summary>Gets or sets the ignore pantry.</summary>
        /// <value>The ignore pantry.</value>
        public bool? ignorePantry { get; set; }
    }
}
