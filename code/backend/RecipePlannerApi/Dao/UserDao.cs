using System.Data.SqlClient;
using AutoMapper;
using System.Data;
using RecipePlannerApi.Model;
using Microsoft.Extensions.Configuration;
using RecipePlannerApi.Dao.Request;
using AutoMapper.Data;

namespace RecipePlannerApi.Dao {
    public class UserDao: Dao {

        public delegate void CommandUpdate(SqlCommand cmd);

        public static IdDto ValidateUser(User user) {
            CommandUpdate cmd = c => {
                c.CommandType = CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@username", user.Username);
                c.Parameters.AddWithValue("@password", user.Password);
            };

            return execute<IdDto>("dbo.validate_user", cmd).FirstOrDefault();
        }

        public static int? CreateUser(User user) {
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
