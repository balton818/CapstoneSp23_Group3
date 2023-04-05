using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Dao.Request;
using System.Diagnostics.CodeAnalysis;
using static RecipePlannerApi.Dao.Dao;
using static RecipePlannerApi.Dao.UserDao;

namespace RecipePlannerApi.Dao
{
    [ExcludeFromCodeCoverage]
    public class AppDao: Dao, IAppDao {
        /// <summary>
        /// Gets the list application cuisines from the database.
        /// </summary>
        /// <returns>List of cuisines</returns>
        public List<string> getAppCuisines() {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
            };

            return execute<StringDto>("get_cuisines", cmd).Select(s => s.Value).ToList();

        }

        /// <summary>
        /// Gets the list application diets from the database.
        /// </summary>
        /// <returns>List of diets</returns>
        public List<string> getAppDiets() {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
            };

            return execute<StringDto>("get_diets", cmd).Select(s => s.Value).ToList();

        }

        /// <summary>
        /// Gets the list application meal types from the database.
        /// </summary>
        /// <returns>List of meal types</returns>
        public List<string> getAppMealTypes() {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
            };

            return execute<StringDto>("get_meal_types", cmd).Select(s => s.Value).ToList();

        }
    }
}
