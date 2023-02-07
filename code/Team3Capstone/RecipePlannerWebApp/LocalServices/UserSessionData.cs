using RecipePlannerApi.Model;

namespace RecipePlannerWebApp.LocalServices
{
    public class UserSessionData
    {
        public User CurrentUser { get; set; } = new User();
    }
}
