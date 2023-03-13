using com.spoonacular;
using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api;

namespace RecipePlannerApiTests.TestApi {
    public class TestGetRecipeInformation {
        [Fact]
        public void TestValidRequest() {
            var api = new Mock<IRecipesApi>();

            api.Setup(x => x.GetRecipeInformation(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new GetRecipeInformation200Response { ExtendedIngredients = new List<GetRecipeInformation200ResponseExtendedIngredientsInner>()});

            api.Setup(x => x.GetAnalyzedRecipeInstructions(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new List<GetAnalyzedRecipeInstructions200ResponseParsedInstructionsInner>());

            var service = new RecipeApi(api.Object);

            var result = service.GetRecipeInformation(1);

            Assert.NotNull(result);
        }
    }
}
