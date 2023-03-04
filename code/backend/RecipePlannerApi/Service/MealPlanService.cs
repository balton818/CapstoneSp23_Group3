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

            return mealPlan;
        }
    }
}
