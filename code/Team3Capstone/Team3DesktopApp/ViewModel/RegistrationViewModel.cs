using System.Threading.Tasks;
using System;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

public class RegistrationViewModel
{
    public async Task<int> RegisterAsync(string userName, string password, string email, string firstName, string lastName)
    {

        HttpClientConnection connection = new HttpClientConnection();
        User toCreate = new User(userName, firstName, lastName, email, password);
        int result = await connection.RegisterUser(toCreate);
        Console.WriteLine(result);
        return result;

    }
}