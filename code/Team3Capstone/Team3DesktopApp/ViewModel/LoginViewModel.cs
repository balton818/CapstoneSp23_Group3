using System;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;

namespace Team3DesktopApp.ViewModel;

public class LoginViewModel
{
    #region Methods

    /// <summary>Logins the asynchronous.</summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <returns>
    ///     <br />
    /// </returns>
    public async Task<int> LoginAsync(string userName, string password)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            return -1;
        }

        var connection = new HttpClientConnection();
        var result = await connection.ValidateUser(userName, password);
        Console.WriteLine(result);
        return result;
    }

    #endregion
}