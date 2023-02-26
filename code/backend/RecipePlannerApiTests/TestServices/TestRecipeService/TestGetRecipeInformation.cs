using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service.Interface;
using RecipePlannerApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlannerApi.Model;

namespace RecipePlannerApiTests.TestServices.TestRecipeService {
    public class TestGetRecipeInformation {
        [Fact]
        public void TestValidRequest() {
            var userService = new Mock<IUserService>();
            var ingredientDao = new Mock<IIngredientDao>();
            var recipeApi = new Mock<IRecipeApi>();
            var measurementService = new Mock<IMeasurementService>();

            recipeApi.Setup(x => x.GetRecipeInformation(It.IsAny<int>()))
                .Returns(new RecipeInformation());
            var service = new RecipeService(userService.Object, ingredientDao.Object, recipeApi.Object, measurementService.Object);

            var result = service.GetRecipeInformation(1);

            Assert.NotNull(result);
        }
    }
}
