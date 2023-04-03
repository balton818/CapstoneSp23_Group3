using AutoMapper;
using System.Data.SqlClient;
using System.Data;
using AutoMapper.Data;
using RecipePlannerApi.Model;
using RecipePlannerApi.Dao.Request;
using System.Diagnostics.CodeAnalysis;
using RecipePlannerApi.Dao.Dto;

namespace RecipePlannerApi.Dao {

    [ExcludeFromCodeCoverage]
    public abstract class Dao {
        public delegate void CommandUpdate(SqlCommand cmd);

        /// <summary>Executes the specified stored proc.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProc">The stored proc.</param>
        /// <param name="updateCmd">The update command.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> execute<T>(string storedProc, CommandUpdate updateCmd) {

            var mapper = new MapperConfiguration(config => {
                config.AddDataReaderMapping();
                config.CreateMap<IDataReader, User>();
                config.CreateMap<IDataReader, int>();
                config.CreateMap<IDataReader, IdDto>();
                config.CreateMap<IDataReader, PantryItem>();
                config.CreateMap<IDataReader, Ingredient>();
                config.CreateMap<IDataReader, StringDto>();
                config.CreateMap<StringDto, string>();
                config.CreateMap<IDataReader, MealPlan>().ForMember(mealPlan => mealPlan.meals, m => m.Ignore());
                config.CreateMap<IDataReader, MealDto>().ForMember(meal => meal.DayOfWeek, d => d.AddTransform(d => d - 1));
                config.CreateMap<IDataReader, ShoppingListIngredient>();
            }).CreateMapper();

            using (SqlConnection conn = new SqlConnection(Connection.ConnectionString)) {
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
