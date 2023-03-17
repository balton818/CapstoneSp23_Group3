using com.spoonacular;
using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api;
using RecipePlannerApi.Api.Requests;

namespace RecipePlannerApiTests.TestApi {
    public class TestSearchRecipesByIngredients {
        [Fact]
        public void TestValidRequest() {
            var api = new Mock<IRecipesApi>();

            api.Setup(x => x.SearchRecipesByIngredients(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<bool?>(),
                                                        It.IsAny<decimal?>(), It.IsAny<bool?>()))
                .Returns(new List<SearchRecipesByIngredients200ResponseInner>());

            var service = new RecipeApi(api.Object);

            var result = service.SearchRecipesByIngredients(new SearchRecipesByIngredientsRequest() {
                ignorePantry = true,
                ingredients = "",
                limitLicense = false,
                number = 2,
                ranking = 2
            });

            Assert.NotNull(result);
        }
    }
}
