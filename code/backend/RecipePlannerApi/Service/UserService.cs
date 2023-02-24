using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
    public class UserService: IUserService {
        private readonly IUserDao _userDao;
        private readonly IPantryDao _pantryDao;

        public UserService(IUserDao userDao, IPantryDao pantryDao) {
            this._userDao = userDao;
            this._pantryDao = pantryDao;
        }



        /// <summary>Initializes the <see cref="UserService" /> class.</summary>
        public UserService() { }

        /// <summary>Validates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public int? ValidateUser(User user) {
            return this._userDao.ValidateUser(user)?.Id;
        }

        /// <summary>Creates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public int? CreateUser(User user) {
            return this._userDao.CreateUser(user);
        }


        /// <summary>Adds the pantry item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The added pantry item</returns>
        /// <exception cref="System.ArgumentNullException">pantry item cannot be null</exception>
        /// <exception cref="System.ArgumentException">ingredient name cannot be null or empty and must be less than or equal to 20 characters
        /// or
        /// user id cannot be null</exception>
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


        /// <summary>Gets the user pantry.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The pantry for the user</returns>
        public List<PantryItem> GetUserPantry(int userId) {
            return this._pantryDao.GetUserPantry(userId);
        }


        /// <summary>Updates the pantry item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The updated pantry item</returns>
        /// <exception cref="System.ArgumentException">ingredient name cannot be null or empty and must be less than or equal to 20 characters
        /// or
        /// pantry id cannot be null</exception>
        public PantryItem UpdatePantryItem(PantryItem item) {
            if (item.IngredientName == null || item.IngredientName.Length == 0 || item.IngredientName.Length >= 20) {
                throw new ArgumentException("ingredient name cannot be null or empty and must be less than or equal to 20 characters");
            }

            if (item.PantryId == null) {
                throw new ArgumentException("pantry id cannot be null");
            }

            return this._pantryDao.UpdatePantryItem(item);
        }


        /// <summary>Removes the pantry item.</summary>
        /// <param name="pantry_id">The pantry identifier.</param>
        public void RemovePantryItem(int pantry_id) {
            this._pantryDao.RemovePantryItem(pantry_id);
        }
    }
}
