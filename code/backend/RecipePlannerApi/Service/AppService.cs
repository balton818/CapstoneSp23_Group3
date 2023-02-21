using RecipePlannerApi.Dao;

namespace RecipePlannerApi.Service {
    public class AppService {
        public static List<string> getAppCuisines() {
            return AppDao.getAppCuisines();
        }

        public static List<string> getAppDiets() {
            return AppDao.getAppDiets();
        }

        public static List<string> getAppMealTypes() {
            return AppDao.getAppMealTypes();
        }
    }
}
