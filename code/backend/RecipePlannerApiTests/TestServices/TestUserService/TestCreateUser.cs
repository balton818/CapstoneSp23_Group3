using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestUserService {
    public class TestCreateUser {
        [Fact]
        public void TestValidUserReturnsId() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();

            userDao.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(1);
            var userService = new UserService(userDao.Object, pantryDao.Object);
            var result = userService.CreateUser(new User());

            Assert.Equal(1, result);
        }
    }
}
