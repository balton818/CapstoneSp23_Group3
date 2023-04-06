using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service.Interface
{
    public interface IShoppingListService
    {
        List<ShoppingListIngredient> AddToShoppingList(List<ShoppingListIngredient> shoppingList, int userId);
        void DeleteShoppingListIngredient(int shoppingListId);
        List<ShoppingListIngredient> GetShoppingList(int userId);
        void DeleteAllFromShoppingList(List<ShoppingListIngredient> ingredients);
        ShoppingListIngredient UpsertShoppingListIngredient(ShoppingListIngredient ingredient);
    }
}