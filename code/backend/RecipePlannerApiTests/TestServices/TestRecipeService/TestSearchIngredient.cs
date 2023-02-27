using Moq;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service.Interface;
using RecipePlannerApi.Service;
using RecipePlannerApi.Model;

namespace RecipePlannerApiTests.TestServices.TestRecipeService {
    public class TestSearchIngredient {
        [Fact]
        public void TestValidRequest() {
            var userService = new Mock<IUserService>();
            var ingredientDao = new Mock<IIngredientDao>();
            var recipeApi = new Mock<IRecipeApi>();
            var measurementService = new Mock<IMeasurementService>();

            ingredientDao.Setup(x => x.SearchIngredient(It.IsAny<string>()))
                .Returns(new List<Ingredient>());
            var service = new RecipeService(userService.Object, ingredientDao.Object, recipeApi.Object, measurementService.Object);

            var result = service.SearchIngredient("");

            Assert.NotNull(result);
        }
    }
}
