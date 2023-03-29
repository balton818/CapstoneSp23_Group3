using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
    public class AppService: IAppService {
        /// <summary>
        /// The application DAO
        /// </summary>
        private readonly IAppDao _appDao;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppService"/> class.
        /// </summary>
        /// <param name="appDao">The application DAO.</param>
        public AppService(IAppDao appDao) {
            this._appDao = appDao;
        }

        /// <summary>
        /// Gets the application's cuisines.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> getAppCuisines() {
            return this._appDao.getAppCuisines();
        }

        /// <summary>
        /// Gets the application's diets.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> getAppDiets() {
            return this._appDao.getAppDiets();
        }

        /// <summary>
        /// Gets the application's meal types.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> getAppMealTypes() {
            return this._appDao.getAppMealTypes();
        }
    }
}
