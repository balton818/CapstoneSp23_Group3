using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestUserService {
    public class TestUpdatePantry {
        [Fact]
        public void TestValidPantryItem() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();
            var pantryItem = new PantryItem {
                IngredientId = null,
                IngredientName = "apple",
                Quantity = 1,
                PantryId = 2,
                Unit = AppUnit.NONE,
                UserId = 1
            };

            pantryDao.Setup(x => x.UpdatePantryItem(It.IsAny<PantryItem>())).Returns(new PantryItem());
            var userService = new UserService(userDao.Object, pantryDao.Object);
            var result = userService.UpdatePantryItem(pantryItem);

            Assert.NotNull(result);
        }

        [Fact]
        public void TestNullPantryItem() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();

            var userService = new UserService(userDao.Object, pantryDao.Object);


            Assert.Throws<ArgumentNullException>(() => userService.UpdatePantryItem(null));
        }

        [Fact]
        public void TestItemNameNull() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();
            var pantryItem = new PantryItem {
                IngredientId = null,
                IngredientName = null,
                Quantity = 1,
                PantryId = 2,
                Unit = AppUnit.NONE,
                UserId = 1
            };

            var userService = new UserService(userDao.Object, pantryDao.Object);


            Assert.Throws<ArgumentException>(() => userService.UpdatePantryItem(pantryItem));
        }

        [Fact]
        public void TestItemNameEmpty() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();
            var pantryItem = new PantryItem {
                IngredientId = null,
                IngredientName = "",
                Quantity = 1,
                PantryId = 2,
                Unit = AppUnit.NONE,
                UserId = 1
            };

            var userService = new UserService(userDao.Object, pantryDao.Object);


            Assert.Throws<ArgumentException>(() => userService.UpdatePantryItem(pantryItem));
        }

        [Fact]
        public void TestItemNameTooLong() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();
            var pantryItem = new PantryItem {
                IngredientId = null,
                IngredientName = "The worlds most larget recipe ingredient item that ever existed",
                Quantity = 1,
                PantryId = 2,
                Unit = AppUnit.NONE,
                UserId = 1
            };

            var userService = new UserService(userDao.Object, pantryDao.Object);


            Assert.Throws<ArgumentException>(() => userService.UpdatePantryItem(pantryItem));
        }
    }
}
