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

namespace RecipePlannerApiTests.TestServices.TestRecipeService {
    public class TestSearchRecipesByIngredients {
        [Fact]
        public void TestValidRequest() {
            var userService = new Mock<IUserService>();
            var ingredientDao = new Mock<IIngredientDao>();
            var recipeApi = new Mock<IRecipeApi>();
            var measurementService = new Mock<IMeasurementService>();

            var apiReturn = new List<SearchRecipesByIngredients200ResponseInner>(){
                new SearchRecipesByIngredients200ResponseInner() {
                    Id = 1,
                    MissedIngredientCount = 3
                },
                new SearchRecipesByIngredients200ResponseInner() {
                    Id = 2,
                    MissedIngredientCount = 0
                },
                new SearchRecipesByIngredients200ResponseInner() {
                    Id = 4,
                    MissedIngredientCount = 0
                }
            };

            recipeApi.Setup(x => x.SearchRecipesByIngredients(It.IsAny<SearchRecipesByIngredientsRequest>()))
                .Returns(apiReturn);
            var service = new RecipeService(userService.Object, ingredientDao.Object, recipeApi.Object, measurementService.Object);

            var result = service.SearchRecipesByIngredients(new SearchRecipesByIngredientsRequest());

            Assert.Equal(2, result.Count());
        }
    }
}
