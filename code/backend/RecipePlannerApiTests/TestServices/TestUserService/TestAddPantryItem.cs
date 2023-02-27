using Moq;
using Newtonsoft.Json;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestUserService {
    public class TestAddPantryItem {
        [Fact]
        public void TestAddValidPantryItem() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();
            var pantryItem = new PantryItem {
                IngredientId = null,
                IngredientName = "apple",
                Quantity = 1,
                PantryId = 2,
                UnitId = AppUnit.NONE,
                UserId = 1
            };

            pantryDao.Setup(x => x.AddPantryItem(It.IsAny<PantryItem>())).Returns(pantryItem);

            var userService = new UserService(userDao.Object, pantryDao.Object);


            var result = userService.AddPantryItem(pantryItem);

            Assert.Equal(JsonConvert.SerializeObject(pantryItem), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public void TestAddNullPantryItem() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();

            var userService = new UserService(userDao.Object, pantryDao.Object);


            Assert.Throws<ArgumentNullException>(() => userService.AddPantryItem(null));
        }

        [Fact]
        public void TestAddItemNameNull() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();
            var pantryItem = new PantryItem {
                IngredientId = null,
                IngredientName = null,
                Quantity = 1,
                PantryId = 2,
                UnitId = AppUnit.NONE,
                UserId = 1
            };

            var userService = new UserService(userDao.Object, pantryDao.Object);


            Assert.Throws<ArgumentException>(() => userService.AddPantryItem(pantryItem));
        }

        [Fact]
        public void TestAddItemNameEmpty() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();
            var pantryItem = new PantryItem {
                IngredientId = null,
                IngredientName = "",
                Quantity = 1,
                PantryId = 2,
                UnitId = AppUnit.NONE,
                UserId = 1
            };

            var userService = new UserService(userDao.Object, pantryDao.Object);


            Assert.Throws<ArgumentException>(() => userService.AddPantryItem(pantryItem));
        }

        [Fact]
        public void TestAddItemNameTooLong() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();
            var pantryItem = new PantryItem {
                IngredientId = null,
                IngredientName = "The worlds most larget recipe ingredient item that ever existed",
                Quantity = 1,
                PantryId = 2,
                UnitId = AppUnit.NONE,
                UserId = 1
            };

            var userService = new UserService(userDao.Object, pantryDao.Object);


            Assert.Throws<ArgumentException>(() => userService.AddPantryItem(pantryItem));
        }
    }
}
