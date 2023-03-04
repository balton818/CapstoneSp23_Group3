using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service.Interface
{
    public interface IMealPlanService
    {
        MealPlan GetMealPlan(GetMealPlanRequest request);
    }
}