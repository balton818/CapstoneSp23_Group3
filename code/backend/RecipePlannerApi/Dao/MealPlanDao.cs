using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao
{
    public class MealPlanDao : Dao, IMealPlanDao {

        public MealPlan CreateMealPlan(GetMealPlanRequest request) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@mealPlanDate", request.Date);
                c.Parameters.AddWithValue("@userId", request.UserId);
            };

            return execute<MealPlan>("create_meal_plan", cmd).FirstOrDefault();
        }

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

            return execute<Meal>("create_meal", cmd).FirstOrDefault();
        }

        public MealPlan GetMealPlan(GetMealPlanRequest request) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@date", request.Date);
                c.Parameters.AddWithValue("@userId", request.UserId);
            };

            return execute<MealPlan>("get_meal_plan", cmd).FirstOrDefault();
        }

        public List<Meal> GetMealsbyDate(int mealPlanId) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@mealPlanId", mealPlanId);
            };

            return execute<Meal>("get_meal_plan_meals", cmd);
        }

        public void RemoveMeal(int mealId) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@mealId", mealId);
            };

            execute<Meal>("remove_meal", cmd);
        }

        public Meal UpdateMealPlan(Meal meal) {
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

            return execute<Meal>("update_meal", cmd).FirstOrDefault();
        }




    }
}
