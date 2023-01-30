using System.Data.SqlClient;
using AutoMapper;
using System.Data;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao {
    public class UserDao {

        public delegate void CommandUpdate(SqlCommand cmd);

        public static string? ValidateUser(User user) {
            CommandUpdate cmd = c => {
                c.CommandType = CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@username", user.Username);
                c.Parameters.AddWithValue("@password", user.Password);
            };

            return execute<string>("dbo.validate_user", cmd).FirstOrDefault();
        }

        public static List<T> execute<T>(string storedProc, CommandUpdate updateCmd) {

            var mapper = new MapperConfiguration(config => {
                config.CreateMap<IDataReader, User>();
            }).CreateMapper();

            using(SqlConnection conn = new SqlConnection(Connection.ConnectionString)) {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(storedProc, conn)) {
                    updateCmd(cmd);
                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.HasRows) {
                            return mapper.Map<IDataReader, List<T>>(reader);
                        }
                    }
                }
            }

            return new List<T>();
        }
    }
}
