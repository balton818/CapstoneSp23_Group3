using System.Collections;

namespace Team3DesktopApp.Model
{
    public class User
    {
        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        public string Username { get; }

        /// <summary>Gets the first name.</summary>
        /// <value>The first name.</value>
        public string FirstName { get; private set; }
        /// <summary>Gets the last name.</summary>
        /// <value>The last name.</value>
        public string LastName { get; private set; }
        /// <summary>Gets the email.</summary>
        /// <value>The email.</value>
        public string Email { get; private set; }
        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; private set; }
        /// <summary>Gets the address.</summary>
        /// <value>The address.</value>
        public ArrayList Pantry { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="User" /> class.</summary>
        /// <param name="username">The username.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        public User(string username, string firstName, string lastName, string email, string password)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.Pantry = new ArrayList();
        }

        /// <summary>Updates the user information.</summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        public void UpdateUserInfo(string firstName, string lastName, string email, string password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
        }

        /// <summary>Updates the user pantry.</summary>
        /// <param name="pantry">The new pantry.</param>
        public void UpdateUserPantry(ArrayList pantry)
        {
            this.Pantry = pantry;

        }
    }
}
