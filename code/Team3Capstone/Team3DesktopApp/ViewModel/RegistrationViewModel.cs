using System.Threading.Tasks;
using System;
using Team3DesktopApp.Dal;

namespace Team3DesktopApp.ViewModel;

public class RegistrationViewModel
{
    public async Task<bool> RegisterAsync(string userName, string password, string email, string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return false;
        }

        HttpClientConnection connection = new HttpClientConnection();
        bool result = await connection.ValidateUser(userName, password);
        Console.WriteLine(result);
        return result;

    }
}