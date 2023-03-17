using System;
using System.Net.Http;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;

namespace Team3DesktopApp.ViewModel;

/// <summary>
///     login view model class serves as the view model for the login page
/// </summary>
public class LoginViewModel
{
    #region Methods

    /// <summary>Verified the user login information and allows access to the app.</summary>
    /// <param name="userName">user name entered.</param>
    /// <param name="password">The password entered.</param>
    /// <param name="client">client to connect to backend</param>
    /// <returns>
    ///     the user id if successful, -1 if not
    /// </returns>
    public async Task<int> LoginAsync(string userName, string password, HttpClient client)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            return -1;
        }

        var connection = new HttpClientConnection();
        var result = await connection.ValidateUser(userName, password, client);
        Console.WriteLine(result);
        return result;
    }

    #endregion
}