using com.spoonacular;
using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipePlannerApiTests.TestApi {
    public class TestConvertAmount {
        [Fact]
        public void TestValidId() {
            var api = new Mock<IRecipesApi>();

            api.Setup(x => x.ConvertAmounts(It.IsAny<string>(), It.IsAny<decimal?>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ConvertAmounts200Response());

            var service = new RecipeApi(api.Object);

            var result = service.ConvertAmount(new ConvertAmountRequest());

            Assert.NotNull(result);
        }
    }
}
