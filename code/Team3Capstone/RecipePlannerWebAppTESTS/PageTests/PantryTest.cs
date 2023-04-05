using Bunit;
using RecipePlannerWebApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using RecipePlannerApi.Model;
using RecipePlannerWebApp.LocalServices;

namespace RecipePlannerWebAppTESTS.PageTests
{
    public class PantryTest
    {
        [Fact]
        public void TestPageInitializesProperly()
        {
            using var ctx = new TestContext();
            User currUser = new User() { Id = 3 };
            ctx.Services.AddSingleton(new UserSessionData() { CurrentUser = currUser });
            var pantryComp = ctx.RenderComponent<Pantry>();

            var header = "pantry";
            Assert.Equal(header, pantryComp.Find($"#header").TextContent);
        }

    }
}
