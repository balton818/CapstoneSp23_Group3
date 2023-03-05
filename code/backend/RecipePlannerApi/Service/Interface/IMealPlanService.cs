using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service.Interface
{
    public interface IMealPlanService
    {
        Meal AddMeal(Meal meal);
        MealPlan GetMealPlan(GetMealPlanRequest request);
    }
}