using System;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;

namespace Team3DesktopApp.ViewModel;

public class LoginViewModel
{

    public async Task<int> LoginAsync(string userName, string password)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            //for testing
            return -1;
        }

        HttpClientConnection connection = new HttpClientConnection();
        int result = await connection.ValidateUser(userName, password);
        Console.WriteLine(result);
        return result;

    }

}