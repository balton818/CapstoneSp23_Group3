using com.spoonacular;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Api
{
    /// <summary>Wrapper class for the spoonacular api</summary>
    public class RecipeApi: IRecipeApi
    {
        private readonly IRecipesApi api;

        /// <summary>Initializes the <see cref="RecipeApi" /> class.</summary>
        public RecipeApi(IRecipesApi api)
        {
            this.api = api;
        }


        /// <summary>Searches the recipes by ingredients.</summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   Responce from api
        ///   <br />
        /// </returns>
        public List<SearchRecipesByIngredients200ResponseInner> SearchRecipesByIngredients(SearchRecipesByIngredientsRequest request)
        {
            return api.SearchRecipesByIngredients(request.ingredients, request.number, request.limitLicense, request.ranking, request.ignorePantry);
        }


        /// <summary>Gets the recipe information.</summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns>
        ///   <para>The information for the recipe</para>
        /// </returns>
        public RecipeInformation GetRecipeInformation(int recipeId)
        {
            var info = api.GetRecipeInformation(recipeId, false);
            var ingredients = new List<Ingredient>();
            var instructions = GetRecipeInstructions(recipeId);
            foreach (var item in info?.ExtendedIngredients)
            {
                var ingredient = new Ingredient()
                {
                    IngredientId = item.Id,
                    IngredientName = item.Name,
                    Quantity = (int)Math.Ceiling(item.Amount.Value),
                    Unit = item.Unit
                };
                ingredients.Add(ingredient);
            }

            return new RecipeInformation {
                Summary = info.Summary,
                Ingredients = ingredients,
                Steps = instructions,
                Image = info.Image,
                ImageType = info.ImageType
            };
        }


        /// <summary>Gets the recipe instructions.</summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns>
        ///   <para>The instructionsfor the recipe</para>
        /// </returns>
        public List<RecipesStep> GetRecipeInstructions(int recipeId)
        {
            var analyzedInsturctions = api.GetAnalyzedRecipeInstructions(recipeId, false).FirstOrDefault();
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

        public BrowseRecipeResponse BrowseRecipes(BrowseRecipeRequest request, string ingredients, int perPage) {
            var searchRequest = new SearchRecipeRequest() {
                query = request.Query,
                cuisine = request.Cuisine,
                diet = request.Diet,
                type = request.Type,
                offset = request.PageNumber * perPage,
                limitLicense = false,
                sort = "min-missing-ingredients",
                addRecipeInformation = false,
                number = perPage
            };

            var response = api.SearchRecipes(searchRequest);
            var recipes = new List<Recipe>();

            foreach (var item in response.Results) {
                recipes.Add(new Recipe() {
                    ApiId = item.Id,
                    Image = item.Image,
                    ImageType = item.ImageType,
                    Title = item.Title
                });
            }

            return new BrowseRecipeResponse() {
                recipes = recipes,
                page = response.Offset / perPage,
                totalNumberOfRecipes = response.TotalResults
            };
        }

        public decimal? ConvertAmount(ConvertAmountRequest request) {
            var response = this.api.ConvertAmounts(request.IngredientName, request.SourceAmount, request.SourceUnit, request.TargetUnit);

            return response.TargetAmount;
        }
    }
}
