using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service;

namespace RecipePlannerApiTests.TestServices.TestMealPlanService {
    public class TestRemoveMeal {
        [Fact]
        public void TestValidId() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.RemoveMeal(It.IsAny<int>()));
            var service = new MealPlanService(mealPlanDao.Object);

            service.RemoveMeal(1);

            mealPlanDao.Verify(dao => dao.RemoveMeal(It.IsAny<int>()));
        }
        [Fact]
        public void TestIdZero() {
            var mealPlanDao = new Mock<IMealPlanDao>();

            mealPlanDao.Setup(x => x.RemoveMeal(It.IsAny<int>()));
            var service = new MealPlanService(mealPlanDao.Object);

            Assert.Throws<ArgumentException>(() => service.RemoveMeal(0));
        }
    }
}
