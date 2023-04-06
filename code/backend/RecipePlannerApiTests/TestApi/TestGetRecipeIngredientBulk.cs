using com.spoonacular;
using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api;

namespace RecipePlannerApiTests.TestApi {
    public class TestGetRecipeIngredientBulk {
        [Fact]
        public void TestValidRequest() {
            var api = new Mock<IRecipesApi>();

            api.Setup(x => x.GetRecipeInformationBulk(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(new List<GetRecipeInformationBulk200ResponseInner>() {
                    new GetRecipeInformationBulk200ResponseInner(){
                        ExtendedIngredients = new List<GetRecipeInformation200ResponseExtendedIngredientsInner>()
                    } });

            var service = new RecipeApi(api.Object);

            var result = service.GetRecipeIngredientsBulk(new List<int>());

            Assert.NotNull(result);
        }
    }
}
