using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipePlannerApi.Controllers;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApiTests.TestControllers {
    public class TestAppControllerBadRequest {
        [Fact]
        public void TestGetAppCuisinesBadRequest() {
            var appService = new Mock<IAppService>();

            appService.Setup(x => x.getAppCuisines()).Throws(new Exception());

            var controller = new AppController(appService.Object);

            var result = controller.getAppCuisines();

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetAppDietsBadRequest() {
            var appService = new Mock<IAppService>();

            appService.Setup(x => x.getAppDiets()).Throws(new Exception());

            var service = new AppController(appService.Object);

            var result = service.getAppDiets();

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetAppMealTypesBadRequest() {
            var appService = new Mock<IAppService>();

            appService.Setup(x => x.getAppMealTypes()).Throws(new Exception());

            var service = new AppController(appService.Object);

            var result = service.getAppMealTypes();

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
