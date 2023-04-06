using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Dao
{
    [ExcludeFromCodeCoverage]
    public class ShoppingListDao : Dao, IShoppingListDao {
        public ShoppingListIngredient UpsertShoppingListIngredient(ShoppingListIngredient item) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@userId", item.UserId);
                c.Parameters.AddWithValue("@shoppingListId", item.ShoppingListId);
                c.Parameters.AddWithValue("@ingredientName", item.IngredientName);
                c.Parameters.AddWithValue("@ingredientId", item.IngredientId);
                c.Parameters.AddWithValue("@quantity", item.Quantity);
                c.Parameters.AddWithValue("@unitId", (int)item.UnitId);
            };

            return execute<ShoppingListIngredient>("upsert_shopping_list_ingredient", cmd).FirstOrDefault();
        }

        public void DeleteShoppingListIngredient(int shoppingListId) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@shoppingListId", shoppingListId);
            };

            execute<ShoppingListIngredient>("delete_shopping_list_ingredient", cmd);
        }

        public List<ShoppingListIngredient> GetShoppingList(int userId) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@userId", userId);
            };

            return execute<ShoppingListIngredient>("get_shopping_list", cmd);
        }
    }
}
