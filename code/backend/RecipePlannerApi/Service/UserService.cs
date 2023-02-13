using RecipePlannerApi.Dao;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service {
    public static class UserService {

        /// <summary>Initializes the <see cref="UserService" /> class.</summary>
        static UserService() { }

        /// <summary>Validates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static int? ValidateUser(User user) {
            return UserDao.ValidateUser(user)?.Id;
        }

        /// <summary>Creates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static int? CreateUser(User user) {
            return UserDao.CreateUser(user);
        }


        /// <summary>Adds the pantry item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The added pantry item</returns>
        /// <exception cref="System.ArgumentNullException">pantry item cannot be null</exception>
        /// <exception cref="System.ArgumentException">ingredient name cannot be null or empty and must be less than or equal to 20 characters
        /// or
        /// user id cannot be null</exception>
        public static PantryItem AddPantryItem(PantryItem item) {
            if(item == null) {
                throw new ArgumentNullException("pantry item cannot be null");
            }

            if (item.IngredientName == null || item.IngredientName.Length == 0 || item.IngredientName.Length >= 20) {
                throw new ArgumentException("ingredient name cannot be null or empty and must be less than or equal to 20 characters");
            }

            if(item.UserId == null) {
                throw new ArgumentException("user id cannot be null");
            }

            return PantryDao.AddPantryItem(item);
        }


        /// <summary>Gets the user pantry.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The pantry for the user</returns>
        public static List<PantryItem> GetUserPantry(int userId) {
            return PantryDao.GetUserPantry(userId);
        }


        /// <summary>Updates the pantry item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The updated pantry item</returns>
        /// <exception cref="System.ArgumentException">ingredient name cannot be null or empty and must be less than or equal to 20 characters
        /// or
        /// pantry id cannot be null</exception>
        public static PantryItem UpdatePantryItem(PantryItem item) {
            if (item.IngredientName == null || item.IngredientName.Length == 0 || item.IngredientName.Length >= 20) {
                throw new ArgumentException("ingredient name cannot be null or empty and must be less than or equal to 20 characters");
            }

            if (item.PantryId == null) {
                throw new ArgumentException("pantry id cannot be null");
            }

            return PantryDao.UpdatePantryItem(item);
        }


        /// <summary>Removes the pantry item.</summary>
        /// <param name="pantry_id">The pantry identifier.</param>
        public static void RemovePantryItem(int pantry_id) {
            PantryDao.RemovePantryItem(pantry_id);
        }
    }
}
