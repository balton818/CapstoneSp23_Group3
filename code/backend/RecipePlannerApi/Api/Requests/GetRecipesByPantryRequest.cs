using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Api.Requests {
    [ExcludeFromCodeCoverage]
    public class GetRecipesByPantryRequest {
        public int userId { get; set; }
    }
}
