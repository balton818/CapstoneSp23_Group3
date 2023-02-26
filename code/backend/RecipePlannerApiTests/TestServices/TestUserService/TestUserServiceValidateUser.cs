using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Dao.Request;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests {
    public class TestUserServiceValidateUser {
        [Fact]
        public void TestValidUserReturnsId() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();

            userDao.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(new IdDto() { Id = 1 });
            var userService = new UserService(userDao.Object, pantryDao.Object);
            var result = userService.ValidateUser("username","password");

            Assert.Equal(1, result);
        }
    }
}