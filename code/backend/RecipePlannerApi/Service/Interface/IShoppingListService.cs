using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service.Interface
{
    public interface IShoppingListService
    {
        List<ShoppingListIngredient> CompleteShoppingList(List<ShoppingListIngredient> list, int userId);
        void DeleteShoppingListIngredient(int shoppingListId);
        List<ShoppingListIngredient> GetShoppingList(int userId);
        ShoppingListIngredient UpsertShoppingListIngredient(ShoppingListIngredient ingredient);
    }
}