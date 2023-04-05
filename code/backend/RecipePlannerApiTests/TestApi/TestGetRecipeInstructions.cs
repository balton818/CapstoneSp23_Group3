using com.spoonacular;
using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api;

namespace RecipePlannerApiTests.TestApi {
    public class TestGetRecipeInstructions {
        [Fact]
        public void TestValidId() {
            var api = new Mock<IRecipesApi>();

            api.Setup(x => x.GetAnalyzedRecipeInstructions(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new List<GetAnalyzedRecipeInstructions200ResponseParsedInstructionsInner>());

            var service = new RecipeApi(api.Object);

            var result = service.GetRecipeInstructions(1);

            Assert.NotNull(result);
        }
    }
}
