using RecipePlannerApi.Dao;

namespace RecipePlannerApi.Service {
    public class AppService: IAppService {
        private readonly IAppDao _appDao;

        public AppService(IAppDao appDao) {
            this._appDao = appDao;
        }

        public List<string> getAppCuisines() {
            return this._appDao.getAppCuisines();
        }

        public List<string> getAppDiets() {
            return this._appDao.getAppDiets();
        }

        public List<string> getAppMealTypes() {
            return this._appDao.getAppMealTypes();
        }
    }

    public interface IAppService {
        public List<string> getAppCuisines();

        public List<string> getAppDiets();

        public List<string> getAppMealTypes();
    }
}
