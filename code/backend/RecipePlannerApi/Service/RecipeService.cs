using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Dao.Request;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
    public class RecipeService: IRecipeService {
        private readonly IUserService _userService;
        private readonly IIngredientDao _ingredientDao;
        private readonly IRecipeApi _recipeApi;

        public RecipeService(IUserService userService, IIngredientDao ingredientDao, IRecipeApi recipeApi) {
            this._userService = userService;
            this._ingredientDao = ingredientDao;
            this._recipeApi = recipeApi;
        }

        /// <summary>Searches recipes by ingredients in api.</summary>
        /// <param name="request">The request to search for recipes by ingredients.</param>
        /// <returns>
        ///     List of qualifing recipes
        /// </returns>
        public List<SearchRecipesByIngredients200ResponseInner> SearchRecipes(SearchRecipesByIngredientsRequest request) {
            return this._recipeApi.SearchRecipesByIngredients(request);
        }

        /// <summary>Searches recipes by ingredients in api.</summary>
        /// <param name="request">The request to search for recipes by ingredients.</param>
        /// <returns>
        ///     List of qualifing recipes
        /// </returns>
        public List<Recipe> SearchRecipesByIngredients(SearchRecipesByIngredientsRequest request) {
            var searchResponse = this._recipeApi.SearchRecipesByIngredients(request);
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
        public List<Recipe> GetRecipesByUserPantry(int userId)
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

            var searchResponse = this._recipeApi.SearchRecipesByIngredients(searchRequest);

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

        private List<PantryItem> GetUserPantry(int userId) {
            var pantry = _userService.GetUserPantry(userId);

            pantry.Add(new PantryItem() {
                IngredientId = 14412,
                IngredientName = "water",
                Quantity = 100,
                unit = AppUnits.NONE
            });

            return pantry;
        }

        private bool CheckIngredientAmounts(List<SearchRecipesByIngredients200ResponseInnerMissedIngredientsInner> usedIngredients, List<PantryItem> pantry) {
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

        private PantryItem GetMatchingPantryItem(List<PantryItem> pantry, Ingredient recipeIngredient) {
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
        public RecipeInformation GetRecipeInformation(int recipeId) {
            return this._recipeApi.GetRecipeInformation(recipeId);
        }

        public List<Ingredient> SearchIngredient(string search) {
            return this._ingredientDao.SearchIngredient(search);
        }

        public BrowseRecipeResponse BrowseRecipes(BrowseRecipeRequest request) {
            var pantry = GetUserPantry(request.UserId);
            var ingredients = string.Join(",", pantry.Select(item => item.IngredientName).ToList());
   
            return this._recipeApi.BrowseRecipes(request, ingredients, 20);
        }
    }
}
