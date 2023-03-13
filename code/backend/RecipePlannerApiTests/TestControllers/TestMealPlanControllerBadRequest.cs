using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Controllers;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipePlannerApiTests.TestControllers {
    public class TestMealPlanControllerBadRequest {
        [Fact]
        public void TestGetMealPlanBadRequest() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.GetMealPlan(It.IsAny<GetMealPlanRequest>())).Throws<Exception>();

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.GetMealPlan(new GetMealPlanRequest());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetThisWeeksMealPlanBadRequest() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.GetThisWeeksMealPlan(It.IsAny<int>())).Throws<Exception>();

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.GetThisWeek(1);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetNextWeeksMealPlanBadRequest() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.GetNextWeeksMealPlan(It.IsAny<int>())).Throws<Exception>();

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.GetNextWeek(1);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestAddMealBadRequest() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.AddMeal(It.IsAny<Meal>())).Throws<Exception>();

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.AddMeal(new Meal());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestUpdateMealBadRequest() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Throws<Exception>();

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.UpdateMeal(new Meal());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestRemoveMealBadRequest() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.RemoveMeal(It.IsAny<int>())).Throws<Exception>();

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.RemoveMeal(1);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
