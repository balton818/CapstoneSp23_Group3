using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao.Interface
{
    public interface IIngredientDao
    {
        /// <summary>Searches the ingredients in the database based on search string.</summary>
        /// <param name="search">The search query.</param>
        /// <returns>List of matching ingredients</returns>
        public List<Ingredient> SearchIngredient(string search);
    }
}
