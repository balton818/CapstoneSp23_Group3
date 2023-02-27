using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApiTests.TestServices.TestRecipeService {
    public class TestSearchRecipes {

        [Fact]
        public void TestValidRequest() {
            var userService = new Mock<IUserService>();
            var ingredientDao = new Mock<IIngredientDao>();
            var recipeApi = new Mock<IRecipeApi>();
            var measurementService = new Mock<IMeasurementService>();

            recipeApi.Setup(x => x.SearchRecipesByIngredients(It.IsAny<SearchRecipesByIngredientsRequest>()))
                .Returns(new List<SearchRecipesByIngredients200ResponseInner>());
            var service = new RecipeService(userService.Object, ingredientDao.Object, recipeApi.Object, measurementService.Object);

            var result = service.SearchRecipes(new SearchRecipesByIngredientsRequest());

            Assert.NotNull(result);
        }
    }
}
