using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao.Interface
{
    public interface IPantryDao
    {
        /// <summary>Adds the pantry item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The added pantry item</returns>
        public PantryItem AddPantryItem(PantryItem item);


        /// <summary>Gets the pantry item.</summary>
        /// <param name="pantry_id">The pantry identifier.</param>
        /// <returns>The pantry item</returns>
        public PantryItem GetPantryItem(int pantry_id);


        /// <summary>Gets the user pantry.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user's pantry</returns>
        public List<PantryItem> GetUserPantry(int userId);

        /// <summary>Updates the pantry item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The updated pantry item</returns>
        public PantryItem UpdatePantryItem(PantryItem item);


        /// <summary>Removes the pantry item.</summary>
        /// <param name="pantry_id">The pantry identifier.</param>
        public void RemovePantryItem(int pantry_id);
    }
}
