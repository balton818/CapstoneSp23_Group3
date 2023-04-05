using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service.Interface
{
    /// <summary>
    /// Interface IRecipeService
    /// </summary>
    public interface IRecipeService
    {
        /// <summary>
        /// Searches recipes by ingredients in api.
        /// </summary>
        /// <param name="request">The request to search for recipes by ingredients.</param>
        /// <returns>List of qualifing recipes</returns>
        public List<SearchRecipesByIngredients200ResponseInner> SearchRecipes(SearchRecipesByIngredientsRequest request);

        /// <summary>
        /// Searches recipes by ingredients in api.
        /// </summary>
        /// <param name="request">The request to search for recipes by ingredients.</param>
        /// <returns>List of qualifing recipes</returns>
        public List<Recipe> SearchRecipesByIngredients(SearchRecipesByIngredientsRequest request);


        /// <summary>
        /// Gets recipes by the ingredients the user has in their pantry.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>A list of recipes</returns>
        public List<Recipe> GetRecipesByUserPantry(int userId);

        /// <summary>
        /// Gets the recipe information.
        /// </summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns>The information for the recipe</returns>
        public RecipeInformation GetRecipeInformation(int recipeId);

        /// <summary>
        /// Searches for ingredients that match the string.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List&lt;Ingredient&gt;.</returns>
        public List<Ingredient> SearchIngredient(string search);

        /// <summary>
        /// Searches for the recipes that match the given filters.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>BrowseRecipeResponse.</returns>
        public BrowseRecipeResponse BrowseRecipes(BrowseRecipeRequest request);
        List<ShoppingListIngredient> AddRecipeIngredientsToShoppingList(List<Ingredient> ingredients, int userId);
        List<ShoppingListIngredient> AddRecipeIngredientsToShoppingList(List<int> recipeIds, int userId);
        List<PantryItem> UseIngredients(List<Ingredient> ingredients, int userId);
        List<PantryItem> BuyIngredients(List<ShoppingListIngredient> ingredients, int userId);
    }
}
