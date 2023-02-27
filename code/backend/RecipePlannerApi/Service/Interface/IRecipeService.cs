using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service.Interface
{
    public interface IRecipeService
    {
        /// <summary>Searches recipes by ingredients in api.</summary>
        /// <param name="request">The request to search for recipes by ingredients.</param>
        /// <returns>
        ///     List of qualifing recipes
        /// </returns>
        public List<SearchRecipesByIngredients200ResponseInner> SearchRecipes(SearchRecipesByIngredientsRequest request);

        /// <summary>Searches recipes by ingredients in api.</summary>
        /// <param name="request">The request to search for recipes by ingredients.</param>
        /// <returns>
        ///     List of qualifing recipes
        /// </returns>
        public List<Recipe> SearchRecipesByIngredients(SearchRecipesByIngredientsRequest request);


        /// <summary>Gets recipes by the ingredients the user has in their pantry.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A list of recipes</returns>
        public List<Recipe> GetRecipesByUserPantry(int userId);

        /// <summary>Gets the recipe information.</summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns>The information for the recipe</returns>
        public RecipeInformation GetRecipeInformation(int recipeId);

        public List<Ingredient> SearchIngredient(string search);

        public BrowseRecipeResponse BrowseRecipes(BrowseRecipeRequest request);
    }
}
