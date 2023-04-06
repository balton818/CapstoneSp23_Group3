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

        [Fact]
        public void TestGetShoppingList() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            shoppingService.Setup(x => x.GetShoppingList(It.IsAny<int>()))
                .Returns(new List<ShoppingListIngredient>());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.GetShoppingList(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestAddShoppingListIngredient() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            shoppingService.Setup(x => x.UpsertShoppingListIngredient(It.IsAny<ShoppingListIngredient>()))
                .Returns(new ShoppingListIngredient());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.AddShoppingListIngredient(new ShoppingListIngredient());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestUpdateShoppingListIngredient() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            shoppingService.Setup(x => x.UpsertShoppingListIngredient(It.IsAny<ShoppingListIngredient>()))
                .Returns(new ShoppingListIngredient());

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.UpdateShoppingListIngredient(new ShoppingListIngredient());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestRemoveShoppingListIngredient() {
            var userService = new Mock<IUserService>();
            var shoppingService = new Mock<IShoppingListService>();

            shoppingService.Setup(x => x.DeleteShoppingListIngredient(It.IsAny<int>()));

            var controller = new UserController(userService.Object, shoppingService.Object);

            var result = controller.RemoveShoppingListIngredient(1);

            Assert.NotNull(result);
        }

    }
}
