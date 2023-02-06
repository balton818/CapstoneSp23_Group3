using System;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;

namespace Team3DesktopApp.ViewModel;

public class LoginViewModel
{

    public async Task<bool> LoginAsync(string userName, string password)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            return false;
        }

        HttpClientConnection connection = new HttpClientConnection();
        bool result = await connection.ValidateUser(userName, password);
        Console.WriteLine(result);
        return result;

    }

}