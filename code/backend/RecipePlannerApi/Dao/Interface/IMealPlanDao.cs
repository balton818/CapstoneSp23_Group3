using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao.Interface
{
    public interface IMealPlanDao
    {
        Meal CreateMeal(Meal meal);
        MealPlan CreateMealPlan(GetMealPlanRequest request);
        MealPlan GetMealPlan(GetMealPlanRequest request);
        List<Meal> GetMealPlanMeals(int mealPlanId);
        void RemoveMeal(int mealId);
        Meal UpdateMeal(Meal meal);
    }
}