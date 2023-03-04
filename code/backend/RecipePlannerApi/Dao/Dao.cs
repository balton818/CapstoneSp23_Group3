using AutoMapper;
using System.Data.SqlClient;
using System.Data;
using AutoMapper.Data;
using RecipePlannerApi.Model;
using RecipePlannerApi.Dao.Request;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Dao {

    [ExcludeFromCodeCoverage]
    public abstract class Dao {
        public delegate void CommandUpdate(SqlCommand cmd);
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
                config.CreateMap<IDataReader, Meal>().ForMember(meal => meal.dayOfWeek, d => d.AddTransform(d => d - 1))
                .ForMember(meal => meal.recipe,
                    r => r.MapFrom(reader => new Recipe 
                    { 
                        Id = (int?)reader["RecipeId"], 
                        Image = reader["Image"].ToString(), 
                        ImageType = reader["ImageType"].ToString(), 
                        Title = reader["Title"].ToString() 
                    }));
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
