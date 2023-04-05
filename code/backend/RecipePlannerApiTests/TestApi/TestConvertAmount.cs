using com.spoonacular;
using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Api;

namespace RecipePlannerApiTests.TestApi {
    public class TestConvertAmount {
        [Fact]
        public void TestValidParameters() {
            var api = new Mock<IRecipesApi>();

            api.Setup(x => x.ConvertAmounts(It.IsAny<string>(), It.IsAny<decimal?>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ConvertAmounts200Response { TargetAmount = 3});

            var service = new RecipeApi(api.Object);

            var result = service.ConvertAmount(new ConvertAmountRequest());

            Assert.NotNull(result);
        }
    }
}
