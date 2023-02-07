using Org.OpenAPITools.Model;
using RecipePlannerApi.Api;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service {
    public static class RecipeService {

        /// <summary>Searches recipes by ingredients in api.</summary>
        /// <param name="request">The request to search for recipes by ingredients.</param>
        /// <returns>
        ///     List of qualifing recipes
        /// </returns>
        public static List<Recipe> SearchRecipesByIngredients(SearchRecipesByIngredientsRequest request) {
            var searchResponse = RecipeApi.SearchRecipesByIngredients(request);
            var recipes = new List<Recipe>();

            foreach (var item in searchResponse) {
                if(item.MissedIngredients.Count > 0) {
                    continue;
                }

                var recipe = new Recipe() {
                    Id = item.Id,
                    Image = item.Image,
                    ImageType = item.ImageType,
                    Title = item.Title
                };

                recipes.Add(recipe);
            }

            return recipes;
        }
    }
}
