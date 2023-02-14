namespace Team3DesktopApp.Model;

/// <summary>
///     The user class
/// </summary>
public class User
{
    #region Properties

    /// <summary>
    ///     Gets the username.
    /// </summary>
    /// <value>
    ///     The username.
    /// </value>
    public string Username { get; }

    /// <summary>
    ///     Gets the first name.
    /// </summary>
    /// <value>
    ///     The first name.
    /// </value>
    public string FirstName { get; private set; }

    /// <summary>
    ///     Gets the last name.
    /// </summary>
    /// <value>
    ///     The last name.
    /// </value>
    public string LastName { get; private set; }

    /// <summary>
    ///     Gets the email.
    /// </summary>
    /// <value>
    ///     The email.
    /// </value>
    public string Email { get; private set; }

    /// <summary>
    ///     Gets the password.
    /// </summary>
    /// <value>
    ///     The password.
    /// </value>
    public string Password { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="User" /> class.
    /// </summary>
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
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Updates the user information.
    /// </summary>
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

    #endregion
}