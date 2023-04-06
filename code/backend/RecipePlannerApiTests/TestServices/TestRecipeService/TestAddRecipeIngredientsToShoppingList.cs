using Moq;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;
using RecipePlannerApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipePlannerApiTests.TestServices.TestRecipeService {
    public class TestAddRecipeIngredientsToShoppingList {
        [Fact]
        public void TestValidRequest() {
            var userService = new Mock<IUserService>();
            var ingredientDao = new Mock<IIngredientDao>();
            var recipeApi = new Mock<IRecipeApi>();
            var measurementService = new Mock<IMeasurementService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.GetUserPantry(It.IsAny<int>()))
                .Returns(new List<PantryItem> { new PantryItem { IngredientId = 1, IngredientName = "apple", Quantity = 5, UnitId = AppUnit.NONE }, new PantryItem { IngredientName = "pear", Quantity = 5, UnitId = AppUnit.NONE } });
            shoppingService.Setup(x => x.AddToShoppingList(It.IsAny<List<ShoppingListIngredient>>(), It.IsAny<int>())).Returns(new List<ShoppingListIngredient>());
            var service = new RecipeService(userService.Object, ingredientDao.Object, recipeApi.Object, measurementService.Object, shoppingService.Object);

            var ingredients = new List<Ingredient>() { new Ingredient { IngredientId = 1 }, new Ingredient { IngredientName = "pear" }, new Ingredient() };

            var result = service.AddRecipeIngredientsToShoppingList(ingredients, 1);

            Assert.NotNull(result);
        }

        [Fact]
        public void TestValidRequestRecipeIds() {
            var userService = new Mock<IUserService>();
            var ingredientDao = new Mock<IIngredientDao>();
            var recipeApi = new Mock<IRecipeApi>();
            var measurementService = new Mock<IMeasurementService>();
            var shoppingService = new Mock<IShoppingListService>();

            userService.Setup(x => x.GetUserPantry(It.IsAny<int>()))
                .Returns(new List<PantryItem> { new PantryItem { IngredientName = "apple", Quantity = 5, UnitId = AppUnit.NONE }, new PantryItem { IngredientName = "pear", Quantity = 5, UnitId = AppUnit.NONE } });
            shoppingService.Setup(x => x.AddToShoppingList(It.IsAny<List<ShoppingListIngredient>>(), It.IsAny<int>())).Returns(new List<ShoppingListIngredient>());
            recipeApi.Setup(x => x.GetRecipeIngredientsBulk(It.IsAny<List<int>>())).Returns(new List<Ingredient>() { new Ingredient()});
            var service = new RecipeService(userService.Object, ingredientDao.Object, recipeApi.Object, measurementService.Object, shoppingService.Object);

            var result = service.AddRecipeIngredientsToShoppingList(new List<int>(), 1);

            Assert.NotNull(result);
        }
    }
}
