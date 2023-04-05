using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao.Interface
{
    public interface IShoppingListDao
    {
        void DeleteShoppingListIngredient(int shoppingListId);
        List<ShoppingListIngredient> GetShoppingList(int userId);
        ShoppingListIngredient UpsertShoppingListIngredient(ShoppingListIngredient item);
    }
}