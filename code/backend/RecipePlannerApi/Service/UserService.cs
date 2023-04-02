using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
    public class UserService: IUserService {
        private readonly IUserDao _userDao;
        private readonly IPantryDao _pantryDao;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userDao">The user DAO.</param>
        /// <param name="pantryDao">The pantry DAO.</param>
        public UserService(IUserDao userDao, IPantryDao pantryDao) {
            this._userDao = userDao;
            this._pantryDao = pantryDao;
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns><br /></returns>
        public int? ValidateUser(string username, string password) {
            return this._userDao.ValidateUser(username, password)?.Id;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><br /></returns>
        public int? CreateUser(User user) {
            return this._userDao.CreateUser(user);
        }


        /// <summary>
        /// Adds the pantry item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The added pantry item</returns>
        /// <exception cref="System.ArgumentNullException">pantry item cannot be null</exception>
        /// <exception cref="System.ArgumentException">ingredient name cannot be null or empty and must be less than or equal to 40 characters</exception>
        /// <exception cref="System.ArgumentException">user id cannot be null or zero</exception>
        public PantryItem AddPantryItem(PantryItem item) {
            if(item == null) {
                throw new ArgumentNullException("pantry item cannot be null");
            }

            if (item.IngredientName == null || item.IngredientName.Length == 0 || item.IngredientName.Length >= 40) {
                throw new ArgumentException("ingredient name cannot be null or empty and must be less than or equal to 40 characters");
            }

            if(item.UserId == null || item.UserId <= 0) {
                throw new ArgumentException("user id cannot be null or zero");
            }

            return this._pantryDao.AddPantryItem(item);
        }


        /// <summary>
        /// Gets the user pantry.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The pantry for the user</returns>
        /// <exception cref="System.ArgumentException">user id cannot be less than 1</exception>
        public List<PantryItem> GetUserPantry(int userId) {
            if (userId <= 0) {
                throw new ArgumentException("user id cannot be less than 1");
            }

            return this._pantryDao.GetUserPantry(userId);
        }


        /// <summary>
        /// Updates the pantry item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The updated pantry item</returns>
        /// <exception cref="System.ArgumentNullException">pantry item cannot be null</exception>
        /// <exception cref="System.ArgumentException">ingredient name cannot be null or empty and must be less than or equal to 20 characters</exception>
        /// <exception cref="System.ArgumentException">pantry id cannot be null</exception>
        public PantryItem UpdatePantryItem(PantryItem item) {
            if (item == null) {
                throw new ArgumentNullException("pantry item cannot be null");
            }

            if (item.IngredientName == null || item.IngredientName.Length == 0 || item.IngredientName.Length >= 40) {
                throw new ArgumentException("ingredient name cannot be null or empty and must be less than or equal to 40 characters");
            }

            if (item.PantryId == null) {
                throw new ArgumentException("pantry id cannot be null");
            }

            return this._pantryDao.UpdatePantryItem(item);
        }

        public List<PantryItem> UpdatePantryItems(List<PantryItem> items, int userId) {
            if (items == null) {
                throw new ArgumentNullException("items list cannot be null");
            }

            foreach (var item in items) {
                if(item.PantryId == null) {
                    AddPantryItem(item);
                } else {
                    UpdatePantryItem(item);
                }
            }

            return GetUserPantry(userId);
        }

        /// <summary>
        /// Removes the pantry item.
        /// </summary>
        /// <param name="pantryId">The pantry identifier.</param>
        /// <exception cref="System.ArgumentException">pantry id cannot be less than 1</exception>
        public void RemovePantryItem(int pantryId) {
            if (pantryId <= 0) {
                throw new ArgumentException("pantry id cannot be less than 1");
            }

            this._pantryDao.RemovePantryItem(pantryId);
        }
    }
}
