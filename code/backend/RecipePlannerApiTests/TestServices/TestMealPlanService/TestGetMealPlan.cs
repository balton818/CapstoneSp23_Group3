using Moq;
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
            mealPlanDao.Setup(x => x.GetMealPlan(It.IsAny<GetMealPlanRequest>())).Returns(new MealPlan { MealPlanId = 1});
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

        [Fact]
        public void TestNullMealPlanReturned() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.GetMealPlanMeals(It.IsAny<int>())).Returns(new List<Meal>());
            mealPlanDao.Setup(x => x.GetMealPlan(It.IsAny<GetMealPlanRequest>())).Returns(() => null); 
            mealPlanDao.Setup(x => x.CreateMealPlan(It.IsAny<GetMealPlanRequest>())).Returns(() => null);
            var service = new MealPlanService(mealPlanDao.Object);

            Assert.Throws<ArgumentNullException>(() => service.GetMealPlan(new GetMealPlanRequest { Date = DateTime.Parse("05/04/13"), UserId = 1 }));
        }

        [Fact]
        public void TestNullMealPlanIdReturned() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.GetMealPlanMeals(It.IsAny<int>())).Returns(new List<Meal>());
            mealPlanDao.Setup(x => x.GetMealPlan(It.IsAny<GetMealPlanRequest>())).Returns(new MealPlan());
            var service = new MealPlanService(mealPlanDao.Object);

            Assert.Throws<MissingFieldException>(() => service.GetMealPlan(new GetMealPlanRequest { Date = DateTime.Parse("05/04/13"), UserId = 1 }));
        }
    }
}
