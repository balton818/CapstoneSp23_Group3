using RecipePlannerApi.Dao;
using RecipePlannerApi.Model;
using System.Runtime.CompilerServices;

namespace RecipePlannerApi.Service {
    public static class UserService {

        /// <summary>Initializes the <see cref="UserService" /> class.</summary>
        static UserService() { }

        /// <summary>Validates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static bool ValidateUser(User user) {
            return UserDao.ValidateUser(user) != null;
        }

        /// <summary>Creates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static int? CreateUser(User user) {
            return UserDao.CreateUser(user);
        }
    }
}
