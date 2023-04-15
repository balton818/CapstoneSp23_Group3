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
        private readonly IShoppingListService _shoppingListService;

        public RecipeService(IUserService userService, IIngredientDao ingredientDao, IRecipeApi recipeApi, IMeasurementService measurementService, IShoppingListService shoppingListService) {
            this._userService = userService;
            this._ingredientDao = ingredientDao;
            this._recipeApi = recipeApi;
            this._measurementService = measurementService;
            this._shoppingListService = shoppingListService;
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
            var pantry = GetUserPantry(userId, true);
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
                var recipeIngredients = item.UsedIngredients.Concat(item.MissedIngredients).ToList();
                if(!CheckIngredientAmounts(recipeIngredients, pantry)) {
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

        private List<PantryItem> GetUserPantry(int userId, bool addWater) {
            var pantry = _userService.GetUserPantry(userId);

            if (addWater) {
                pantry.Add(new PantryItem() {
                    IngredientId = 14412,
                    IngredientName = "water",
                    Quantity = 100,
                    UnitId = AppUnit.NONE
                });
            }

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

                var pantryIngredient = GetMatchingPantryItem(pantry, recipeIngredient.IngredientId, recipeIngredient.IngredientName);

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

        private int TryConvertQuantity(Ingredient recipeIngredient, PantryItem pantryIngredient) {
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

            return recipeQuantity.Value;
        }

        private Tuple<int, AppUnit> TryConvertQuantity(Ingredient recipeIngredient) {
            if (!this._measurementService.IsValidUnit(recipeIngredient.Unit)) {
                return Tuple.Create(recipeIngredient.Quantity, AppUnit.NONE);
            }

            var recipeQuantity = this._measurementService.Convert(recipeIngredient.Quantity, recipeIngredient.Unit);

            return recipeQuantity;
        }

        private PantryItem GetMatchingPantryItem(List<PantryItem> pantry, int? ingredientId, string ingredientName) {
            if(pantry == null || pantry.Count < 0 || (ingredientId == null && string.IsNullOrEmpty(ingredientName))) {
                return null;
            }

            PantryItem pantryIngredient = null;
            if (ingredientId != null)
            {
                pantryIngredient = pantry.Find(i => ingredientId == i.IngredientId);
            }

            if (!string.IsNullOrEmpty(ingredientName))
            {
                pantryIngredient ??= pantry.Find(i => ingredientName.ToLower().Contains(i.IngredientName.ToLower()));
                pantryIngredient ??= pantry.Find(i => i.IngredientName.ToLower().Contains(ingredientName.ToLower()));
            }

            if (ingredientId != null && pantryIngredient != null) {
                pantryIngredient.IngredientId = ingredientId;
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
            var pantry = GetUserPantry(request.UserId, true);
            var ingredients = string.Join(",", pantry.Select(item => item.IngredientName).ToList());
   
            return this._recipeApi.BrowseRecipes(request, ingredients, 20);
        }

        public List<ShoppingListIngredient> AddRecipeIngredientsToShoppingList(List<Ingredient> ingredients, int userId) {
            var pantry = _userService.GetUserPantry(userId);
            var shoppingList = new List<ShoppingListIngredient>();
            foreach (var ingredient in ingredients) {
                var pantryItem = this.GetMatchingPantryItem(pantry, ingredient.IngredientId, ingredient.IngredientName);
                int ingredientQuantity;
                AppUnit unit;
                if (pantryItem != null) {
                    ingredientQuantity = TryConvertQuantity(ingredient, pantryItem);
                    unit = pantryItem.UnitId;
                } else {
                    var quantity = TryConvertQuantity(ingredient);
                    ingredientQuantity = quantity.Item1;
                    unit = quantity.Item2;
                }

                if (pantryItem == null || (pantryItem != null && pantryItem.Quantity < ingredientQuantity)) {
                    shoppingList.Add(new ShoppingListIngredient() {
                        UserId = userId,
                        IngredientName = ingredient.IngredientName,
                        IngredientId = ingredient.IngredientId,
                        Quantity = ingredientQuantity,
                        UnitId = unit
                    });
                }
            }

            return this._shoppingListService.AddToShoppingList(shoppingList, userId);
        }

        public List<ShoppingListIngredient> AddRecipeIngredientsToShoppingList(List<int> recipeIds, int userId) {
            var ingredients = _recipeApi.GetRecipeIngredientsBulk(recipeIds);

            return this.AddRecipeIngredientsToShoppingList(ingredients, userId);
        }

        public List<PantryItem> UseIngredients(List<Ingredient> ingredients, int userId) {
            var pantry = this.GetUserPantry(userId, false);

            List<PantryItem> usedItems = new List<PantryItem>();
            foreach (var ingredient in ingredients) {
                var pantryItem = this.GetMatchingPantryItem(pantry, ingredient.IngredientId, ingredient.IngredientName);
                int ingredientQuantity;
                if (pantryItem != null) {
                    ingredientQuantity = TryConvertQuantity(ingredient, pantryItem);
                    pantryItem.Quantity -= ingredientQuantity;
                    if (pantryItem.Quantity < 0) {
                        pantryItem.Quantity = 0;
                    }

                    usedItems.Add(pantryItem);
                }
            }

            return this._userService.UpdatePantryItems(usedItems, userId);
        }

        public List<PantryItem> BuyIngredients(List<ShoppingListIngredient> ingredients, int userId)
        {
            var pantry = this.GetUserPantry(userId, false);

            List<PantryItem> newPantryItems = new List<PantryItem>();
            foreach (var ingredient in ingredients) {
                var pantryItem = this.GetMatchingPantryItem(pantry, ingredient.IngredientId, ingredient.IngredientName);
                if (pantryItem != null) {
                    pantryItem.Quantity += ingredient.Quantity;

                    newPantryItems.Add(pantryItem);
                } else {
                    newPantryItems.Add(new PantryItem {
                        IngredientId = ingredient.IngredientId,
                        IngredientName = ingredient.IngredientName,
                        UnitId = ingredient.UnitId,
                        Quantity = ingredient.Quantity,
                        UserId = userId
                    });
                }
            }


            var newPantry = this._userService.UpdatePantryItems(newPantryItems, userId);

            this._shoppingListService.DeleteAllFromShoppingList(ingredients);
            return newPantry;
        }
    }
}
