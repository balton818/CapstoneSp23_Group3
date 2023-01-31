namespace RecipePlannerApi.Model
{
    public class User
    {
        /// <summary>Gets the id.</summary>
        /// <value>The id.</value>
        public int Id { get; set; }
        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        public string? Username { get; set; }
        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        public string? Password { get; set; }
        /// <summary>Gets the first name.</summary>
        /// <value>The first name.</value>
        public string? FirstName { get;  set; }
        /// <summary>Gets the last name.</summary>
        /// <value>The last name.</value>
        public string? LastName { get; set; }
        /// <summary>Gets the email.</summary>
        /// <value>The email.</value>
        public string? Email { get; set; }
        /// <summary>Gets the address.</summary>
        /// <value>The address.</value>
        public string? Address { get; set; }
        /// <summary>Gets the address two.</summary>
        /// <value>The address two.</value>
        public string? AddressTwo { get; set; }
        /// <summary>Gets the city.</summary>
        /// <value>The city.</value>
        public string? City { get; set; }
        /// <summary>Gets the state.</summary>
        /// <value>The state.</value>
        public string? State { get; set; }
        /// <summary>Gets the zipcode.</summary>
        /// <value>The zipcode.</value>
        public string? Zipcode { get; set; }
    }
}
