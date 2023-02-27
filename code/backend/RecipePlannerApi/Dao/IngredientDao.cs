using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Dao
{

    [ExcludeFromCodeCoverage]
    public class IngredientDao: Dao, IIngredientDao {

        /// <summary>Searches the ingredient.</summary>
        /// <param name="search">The search.</param>
        /// <returns>List of matching ingredients</returns>
        public List<Ingredient> SearchIngredient(string search) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@search", search);
            };

            return execute<List<Ingredient>>("search_ingredient", cmd).FirstOrDefault();
        }
    }
}
