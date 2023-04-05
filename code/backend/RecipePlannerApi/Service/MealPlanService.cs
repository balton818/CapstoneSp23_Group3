using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
    public class MealPlanService : IMealPlanService {

        /// <summary>
        /// The meal plan DAO
        /// </summary>
        private IMealPlanDao mealPlanDao;

        /// <summary>
        /// Initializes a new instance of the <see cref="MealPlanService"/> class.
        /// </summary>
        /// <param name="mealPlanDao">The meal plan DAO.</param>
        public MealPlanService(IMealPlanDao mealPlanDao) {
            this.mealPlanDao = mealPlanDao;
        }

        /// <summary>
        /// Gets the meal plan.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>MealPlan.</returns>
        /// <exception cref="System.ArgumentNullException">request cannot be null</exception>
        /// <exception cref="System.ArgumentNullException">error getting or creating meal plan</exception>
        /// <exception cref="System.MissingFieldException">meal plan id is missing from meal plan object</exception>
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

            mealPlan.Recipes = this.mealPlanDao.GetMealPlanRecipes(mealPlan.MealPlanId.Value);

            return mealPlan;
        }
        /// <summary>
        /// Gets the this weeks meal plan.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>MealPlan.</returns>
        public MealPlan GetThisWeeksMealPlan(int userId) {
            var date = DateTime.Now;
            return this.GetMealPlan(new GetMealPlanRequest { Date = date, UserId = userId });
        }
        /// <summary>
        /// Gets the next weeks meal plan.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>MealPlan.</returns>
        public MealPlan GetNextWeeksMealPlan(int userId) {
            var date = DateTime.Now.AddDays(7);
            return this.GetMealPlan(new GetMealPlanRequest { Date = date, UserId = userId });
        }

        /// <summary>
        /// Adds the meal.
        /// </summary>
        /// <param name="meal">The meal.</param>
        /// <returns>Meal.</returns>
        public Meal AddMeal(Meal meal) {
            this.ValidateAddMeal(meal);
            return this.mealPlanDao.CreateMeal(meal);
        }

        /// <summary>
        /// Updates the meal.
        /// </summary>
        /// <param name="meal">The meal.</param>
        /// <returns>Meal.</returns>
        public Meal UpdateMeal(Meal meal) {
            this.ValidateUpdateMeal(meal);
            return this.mealPlanDao.UpdateMeal(meal);
        }

        /// <summary>
        /// Removes the meal.
        /// </summary>
        /// <param name="mealId">The meal identifier.</param>
        /// <exception cref="System.ArgumentException">meal id must be greater that 0</exception>
        public void RemoveMeal(int mealId) {
            if(mealId == 0) {
                throw new ArgumentException("meal id must be greater that 0");
            }

            this.mealPlanDao.RemoveMeal(mealId);
        }

        /// <summary>
        /// Validates the add meal.
        /// </summary>
        /// <param name="meal">The meal.</param>
        /// <exception cref="System.ArgumentNullException">meal cannot be null</exception>
        /// <exception cref="System.ArgumentNullException">recipe cannot be null</exception>
        /// <exception cref="System.ArgumentException">meal plan Id must be greater than 0</exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentException">image type cannot be null or empty</exception>
        /// <exception cref="System.ArgumentException">image cannot be null or empty</exception>
        /// <exception cref="System.ArgumentException">title cannot be null or empty</exception>
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

        /// <summary>
        /// Validates the update meal.
        /// </summary>
        /// <param name="meal">The meal.</param>
        /// <exception cref="System.ArgumentNullException">meal cannot be null</exception>
        /// <exception cref="System.ArgumentNullException">recipe cannot be null</exception>
        /// <exception cref="System.ArgumentException">meal plan Id must be greater than 0</exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentException">image type cannot be null or empty</exception>
        /// <exception cref="System.ArgumentException">image cannot be null or empty</exception>
        /// <exception cref="System.ArgumentException">title cannot be null or empty</exception>
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
