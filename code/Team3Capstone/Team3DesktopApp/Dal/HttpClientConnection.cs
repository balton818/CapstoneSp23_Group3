using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using Team3DesktopApp.Model;
using static System.Net.WebRequestMethods;

namespace Team3DesktopApp.Dal;

public class HttpClientConnection
{
    public static HttpClient client = new HttpClient();

    public HttpClientConnection()
    {
        RunAsync().GetAwaiter().GetResult();
    }

    static async Task RunAsync()
    {
        // Update port # in the following line.
        client.BaseAddress = new Uri("http://localhost:7278/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        User user = null;
        Uri query = new Uri(string.Format("api/User/{0}/{1}", username, password), UriKind.Relative);
        HttpResponseMessage response = await client.GetAsync(query);
        if (response.IsSuccessStatusCode)
        {
            user = await response.Content.ReadAsAsync<User>();
            Console.WriteLine(user.username);
        }

        Console.WriteLine(response.StatusCode);
        return user;
    }


}