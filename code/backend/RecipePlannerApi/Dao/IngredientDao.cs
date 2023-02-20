using RecipePlannerApi.Model;
using static RecipePlannerApi.Dao.UserDao;

namespace RecipePlannerApi.Dao {
    public class IngredientDao: Dao {

        /// <summary>Searches the ingredient.</summary>
        /// <param name="search">The search.</param>
        /// <returns>List of matching ingredients</returns>
        public static List<Ingredient> SearchIngredient(string search) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@search", search);
            };

            return execute<List<Ingredient>>("search_ingredient", cmd).FirstOrDefault();
        }
    }
}
