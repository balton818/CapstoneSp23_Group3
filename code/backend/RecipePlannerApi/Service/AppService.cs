using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
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
}
