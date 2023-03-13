using Moq;
using RecipePlannerApi.Api.Interface;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;
using RecipePlannerApi.Controllers.Requests;

namespace RecipePlannerApiTests.TestServices.TestMealPlanService {
    public class TestGetMealPlan {

        [Fact]
        public void TestValidRequest() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.GetMealPlanMeals(It.IsAny<int>())).Returns(new List<Meal>());
            mealPlanDao.Setup(x => x.GetMealPlan(It.IsAny<GetMealPlanRequest>())).Returns(new MealPlan());
            var service = new MealPlanService(mealPlanDao.Object);

            var result = service.GetMealPlan(new GetMealPlanRequest { Date = DateTime.Parse("05/04/13"), UserId = 1 });

            Assert.NotNull(result);
        }

        [Fact]
        public void TestNullRequest() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.GetMealPlanMeals(It.IsAny<int>())).Returns(new List<Meal>());
            mealPlanDao.Setup(x => x.GetMealPlan(It.IsAny<GetMealPlanRequest>())).Returns(new MealPlan());
            var service = new MealPlanService(mealPlanDao.Object);

            Assert.Throws<ArgumentNullException>(() => service.GetMealPlan(null));
        }
    }
}
