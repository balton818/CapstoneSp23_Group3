using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service.Interface
{
    /// <summary>
    /// Interface IMealPlanService
    /// </summary>
    public interface IMealPlanService
    {
        /// <summary>
        /// Adds the meal.
        /// </summary>
        /// <param name="meal">The meal.</param>
        /// <returns>Meal.</returns>
        Meal AddMeal(Meal meal);
        /// <summary>
        /// Gets the meal plan.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>MealPlan.</returns>
        MealPlan GetMealPlan(GetMealPlanRequest request);
        /// <summary>
        /// Gets the next weeks meal plan.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>MealPlan.</returns>
        MealPlan GetNextWeeksMealPlan(int userId);
        /// <summary>
        /// Gets the this weeks meal plan.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>MealPlan.</returns>
        MealPlan GetThisWeeksMealPlan(int userId);
        /// <summary>
        /// Removes the meal.
        /// </summary>
        /// <param name="mealId">The meal identifier.</param>
        void RemoveMeal(int mealId);
        /// <summary>
        /// Updates the meal.
        /// </summary>
        /// <param name="meal">The meal.</param>
        /// <returns>Meal.</returns>
        Meal UpdateMeal(Meal meal);
    }
}