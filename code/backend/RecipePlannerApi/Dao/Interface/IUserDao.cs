using RecipePlannerApi.Dao.Request;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao.Interface
{
    public interface IUserDao
    {
        public IdDto ValidateUser(string username, string password);
        public int? CreateUser(User user);
    }
}
