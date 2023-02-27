using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestUserService {
    public class TestGetUserPantry {
        [Fact]
        public void TestValidUserId() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();

            pantryDao.Setup(x => x.GetUserPantry(It.IsAny<int>())).Returns(new List<PantryItem>());
            var userService = new UserService(userDao.Object, pantryDao.Object);
            var result = userService.GetUserPantry(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void TestInvalidUserId() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();

            pantryDao.Setup(x => x.GetUserPantry(It.IsAny<int>())).Returns(new List<PantryItem>());
            var userService = new UserService(userDao.Object, pantryDao.Object);
            Assert.Throws<ArgumentException>(() => userService.GetUserPantry(0));
        }
    }
}
