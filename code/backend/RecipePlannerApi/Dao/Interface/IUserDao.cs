using RecipePlannerApi.Dao.Request;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao.Interface
{
    public interface IUserDao
    {
        public IdDto ValidateUser(User user);
        public int? CreateUser(User user);
    }
}
