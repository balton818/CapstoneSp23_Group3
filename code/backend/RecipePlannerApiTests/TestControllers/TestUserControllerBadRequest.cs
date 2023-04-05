using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipePlannerApi.Controllers;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApiTests.TestControllers {
    public class TestUserControllerBadRequest {
        [Fact]
        public void TestValidateUserBadRequest() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.ValidateUser("username", "password");

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestCreateUserBadRequest() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.CreateUser(It.IsAny<User>()))
                .Throws(new Exception());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.CreateUser(new User());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetUserPantryBadRequest() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.GetUserPantry(It.IsAny<int>()))
                .Throws(new Exception());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.GetPantry(1);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestAddPantryItemBadRequest() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.AddPantryItem(It.IsAny<PantryItem>()))
                .Throws(new Exception());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.AddPantryItem(new PantryItem());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestUpdatePantryItemBadRequest() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.UpdatePantryItem(It.IsAny<PantryItem>()))
                .Throws(new Exception());
            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.UpdatePantryItem(new PantryItem());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestRemovePantryItemBadRequest() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.RemovePantryItem(It.IsAny<int>()))
                .Throws(new Exception());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.RemovePantryItem(1);

            Assert.NotNull(result);
        }

    }
}
