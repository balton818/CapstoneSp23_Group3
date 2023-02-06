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
        public string Address { get; private set; }
        /// <summary>Gets the address two.</summary>
        /// <value>The address two.</value>
        public string AddressTwo { get; private set; }
        /// <summary>Gets the city.</summary>
        /// <value>The city.</value>
        public string City { get; private set; }
        /// <summary>Gets the state.</summary>
        /// <value>The state.</value>
        public string State { get; private set; }
        /// <summary>Gets the zipcode.</summary>
        /// <value>The zipcode.</value>
        public string Zipcode { get; private set; }
        public ArrayList Pantry { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="User" /> class.</summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="address">The address.</param>
        /// <param name="addressTwo">The address two.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zipcode">The zipcode.</param>
        public User(string username, string firstName, string lastName, string email, string password, string address, string addressTwo,
            string city, string state, string zipcode)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.Address = address;
            this.AddressTwo = addressTwo;
            this.City = city;
            this.State = state;
            this.Zipcode = zipcode;
            this.Pantry = new ArrayList();
        }

        /// <summary>Updates the user information.</summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="address">The address.</param>
        /// <param name="addressTwo">The address two.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zipcode">The zipcode.</param>
        public void UpdateUserInfo(string firstName, string lastName, string email, string password, string address,
            string addressTwo, string city, string state, string zipcode)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.Address = address;
            this.AddressTwo = addressTwo;
            this.City = city;
            this.State = state;
            this.Zipcode = zipcode;
        }

        /// <summary>Updates the user pantry.</summary>
        /// <param name="pantry">The new pantry.</param>
        public void UpdateUserPantry(ArrayList pantry)
        {
            this.Pantry = pantry;

        }
    }
}
