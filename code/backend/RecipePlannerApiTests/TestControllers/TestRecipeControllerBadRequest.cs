﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Controllers;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApiTests.TestControllers {
    public class TestRecipeControllerBadRequest {
        [Fact]
        public void TestSearchRecipesBadRequest() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.SearchRecipes(It.IsAny<SearchRecipesByIngredientsRequest>()))
                .Throws(new Exception());
            var controller = new RecipeController(recipeService.Object);

            var result = controller.SearchRecipes(new SearchRecipesByIngredientsRequest());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestSearchRecipesByIngredientsBadRequest() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.SearchRecipesByIngredients(It.IsAny<SearchRecipesByIngredientsRequest>()))
                            .Throws(new Exception());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.SearchRecipesByIngredients(new SearchRecipesByIngredientsRequest());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetRecipesByUserPantryt() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.GetRecipesByUserPantry(It.IsAny<int>()))
                            .Throws(new Exception());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.GetRecipesByUserPantry(1);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetRecipeInformationBadRequest() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.GetRecipeInformation(It.IsAny<int>()))
                            .Throws(new Exception());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.GetRecipeInformation(1);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestSearchIngredientsBadRequest() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.SearchIngredient(It.IsAny<string>()))
                            .Throws(new Exception());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.SearchIngredients("");

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestBrowseRecipesBadRequest() {
            var recipeService = new Mock<IRecipeService>();

            recipeService.Setup(x => x.BrowseRecipes(It.IsAny<BrowseRecipeRequest>()))
                            .Throws(new Exception());

            var controller = new RecipeController(recipeService.Object);

            var result = controller.BrowseRecipes(new BrowseRecipeRequest());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
