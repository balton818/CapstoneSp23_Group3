using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipePlannerApi.Controllers;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApiTests.TestControllers {
    public class TestUserController {
        [Fact]
        public void TestValidateUser() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(1);

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.ValidateUser("username", "password");

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestCreateUser() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.CreateUser(It.IsAny<User>()))
                .Returns(1);

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.CreateUser(new User());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetUserPantry() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.GetUserPantry(It.IsAny<int>()))
                .Returns(new List<PantryItem>());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.GetPantry(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestAddPantryItem() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.AddPantryItem(It.IsAny<PantryItem>()))
                .Returns(new PantryItem());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.AddPantryItem(new PantryItem());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestUpdatePantryItem() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.UpdatePantryItem(It.IsAny<PantryItem>()))
                .Returns(new PantryItem());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.UpdatePantryItem(new PantryItem());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestRemovePantryItem() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.RemovePantryItem(It.IsAny<int>()));

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.RemovePantryItem(1);

            Assert.NotNull(result);
        }

    }
}
