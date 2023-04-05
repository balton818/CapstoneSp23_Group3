using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao.Interface
{
    /// <summary>
    /// Interface IMealPlanDao
    /// </summary>
    public interface IMealPlanDao
    {
        /// <summary>
        /// Creates the meal.
        /// </summary>
        /// <param name="meal">The meal.</param>
        /// <returns>The created Meal.</returns>
        Meal CreateMeal(Meal meal);
        /// <summary>
        /// Creates the meal plan.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The created MealPlan.</returns>
        MealPlan CreateMealPlan(GetMealPlanRequest request);
        /// <summary>
        /// Gets the meal plan.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The MealPlan.</returns>
        MealPlan GetMealPlan(GetMealPlanRequest request);
        /// <summary>
        /// Gets the meal plan meals.
        /// </summary>
        /// <param name="mealPlanId">The meal plan identifier.</param>
        /// <returns>The list of Meals.</returns>
        List<Meal> GetMealPlanMeals(int mealPlanId);
        List<int> GetMealPlanRecipes(int mealPlanId);

        /// <summary>
        /// Removes the meal with the matching meal plan id.
        /// </summary>
        /// <param name="mealId">The meal identifier.</param>
        void RemoveMeal(int mealId);
        /// <summary>
        /// Updates the meal.
        /// </summary>
        /// <param name="meal">The meal.</param>
        /// <returns>The updated Meal.</returns>
        Meal UpdateMeal(Meal meal);
    }
}