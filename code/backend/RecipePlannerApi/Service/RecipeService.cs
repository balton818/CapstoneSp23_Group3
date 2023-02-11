using RecipePlannerApi.Api;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao.Request;
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


        /// <summary>Gets recipes by the ingredients the user has in their pantry.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A list of recipes</returns>
        public static List<Recipe> GetRecipesByUserPantry(GetRecipesByPantryRequest request) {
            //get user's pantry
            var ingredients = "eggs, water, sugar, flour, regular oats, cherry, apples, grapes, chocolate chips, carrot, onion, potato, peach, milk, butter, sausage";
            var searchRequest = new SearchRecipesByIngredientsRequest() {
                ingredients = ingredients,
                ignorePantry = true,
                limitLicense = false,
                number = 30
            };

            var searcResponse = RecipeApi.SearchRecipesByIngredients(searchRequest);

            return new List<Recipe>();
        }


        /// <summary>Gets the recipe information.</summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns>The information for the recipe</returns>
        public static RecipeInformation GetRecipeInformation(int recipeId) {
            return RecipeApi.GetRecipeInformation(recipeId);
        }
    }
}
