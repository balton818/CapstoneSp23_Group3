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


        /// <summary>Searches for the specified amount of recipes per page that match the filters in the request taking into account the ingredients the passed ingredients</summary>
        /// <param name="request">The request to browse recipes.</param>
        /// <param name="ingredients">The ingredients to inform what which recipes to bring back.</param>
        /// <param name="perPage">The amount of recipes to return per page.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public BrowseRecipeResponse BrowseRecipes(BrowseRecipeRequest request, string ingredients, int perPage);

        /// <summary>Converts measurement amounts with the Spoonacular api.</summary>
        /// <param name="request">The request to convert measurement amounts.</param>
        /// <returns>The conversion result if the is one</returns>
        public decimal? ConvertAmount(ConvertAmountRequest request);
    }
}
