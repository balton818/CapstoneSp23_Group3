using Moq;
using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestMealPlanService {
    public class TestGetNextWeeksMealPlan {

        [Fact]
        public void TestValidRequest() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.GetMealPlanMeals(It.IsAny<int>())).Returns(new List<Meal>());
            mealPlanDao.Setup(x => x.GetMealPlan(It.IsAny<GetMealPlanRequest>())).Returns(new MealPlan());
            var service = new MealPlanService(mealPlanDao.Object);

            var result = service.GetNextWeeksMealPlan(1);

            Assert.NotNull(result);
        }
    }
}
