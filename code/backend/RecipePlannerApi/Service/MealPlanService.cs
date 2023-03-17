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
            if(request == null) {
                throw new ArgumentNullException("request cannot be null");
            }

            var mealPlan = this.mealPlanDao.GetMealPlan(request);

            if (mealPlan == null) {
                mealPlan = this.mealPlanDao.CreateMealPlan(request);
            }

            if(mealPlan == null) {
                throw new ArgumentNullException("error getting or creating meal plan");
            }

            if(mealPlan.MealPlanId == null) {
                throw new MissingFieldException("meal plan id is missing from meal plan object");
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
            this.ValidateAddMeal(meal);
            return this.mealPlanDao.CreateMeal(meal);
        }

        public Meal UpdateMeal(Meal meal) {
            this.ValidateUpdateMeal(meal);
            return this.mealPlanDao.UpdateMeal(meal);
        }

        public void RemoveMeal(int mealId) {
            if(mealId == 0) {
                throw new ArgumentException("meal id must be greater that 0");
            }

            this.mealPlanDao.RemoveMeal(mealId);
        }

        private void ValidateAddMeal(Meal meal) {
            if (meal == null) {
                throw new ArgumentNullException("meal cannot be null");
            }

            if (meal.MealPlanId == 0) {
                throw new ArgumentException("meal plan Id must be greater than 0");
            }

            if (!Enum.IsDefined(meal.MealType)) {
                throw new ArgumentException(meal.MealType + " is not a valid meal type");
            }

            if (!Enum.IsDefined(meal.DayOfWeek)) {
                throw new ArgumentException(meal.DayOfWeek + " is not a valid day of the week");
            }

            if(meal.Recipe == null) {
                throw new ArgumentNullException("recipe cannot be null");
            }

            if (string.IsNullOrEmpty(meal.Recipe.ImageType)) {
                throw new ArgumentException("image type cannot be null or empty");
            }

            if (string.IsNullOrEmpty(meal.Recipe.Image)) {
                throw new ArgumentException("image cannot be null or empty");
            }

            if (string.IsNullOrEmpty(meal.Recipe.Title)) {
                throw new ArgumentException("title cannot be null or empty");
            }
        }

        private void ValidateUpdateMeal(Meal meal) {
            if (meal == null) {
                throw new ArgumentNullException("meal cannot be null");
            }

            if (meal.MealId == 0) {
                throw new ArgumentException("meal plan Id must be greater than 0");
            }

            if (!Enum.IsDefined(meal.MealType)) {
                throw new ArgumentException(meal.MealType + " is not a valid meal type");
            }

            if (!Enum.IsDefined(meal.DayOfWeek)) {
                throw new ArgumentException(meal.DayOfWeek + " is not a valid day of the week");
            }

            if (meal.Recipe == null) {
                throw new ArgumentNullException("recipe cannot be null");
            }

            if (string.IsNullOrEmpty(meal.Recipe.ImageType)) {
                throw new ArgumentException("image type cannot be null or empty");
            }

            if (string.IsNullOrEmpty(meal.Recipe.Image)) {
                throw new ArgumentException("image cannot be null or empty");
            }

            if (string.IsNullOrEmpty(meal.Recipe.Title)) {
                throw new ArgumentException("title cannot be null or empty");
            }
        }
    }
}
