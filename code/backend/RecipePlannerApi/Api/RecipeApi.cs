using com.spoonacular;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao;
using RecipePlannerApi.Dao.Request;
using RecipePlannerApi.Model;
using System.Runtime.CompilerServices;

namespace RecipePlannerApi.Api
{
    /// <summary>Wrapper class for the spoonacular api</summary>
    public static class RecipeApi
    {
        private static RecipesApi recipesApi = new RecipesApi();

        /// <summary>Initializes the <see cref="RecipeApi" /> class.</summary>
        static RecipeApi()
        {
            Configuration.ApiKey.Add("x-api-key", Connection.SpoonacularApiKey);
        }


        /// <summary>Searches the recipes by ingredients.</summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   Responce from api
        ///   <br />
        /// </returns>
        public static List<SearchRecipesByIngredients200ResponseInner> SearchRecipesByIngredients(SearchRecipesByIngredientsRequest request)
        {
            return recipesApi.SearchRecipesByIngredients(request.ingredients, request.number, request.limitLicense, request.ranking, request.ignorePantry);
        }


        /// <summary>Gets the recipe information.</summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns>
        ///   <para>The information for the recipe</para>
        /// </returns>
        public static RecipeInformation GetRecipeInformation(int recipeId)
        {
            var info = recipesApi.GetRecipeInformation(recipeId, false);
            var ingredients = new List<Ingredient>();
            var instructions = GetRecipeInstructions(recipeId);
            foreach (var item in info?.ExtendedIngredients)
            {
                var ingredient = new Ingredient()
                {
                    IngredientId = item.Id,
                    IngredientName = item.Name,
                    Quantity = (int)Math.Ceiling(item.Amount.Value),
                };
                ingredients.Add(ingredient);
            }

            return new RecipeInformation
            {
                Summary = info.Summary,
                Ingredients = ingredients,
                Steps = instructions
            };
        }


        /// <summary>Gets the recipe instructions.</summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns>
        ///   <para>The instructionsfor the recipe</para>
        /// </returns>
        public static List<RecipesStep> GetRecipeInstructions(int recipeId)
        {
            var analyzedInsturctions = recipesApi.GetAnalyzedRecipeInstructions(recipeId, false).FirstOrDefault();
            var instructions = new List<RecipesStep>();
            if (analyzedInsturctions != null)
            {
                foreach (var step in analyzedInsturctions?.Steps)
                {
                    instructions.Add(new RecipesStep()
                    {
                        stepNumber = (int)step.Number,
                        instructions = step.Step
                    });
                }
            }
            return instructions;
        }
    }
}
