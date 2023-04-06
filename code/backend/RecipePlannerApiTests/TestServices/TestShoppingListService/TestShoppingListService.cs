using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestShoppingListService {
    public class TestShoppingListService {

        [Fact]
        void TestAddToShoppingList() {
            var shoppingListDao = new Mock<IShoppingListDao>();

            shoppingListDao.Setup(x => x.UpsertShoppingListIngredient(It.IsAny<ShoppingListIngredient>())).Returns(new ShoppingListIngredient());
            shoppingListDao.Setup(x => x.GetShoppingList(It.IsAny<int>())).Returns(new List<ShoppingListIngredient>() { new ShoppingListIngredient() { IngredientId = 1 } });
            var service = new ShoppingListService(shoppingListDao.Object);

            var list = new List<ShoppingListIngredient> {
                new ShoppingListIngredient() { IngredientId = 1},
                new ShoppingListIngredient()
            };

            var result = service.AddToShoppingList(list, 1);

            Assert.NotNull(result);
        }

        [Fact]
        void TestDeleteShoppingListIngredient() {
            var shoppingListDao = new Mock<IShoppingListDao>();

            shoppingListDao.Setup(x => x.DeleteShoppingListIngredient(It.IsAny<int>()));
            var service = new ShoppingListService(shoppingListDao.Object);

            var list = new List<ShoppingListIngredient> {
                new ShoppingListIngredient(),
                new ShoppingListIngredient()
            };

            service.DeleteShoppingListIngredient(1);

            shoppingListDao.Verify(x => x.DeleteShoppingListIngredient(It.IsAny<int>()));
        }

        [Fact]
        void TestGetShoppingList() {
            var shoppingListDao = new Mock<IShoppingListDao>();

            shoppingListDao.Setup(x => x.GetShoppingList(It.IsAny<int>())).Returns(new List<ShoppingListIngredient>());
            var service = new ShoppingListService(shoppingListDao.Object);

            var list = new List<ShoppingListIngredient> {
                new ShoppingListIngredient(),
                new ShoppingListIngredient()
            };

            var result = service.GetShoppingList(1);

            Assert.NotNull(result);
        }

        [Fact]
        void TestDeleteAllFromShoppingList() {
            var shoppingListDao = new Mock<IShoppingListDao>();

            shoppingListDao.Setup(x => x.DeleteShoppingListIngredient(It.IsAny<int>()));
            var service = new ShoppingListService(shoppingListDao.Object);

            var list = new List<ShoppingListIngredient> {
                new ShoppingListIngredient(),
                new ShoppingListIngredient{ ShoppingListId = 1},
                new ShoppingListIngredient{ ShoppingListId = 1}
            };

            service.DeleteAllFromShoppingList(list);

            shoppingListDao.Verify(x => x.DeleteShoppingListIngredient(It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        void TestUpsertShoppingListIngredient() {
            var shoppingListDao = new Mock<IShoppingListDao>();

            shoppingListDao.Setup(x => x.UpsertShoppingListIngredient(It.IsAny<ShoppingListIngredient>())).Returns(new ShoppingListIngredient());
            var service = new ShoppingListService(shoppingListDao.Object);

            var result = service.UpsertShoppingListIngredient(new ShoppingListIngredient());

            Assert.NotNull(result);
        }
    }
}
