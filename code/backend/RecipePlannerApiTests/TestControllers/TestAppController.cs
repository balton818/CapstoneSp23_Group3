using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipePlannerApi.Controllers;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApiTests.TestControllers {
    public class TestAppController {
        [Fact]
        public void TestGetAppCuisines() {
            var appService = new Mock<IAppService>();

            appService.Setup(x => x.getAppCuisines()).Returns(new List<string>());

            var controller = new AppController(appService.Object);

            var result = controller.getAppCuisines();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetAppDiets() {
            var appService = new Mock<IAppService>();

            appService.Setup(x => x.getAppDiets()).Returns(new List<string>());

            var service = new AppController(appService.Object);

            var result = service.getAppDiets();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetAppMealTypes() {
            var appService = new Mock<IAppService>();

            appService.Setup(x => x.getAppMealTypes()).Returns(new List<string>());

            var service = new AppController(appService.Object);

            var result = service.getAppMealTypes();

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
