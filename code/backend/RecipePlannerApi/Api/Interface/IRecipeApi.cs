using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Api.Interface
{
    public interface IRecipeApi
    {
        /// <summary>Searches the recipes by ingredients.</summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   Responce from api
        ///   <br />
        /// </returns>
        public List<SearchRecipesByIngredients200ResponseInner> SearchRecipesByIngredients(SearchRecipesByIngredientsRequest request);


        /// <summary>Gets the recipe information.</summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns>
        ///   <para>The information for the recipe</para>
        /// </returns>
        public RecipeInformation GetRecipeInformation(int recipeId);


        /// <summary>Gets the recipe instructions.</summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns>
        ///   <para>The instructionsfor the recipe</para>
        /// </returns>
        public List<RecipesStep> GetRecipeInstructions(int recipeId);

        public BrowseRecipeResponse BrowseRecipes(BrowseRecipeRequest request, string ingredients, int perPage);
    }
}
