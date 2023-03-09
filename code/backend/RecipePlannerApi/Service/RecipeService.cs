using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
    public class RecipeService: IRecipeService {
        private readonly IUserService _userService;
        private readonly IIngredientDao _ingredientDao;
        private readonly IRecipeApi _recipeApi;
        private readonly IMeasurementService _measurementService;

        public RecipeService(IUserService userService, IIngredientDao ingredientDao, IRecipeApi recipeApi, IMeasurementService measurementService) {
            this._userService = userService;
            this._ingredientDao = ingredientDao;
            this._recipeApi = recipeApi;
            this._measurementService = measurementService;
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
                if(item.MissedIngredientCount > 0) {
                    continue;
                }

                var recipe = new Recipe() {
                    ApiId = item.Id,
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
                if (item.MissedIngredientCount > 0 ) {
                    continue;
                }

                if(!CheckIngredientAmounts(item.UsedIngredients, pantry)) {
                    continue;
                }

                var recipe = new Recipe() {
                    ApiId = item.Id,
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
                UnitId = AppUnit.NONE
            });

            return pantry;
        }

        private bool CheckIngredientAmounts(List<SearchRecipesByIngredients200ResponseInnerMissedIngredientsInner> usedIngredients, List<PantryItem> pantry) {
            foreach (var item in usedIngredients) {
                var recipeIngredient = new Ingredient() {
                    IngredientId = item.Id,
                    IngredientName = item.Name,
                    Quantity = (int)Math.Ceiling(item.Amount.Value),
                    Unit = item.Unit,
                };

                var pantryIngredient = GetMatchingPantryItem(pantry, recipeIngredient);

                if (pantryIngredient == null) {
                    return false;
                }

                var recipeQuantity = TryConvertQuantity(recipeIngredient, pantryIngredient);

                if (pantryIngredient == null || pantryIngredient.Quantity < recipeQuantity) {
                    return false;
                }
            }

            return true;
        }

        private int? TryConvertQuantity(Ingredient recipeIngredient, PantryItem pantryIngredient) {
            if (!this._measurementService.IsValidUnit(recipeIngredient.Unit) || pantryIngredient.UnitId == AppUnit.NONE) {
                return recipeIngredient.Quantity;
            }

            var recipeQuantity = this._measurementService.Convert(recipeIngredient.Quantity, recipeIngredient.Unit, pantryIngredient.UnitId);

            if (recipeQuantity == null) {
                var toUnitString = MeasurementService.AppUnitUnitInfo[pantryIngredient.UnitId].Name;
                var result = this._recipeApi.ConvertAmount(new ConvertAmountRequest {
                    IngredientName = recipeIngredient.IngredientName,
                    SourceAmount = recipeIngredient.Quantity,
                    SourceUnit = recipeIngredient.Unit,
                    TargetUnit = toUnitString,
                });

                recipeQuantity = result != null ? (int)Math.Ceiling(result.Value) : recipeIngredient.Quantity;
            }

            return recipeQuantity;
        }

        private PantryItem GetMatchingPantryItem(List<PantryItem> pantry, Ingredient recipeIngredient) {
            PantryItem pantryIngredient;
            pantryIngredient = pantry.Find(i => recipeIngredient.IngredientId == i.IngredientId);
            if (pantryIngredient == null) {
                pantryIngredient = pantry.Find(i => recipeIngredient.IngredientName.ToLower().Contains(i.IngredientName.ToLower()));
                pantryIngredient = pantryIngredient ?? pantry.Find(i => i.IngredientName.ToLower().Contains(i.IngredientName.ToLower()));
            }

            if (recipeIngredient.IngredientId != null) {
                pantryIngredient.IngredientId = recipeIngredient.IngredientId;
            }
            return pantryIngredient;
        }



        /// <summary>Gets the recipe information.</summary>
        /// <param name="recipeId">The recipe identifier for the api.</param>
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
