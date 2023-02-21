using RecipePlannerApi.Model;

namespace RecipePlannerApi.Api.Requests {
    public class BrowseRecipeResponse {
        public List<Recipe> recipes { get; set; }
        public int? page { get; set; }
        public int? totalNumberOfRecipes { get; set; }
    }
}
