using com.spoonacular;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Controllers;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApiTests.TestApi {
    public class TestSearchRecipesByIngredients {
        [Fact]
        public void TestValidRequest() {
            var api = new Mock<IRecipesApi>();

            api.Setup(x => x.SearchRecipesByIngredients(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<bool?>(),
                                                        It.IsAny<int>(), It.IsAny<bool?>()))
                .Returns(new List<SearchRecipesByIngredients200ResponseInner>());

            var service = new RecipeApi(api.Object);

            var result = service.SearchRecipesByIngredients(new SearchRecipesByIngredientsRequest());

            Assert.NotNull(result);
        }
    }
}
