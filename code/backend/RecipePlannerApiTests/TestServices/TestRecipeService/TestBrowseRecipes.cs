using Moq;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service.Interface;
using RecipePlannerApi.Service;
using RecipePlannerApi.Model;

namespace RecipePlannerApiTests.TestServices.TestRecipeService {
    public class TestBrowseRecipes {
        [Fact]
        public void TestValidRequest() {
            var userService = new Mock<IUserService>();
            var ingredientDao = new Mock<IIngredientDao>();
            var recipeApi = new Mock<IRecipeApi>();
            var measurementService = new Mock<IMeasurementService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.GetUserPantry(It.IsAny<int>()))
                .Returns(new List<PantryItem> { new PantryItem { IngredientName = "apple", Quantity = 5, UnitId = AppUnit.NONE }, new PantryItem { IngredientName = "pear", Quantity = 5, UnitId = AppUnit.NONE } });
            recipeApi.Setup(x => x.BrowseRecipes(It.IsAny<BrowseRecipeRequest>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new BrowseRecipeResponse());
            var service = new RecipeService(userService.Object, ingredientDao.Object, recipeApi.Object, measurementService.Object, shoppingService.Object);

            var result = service.BrowseRecipes(new BrowseRecipeRequest());

            Assert.NotNull(result);
        }
    }
}
