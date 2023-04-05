using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service.Interface;
using RecipePlannerApi.Service;
using RecipePlannerApi.Model;

namespace RecipePlannerApiTests.TestServices.TestRecipeService
{
    public class TestGetRecipesByUserPantry
    {
        [Fact]
        public void TestValidRequest()
        {
            var userService = new Mock<IUserService>();
            var ingredientDao = new Mock<IIngredientDao>();
            var recipeApi = new Mock<IRecipeApi>();
            var measurementService = new Mock<IMeasurementService>();
            var shoppingService = new Mock<IShoppingListService>();

            var apiReturn = new List<SearchRecipesByIngredients200ResponseInner>(){
                new SearchRecipesByIngredients200ResponseInner() {
                    Id = 1,
                    MissedIngredientCount = 3,
                },
                new SearchRecipesByIngredients200ResponseInner() {
                    Id = 2,
                    MissedIngredientCount = 0,
                    UsedIngredients = new List<SearchRecipesByIngredients200ResponseInnerMissedIngredientsInner>
                    {
                        new SearchRecipesByIngredients200ResponseInnerMissedIngredientsInner() { Name = "apple",  Amount = 2 },
                        new SearchRecipesByIngredients200ResponseInnerMissedIngredientsInner() { Name = "pear",  Amount = 3 }
                    }
                },
                new SearchRecipesByIngredients200ResponseInner() {
                    Id = 4,
                    MissedIngredientCount = 0,
                    UsedIngredients = new List<SearchRecipesByIngredients200ResponseInnerMissedIngredientsInner>
                    {
                        new SearchRecipesByIngredients200ResponseInnerMissedIngredientsInner() { Name = "apple",  Amount = 6 },
                        new SearchRecipesByIngredients200ResponseInnerMissedIngredientsInner() { Name = "pear",  Amount = 3 }
                    }
                }
            };

            userService.Setup(x => x.GetUserPantry(It.IsAny<int>()))
                .Returns(new List<PantryItem> { new PantryItem { IngredientName = "apple", Quantity = 5, UnitId = AppUnit.NONE }, new PantryItem { IngredientName = "pear", Quantity = 5, UnitId = AppUnit.NONE } });
            recipeApi.Setup(x => x.SearchRecipesByIngredients(It.IsAny<SearchRecipesByIngredientsRequest>()))
                .Returns(apiReturn);
            var service = new RecipeService(userService.Object, ingredientDao.Object, recipeApi.Object, measurementService.Object, shoppingService.Object);

            var result = service.GetRecipesByUserPantry(1);

            Assert.Single(result);
        }
    }
}
