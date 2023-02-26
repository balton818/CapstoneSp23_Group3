using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao
{
    public class PantryDao: Dao, IPantryDao {

        /// <summary>Adds the pantry item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The added pantry item</returns>
        public PantryItem AddPantryItem(PantryItem item) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@userId", item.UserId);
                c.Parameters.AddWithValue("@ingredientId", item.IngredientId);
                c.Parameters.AddWithValue("@ingredientName", item.IngredientName);
                c.Parameters.AddWithValue("@quantity", item.Quantity);
                c.Parameters.AddWithValue("@unitId", (int)item.Unit);
            };

            return execute<PantryItem>("add_pantry_item", cmd).FirstOrDefault();
        }


        /// <summary>Gets the pantry item.</summary>
        /// <param name="pantry_id">The pantry identifier.</param>
        /// <returns>The pantry item</returns>
        public PantryItem GetPantryItem(int pantry_id) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@pantryId", pantry_id);
            };

            return execute<PantryItem>("get_pantry_item", cmd).FirstOrDefault();
        }


        /// <summary>Gets the user pantry.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user's pantry</returns>
        public List<PantryItem> GetUserPantry(int userId) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@userId", userId);
            };

            return execute<PantryItem>("get_user_pantry", cmd);
        }

        /// <summary>Updates the pantry item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The updated pantry item</returns>
        public PantryItem UpdatePantryItem(PantryItem item) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@pantryId", item.PantryId);
                c.Parameters.AddWithValue("@ingredientId", item.IngredientId);
                c.Parameters.AddWithValue("@ingredientName", item.IngredientName);
                c.Parameters.AddWithValue("@quantity", item.Quantity);
                c.Parameters.AddWithValue("@unitId", (int)item.Unit);
            };

            return execute<PantryItem>("update_pantry_item", cmd).FirstOrDefault();
        }


        /// <summary>Removes the pantry item.</summary>
        /// <param name="pantry_id">The pantry identifier.</param>
        public void RemovePantryItem(int pantry_id) {
            CommandUpdate cmd = c => {
                c.CommandType = System.Data.CommandType.StoredProcedure;
                c.Parameters.AddWithValue("@pantryId", pantry_id);
            };

            execute<PantryItem>("remove_pantry_item", cmd).FirstOrDefault();
        }
    }
}
