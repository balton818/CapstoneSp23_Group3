using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices {
    public class TestAppService {

        [Fact]
        public void TestGetAppCuisines() {
            var appDao = new Mock<IAppDao>();
            
            appDao.Setup(x => x.getAppCuisines()).Returns(new List<string>());

            var service = new AppService(appDao.Object);

            var result = service.getAppCuisines();

            Assert.NotNull(result);
        }

        [Fact]
        public void TestGetAppDiets() {
            var appDao = new Mock<IAppDao>();

            appDao.Setup(x => x.getAppDiets()).Returns(new List<string>());

            var service = new AppService(appDao.Object);

            var result = service.getAppDiets();

            Assert.NotNull(result);
        }

        [Fact]
        public void TestGetAppMealTypes() {
            var appDao = new Mock<IAppDao>();

            appDao.Setup(x => x.getAppMealTypes()).Returns(new List<string>());

            var service = new AppService(appDao.Object);

            var result = service.getAppMealTypes();

            Assert.NotNull(result);
        }
    }
}
