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

    

    public HttpClientConnection()
    {
    }


    public async Task<bool> ValidateUser(string username, string password)
    {
        using (var client = new HttpClient())

            {
                client.BaseAddress = new Uri("https://localhost:7278/api/");
                Uri query = new Uri(string.Format("User/{0}, {1}", username, password), UriKind.Relative);
                Console.WriteLine(query);
                HttpResponseMessage response = await client.GetAsync(query);
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                var readTask = response.Content.ReadAsAsync<bool>();
                    readTask.Wait();

                    var result = readTask.Result;
                return result;
                }
            }
    
        return false;
    }


}