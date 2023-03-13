using com.spoonacular;
using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Api;

namespace RecipePlannerApiTests.TestApi {
    public class TestBrowseRecipe {
        [Fact]
        public void TestValidRequest() {
            var api = new Mock<IRecipesApi>();

            api.Setup(x => x.SearchRecipes(It.IsAny<SearchRecipeRequest>()))
                .Returns(new SearchRecipes200Response());

            var service = new RecipeApi(api.Object);

            var result = service.BrowseRecipes(new BrowseRecipeRequest(), "", 0);

            Assert.NotNull(result);
        }
    }
}
