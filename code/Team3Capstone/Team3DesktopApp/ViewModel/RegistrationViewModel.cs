using System;
using System.Net.Http;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

public class RegistrationViewModel
{
    #region Methods

    /// <summary>Registers the asynchronous.</summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <param name="client"></param>
    /// <returns>
    ///     <br />
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