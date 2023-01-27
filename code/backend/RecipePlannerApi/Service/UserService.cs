using RecipePlannerApi.Dao;
using RecipePlannerApi.Model;
using System.Runtime.CompilerServices;

namespace RecipePlannerApi.Service {
    public static class UserService {

        static UserService() { }

        public static bool ValidateUser(User user) {
            return UserDao.ValidateUser(user) != null;
        }
    }
}
