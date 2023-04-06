using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Controllers.Requests {
    [ExcludeFromCodeCoverage]
    public class GetMealPlanRequest {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
    }
}
