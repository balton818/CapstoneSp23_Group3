using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Dao.Request;
using RecipePlannerApi.Model;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Dao
{

    [ExcludeFromCodeCoverage]
    public class UserDao: Dao, IUserDao {

        public IdDto ValidateUser(string username, string password) {
            CommandUpdate cmd = c => {
                c.CommandType = CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@username", username);
                c.Parameters.AddWithValue("@password", password);
            };

            return execute<IdDto>("dbo.validate_user", cmd).FirstOrDefault();
        }

        public int? CreateUser(User user) {
            CommandUpdate cmd = c => {
                c.CommandType = CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@username", user.Username);
                c.Parameters.AddWithValue("@password", user.Password);
                c.Parameters.AddWithValue("@first_name", user.FirstName);
                c.Parameters.AddWithValue("@last_name", user.LastName);
                c.Parameters.AddWithValue("@email", user.Email);
            };

            return execute<IdDto>("dbo.create_user", cmd).FirstOrDefault()?.Id;
        }
    }
}
