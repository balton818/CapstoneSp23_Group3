using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestUserService {
    public class TestRemovePantryItem {
        [Fact]
        public void TestValidUserId() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();

            var userService = new UserService(userDao.Object, pantryDao.Object);
            userService.RemovePantryItem(1);

            pantryDao.Verify(n => n.RemovePantryItem(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void TestInvalidUserId() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();

            var userService = new UserService(userDao.Object, pantryDao.Object);
            Assert.Throws<ArgumentException>(() => userService.RemovePantryItem(0));
        }
    }
}
