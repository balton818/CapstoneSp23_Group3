using RecipePlannerApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Api.Requests {
    [ExcludeFromCodeCoverage]
    public class BrowseRecipeResponse {
        public List<Recipe> recipes { get; set; }
        public int? page { get; set; }
        public int? totalNumberOfRecipes { get; set; }
    }
}
