using RecipePlannerApi.Dao.Request;
using static RecipePlannerApi.Dao.UserDao;

namespace RecipePlannerApi.Dao {
    public class AppDao:Dao {
        public static List<string> getAppCuisines() {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
            };

            return execute<StringDto>("get_cuisines", cmd).Select(s => s.Value).ToList();

        }

        public static List<string> getAppDiets() {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
            };

            return execute<StringDto>("get_diets", cmd).Select(s => s.Value).ToList();

        }

        public static List<string> getAppMealTypes() {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
            };

            return execute<StringDto>("get_meal_types", cmd).Select(s => s.Value).ToList();

        }
    }
}
