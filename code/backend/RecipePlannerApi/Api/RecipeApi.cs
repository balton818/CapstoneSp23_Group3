using com.spoonacular;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao;

namespace RecipePlannerApi.Api {
    /// <summary>Wrapper class for the spoonacular api</summary>
    public static class RecipeApi {
        private static RecipesApi recipesApi = new RecipesApi();

        /// <summary>Initializes the <see cref="RecipeApi" /> class.</summary>
        static RecipeApi() {
            Configuration.ApiKey.Add("x-api-key", Connection.SpoonacularApiKey);
        }


        /// <summary>Searches the recipes by ingredients.</summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   Responce from api
        ///   <br />
        /// </returns>
        public static List<SearchRecipesByIngredients200ResponseInner> SearchRecipesByIngredients(SearchRecipesByIngredientsRequest request) {
            return recipesApi.SearchRecipesByIngredients(request.ingredients, request.number, request.limitLicense, request.ranking, request.ignorePantry);
        }
    }
}
