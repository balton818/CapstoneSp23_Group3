using RecipePlannerApi.Model;

namespace RecipePlannerApi.Service.Interface
{
    public interface IUserService
    {
        /// <summary>Validates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public int? ValidateUser(User user);

        /// <summary>Creates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public int? CreateUser(User user);


        /// <summary>Adds the pantry item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The added pantry item</returns>
        /// <exception cref="ArgumentNullException">pantry item cannot be null</exception>
        /// <exception cref="ArgumentException">ingredient name cannot be null or empty and must be less than or equal to 20 characters
        /// or
        /// user id cannot be null</exception>
        public PantryItem AddPantryItem(PantryItem item);


        /// <summary>Gets the user pantry.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The pantry for the user</returns>
        public List<PantryItem> GetUserPantry(int userId);


        /// <summary>Updates the pantry item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The updated pantry item</returns>
        /// <exception cref="ArgumentException">ingredient name cannot be null or empty and must be less than or equal to 20 characters
        /// or
        /// pantry id cannot be null</exception>
        public PantryItem UpdatePantryItem(PantryItem item);


        /// <summary>Removes the pantry item.</summary>
        /// <param name="pantry_id">The pantry identifier.</param>
        public void RemovePantryItem(int pantry_id);
    }
}
