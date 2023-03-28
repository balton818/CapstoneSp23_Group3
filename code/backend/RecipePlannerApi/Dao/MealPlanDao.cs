using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Dao.Dto;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Dao
{
    [ExcludeFromCodeCoverage]
    public class MealPlanDao : Dao, IMealPlanDao {

        /// <summary>
        /// Creates the meal plan.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The created MealPlan.</returns>
        public MealPlan CreateMealPlan(GetMealPlanRequest request) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@mealPlanDate", request.Date);
                c.Parameters.AddWithValue("@userId", request.UserId);
            };

            return execute<MealPlan>("create_meal_plan", cmd).FirstOrDefault();
        }

        /// <summary>
        /// Creates the meal.
        /// </summary>
        /// <param name="meal">The meal.</param>
        /// <returns>The created Meal.</returns>
        public Meal CreateMeal(Meal meal) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@mealPlanId", meal.MealPlanId);
                c.Parameters.AddWithValue("@dayOfWeek", meal.DayOfWeek + 1);
                c.Parameters.AddWithValue("@mealType", meal.MealType);
                c.Parameters.AddWithValue("@apiId", meal.Recipe.ApiId);
                c.Parameters.AddWithValue("@title", meal.Recipe.Title);
                c.Parameters.AddWithValue("@imageUrl", meal.Recipe.Image);
                c.Parameters.AddWithValue("@imageType", meal.Recipe.ImageType);
            };

            return new Meal(execute<MealDto>("create_meal", cmd).FirstOrDefault());
        }

        /// <summary>
        /// Gets the meal plan.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The MealPlan.</returns>
        public MealPlan GetMealPlan(GetMealPlanRequest request) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@date", request.Date);
                c.Parameters.AddWithValue("@userId", request.UserId);
            };

            return execute<MealPlan>("get_meal_plan", cmd).FirstOrDefault();
        }

        /// <summary>
        /// Gets the meal plan meals.
        /// </summary>
        /// <param name="mealPlanId">The meal plan identifier.</param>
        /// <returns>The list of Meals.</returns>
        public List<Meal> GetMealPlanMeals(int mealPlanId) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@mealPlanId", mealPlanId);
            };

            return execute<MealDto>("get_meal_plan_meals", cmd).Select(m => new Meal(m)).ToList();
        }

        /// <summary>
        /// Removes the meal with the matching meal plan id.
        /// </summary>
        /// <param name="mealId">The meal identifier.</param>
        public void RemoveMeal(int mealId) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@mealId", mealId);
            };

            execute<Meal>("remove_meal", cmd);
        }

        /// <summary>
        /// Updates the meal.
        /// </summary>
        /// <param name="meal">The meal.</param>
        /// <returns>The updated Meal.</returns>
        public Meal UpdateMeal(Meal meal) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@mealId", meal.MealId);
                c.Parameters.AddWithValue("@dayOfWeek", meal.DayOfWeek + 1);
                c.Parameters.AddWithValue("@mealType", meal.MealType);
                c.Parameters.AddWithValue("@apiId", meal.Recipe.ApiId);
                c.Parameters.AddWithValue("@title", meal.Recipe.Title);
                c.Parameters.AddWithValue("@imageUrl", meal.Recipe.Image);
                c.Parameters.AddWithValue("@imageType", meal.Recipe.ImageType);
            };

            return new Meal(execute<MealDto>("update_meal", cmd).FirstOrDefault());
        }
    }
}
