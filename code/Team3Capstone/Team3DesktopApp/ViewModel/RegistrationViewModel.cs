using System;
using System.Net.Http;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

/// <summary>
///     The registration page view model
/// </summary>
public class RegistrationViewModel
{
    #region Methods

    /// <summary>Registers a new user.</summary>
    /// <param name="userName">Desired username .</param>
    /// <param name="password">entered password.</param>
    /// <param name="email">The users email.</param>
    /// <param name="firstName">The users first name.</param>
    /// <param name="lastName">The users last name.</param>
    /// <param name="client">the client used to connect to the backend</param>
    /// <returns>
    ///     the id of the user that was created or -1 if the user was not created
    /// </returns>
    public async Task<int> RegisterAsync(string userName, string password, string email, string firstName,
        string lastName, HttpClient client)

    {
        var connection = new HttpClientConnection();
        var toCreate = new User(userName, firstName, lastName, email, password);
        var result = await connection.RegisterUser(toCreate, client);
        Console.WriteLine(result);
        return result;
    }

    #endregion
}