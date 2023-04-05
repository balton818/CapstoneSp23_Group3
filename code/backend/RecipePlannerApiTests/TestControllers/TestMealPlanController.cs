using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipePlannerApi.Controllers;
using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApiTests.TestControllers {
    public class TestMealPlanController {

        [Fact]
        public void TestGetMealPlan() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.GetMealPlan(It.IsAny<GetMealPlanRequest>())).Returns(new MealPlan());

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.GetMealPlan(new GetMealPlanRequest());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetThisWeeksMealPlan() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.GetThisWeeksMealPlan(It.IsAny<int>())).Returns(new MealPlan());

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.GetThisWeek(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetNextWeeksMealPlan() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.GetNextWeeksMealPlan(It.IsAny<int>())).Returns(new MealPlan());

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.GetNextWeek(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestAddMeal() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.AddMeal(It.IsAny<Meal>())).Returns(new Meal());

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.AddMeal(new Meal());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestUpdateMeal() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.UpdateMeal(It.IsAny<Meal>())).Returns(new Meal());

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.UpdateMeal(new Meal());

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void TestRemoveMeal() {
            var mealplan = new Mock<IMealPlanService>();

            mealplan.Setup(x => x.RemoveMeal(It.IsAny<int>()));

            var controller = new MealPlanController(mealplan.Object);

            var result = controller.RemoveMeal(1);

            Assert.IsType<OkResult>(result);
        }
    }
}
