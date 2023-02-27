using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao.Interface
{
    public interface IIngredientDao
    {
        public List<Ingredient> SearchIngredient(string search);
    }
}
