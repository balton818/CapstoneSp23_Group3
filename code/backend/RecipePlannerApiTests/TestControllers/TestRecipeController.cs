using Microsoft.AspNetCore.Mvc;
using Moq;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Controllers;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApiTests.TestControllers {
    public class TestRecipeController {
        [Fact]
        public void TestSearchRecipes() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.SearchRecipes(It.IsAny<SearchRecipesByIngredientsRequest>()))
                .Returns(new List<SearchRecipesByIngredients200ResponseInner>());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.SearchRecipes(new SearchRecipesByIngredientsRequest());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestSearchRecipesByIngredients() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.SearchRecipesByIngredients(It.IsAny<SearchRecipesByIngredientsRequest>()))
                            .Returns(new List<Recipe>());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.SearchRecipesByIngredients(new SearchRecipesByIngredientsRequest());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetRecipeInformation() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.GetRecipeInformation(It.IsAny<int>()))
                            .Returns(new RecipeInformation());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.GetRecipeInformation(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetRecipesByUserPantry() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.GetRecipesByUserPantry(It.IsAny<int>()))
                            .Returns(new List<Recipe>());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.GetRecipesByUserPantry(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestSearchIngredients() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.SearchIngredient(It.IsAny<string>()))
                            .Returns(new List<Ingredient>());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.SearchIngredients("");

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestBrowseRecipes() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.BrowseRecipes(It.IsAny<BrowseRecipeRequest>()))
                            .Returns(new BrowseRecipeResponse());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.BrowseRecipes(new BrowseRecipeRequest());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestAddRecipeIngredientsToShoppingListByIngredients() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.AddRecipeIngredientsToShoppingList(It.IsAny<List<Ingredient>>(), It.IsAny<int>()))
                            .Returns(new List<ShoppingListIngredient>());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.AddRecipeIngredientsToShoppingList(new List<Ingredient>(), 1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestAddRecipeIngredientsToShoppingListByRecipeIds() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.AddRecipeIngredientsToShoppingList(It.IsAny<List<int>>(), It.IsAny<int>()))
                            .Returns(new List<ShoppingListIngredient>());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.AddRecipeIngredientsToShoppingList(new List<int>(), 1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestUseIngredients() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.UseIngredients(It.IsAny<List<Ingredient>>(), It.IsAny<int>()))
                            .Returns(new List<PantryItem>());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.UseIngredients(new List<Ingredient>(), 1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestBuyIngredients() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.BuyIngredients(It.IsAny<List<ShoppingListIngredient>>(), It.IsAny<int>()))
                            .Returns(new List<PantryItem>());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.BuyIngredients(new List<ShoppingListIngredient>(), 1);

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
