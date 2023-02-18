using Org.OpenAPITools.Model;
using RecipePlannerApi.Api;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao;
using RecipePlannerApi.Dao.Request;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service {
    public static class RecipeService {

        /// <summary>Searches recipes by ingredients in api.</summary>
        /// <param name="request">The request to search for recipes by ingredients.</param>
        /// <returns>
        ///     List of qualifing recipes
        /// </returns>
        public static List<SearchRecipesByIngredients200ResponseInner> SearchRecipes(SearchRecipesByIngredientsRequest request) {
            return RecipeApi.SearchRecipesByIngredients(request);
        }

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
        public static List<Recipe> GetRecipesByUserPantry(int userId)
        {
            var pantry = GetUserPantry(userId);
            var ingredients = string.Join(",", pantry.Select(item => item.IngredientName).ToList());
            var searchRequest = new SearchRecipesByIngredientsRequest() {
                ingredients = ingredients,
                ranking = 2,
                ignorePantry = true,
                limitLicense = false,
                number = 20
            };

            var searchResponse = RecipeApi.SearchRecipesByIngredients(searchRequest);

            var recipes = new List<Recipe>();

            foreach (var item in searchResponse) {
                if (item.MissedIngredients.Count > 0 ) {
                    continue;
                }

                if(!CheckIngredientAmounts(item.UsedIngredients, pantry)) {
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

        private static List<PantryItem> GetUserPantry(int userId) {
            var pantry = UserService.GetUserPantry(userId);

            pantry.Add(new PantryItem() {
                IngredientId = 14412,
                IngredientName = "water",
                Quantity = 100,
                unit = AppUnits.NONE
            });

            return pantry;
        }

        private static bool CheckIngredientAmounts(List<SearchRecipesByIngredients200ResponseInnerMissedIngredientsInner> usedIngredients, List<PantryItem> pantry) {
            foreach (var item in usedIngredients) {
                var recipeIngredient = new Ingredient() {
                    IngredientId = item.Id,
                    IngredientName = item.Name,
                    Quantity = (int)Math.Ceiling(item.Amount.Value)
                };

                var pantryIngredient = GetMatchingPantryItem(pantry, recipeIngredient);

                if (pantryIngredient == null || pantryIngredient.Quantity < recipeIngredient.Quantity) {
                    return false;
                }
            }

            return true;
        }

        private static PantryItem GetMatchingPantryItem(List<PantryItem> pantry, Ingredient recipeIngredient) {
            PantryItem pantryIngredient;
            pantryIngredient = pantry.Find(i => recipeIngredient.IngredientId == i.IngredientId);
            if (pantryIngredient == null) {
                pantryIngredient = pantry.Find(i => recipeIngredient.IngredientName.ToLower().Contains(i.IngredientName.ToLower()));
            }
            pantryIngredient.IngredientId = recipeIngredient.IngredientId;
            return pantryIngredient;
        }



        /// <summary>Gets the recipe information.</summary>
        /// <param name="recipeId">The recipe identifier.</param>
        /// <returns>The information for the recipe</returns>
        public static RecipeInformation GetRecipeInformation(int recipeId) {
            return RecipeApi.GetRecipeInformation(recipeId);
        }

        public static List<Ingredient> SearchIngredient(string search) {
            return IngredientDao.SearchIngredient(search);
        }
    }
}
