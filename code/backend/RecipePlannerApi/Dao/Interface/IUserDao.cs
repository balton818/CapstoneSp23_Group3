using RecipePlannerApi.Dao.Request;
using RecipePlannerApi.Model;

namespace RecipePlannerApi.Dao.Interface
{
    /// <summary>
    /// Interface IUserDao
    /// </summary>
    public interface IUserDao
    {
        /// <summary>
        /// Validates the user's username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>IdDto the user's id.</returns>
        public IdDto ValidateUser(string username, string password);
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>System.Nullable&lt;System.Int32&gt; The user's id.</returns>
        public int? CreateUser(User user);
    }
}
