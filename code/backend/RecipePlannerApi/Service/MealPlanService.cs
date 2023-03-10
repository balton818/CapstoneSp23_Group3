using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
    public class MealPlanService : IMealPlanService {

        private IMealPlanDao mealPlanDao;

        public MealPlanService(IMealPlanDao mealPlanDao) {
            this.mealPlanDao = mealPlanDao;
        }

        public MealPlan GetMealPlan(GetMealPlanRequest request) {
            var mealPlan = this.mealPlanDao.GetMealPlan(request);

            if (mealPlan == null) {
                mealPlan = this.mealPlanDao.CreateMealPlan(request);
            }

            if(mealPlan.MealPlanId == null) {
                throw new MissingFieldException("meal plan is missing meal plan id from database");
            }

            var meals = this.mealPlanDao.GetMealPlanMeals(mealPlan.MealPlanId.Value);

            foreach(var day in Enum.GetValues(typeof(DayOfWeek))) {
                var dayMeals = meals.Where(meal => meal.DayOfWeek == (DayOfWeek)day).ToList();
                mealPlan.meals.Add((DayOfWeek)day, dayMeals);
            }

            return mealPlan;
        }
        public MealPlan GetThisWeeksMealPlan(int userId) {
            var date = DateTime.Now;
            return this.GetMealPlan(new GetMealPlanRequest { Date = date, UserId = userId });
        }
        public MealPlan GetNextWeeksMealPlan(int userId) {
            var date = DateTime.Now.AddDays(7);
            return this.GetMealPlan(new GetMealPlanRequest { Date = date, UserId = userId });
        }

        public Meal AddMeal(Meal meal) {
            return this.mealPlanDao.CreateMeal(meal);
        }

        public Meal UpdateMeal(Meal meal) {
            return this.mealPlanDao.UpdateMeal(meal);
        }

        public void RemoveMeal(int mealId) {
            this.mealPlanDao.RemoveMeal(mealId);
        }
    }
}
