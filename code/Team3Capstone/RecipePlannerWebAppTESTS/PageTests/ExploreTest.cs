using Bunit;
using RecipePlannerWebApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using RecipePlannerApi.Model;
using RecipePlannerWebApp.LocalServices;

namespace RecipePlannerWebAppTESTS.PageTests
{
    public class ExploreTest
    {
        [Fact]
        public void TestPageInitializesProperly()
        {
            using var ctx = new TestContext();
            User currUser = new User() { Id = 3 };
            ctx.Services.AddSingleton(new UserSessionData() { CurrentUser = currUser });
            var exploreComp = ctx.RenderComponent<ExploreRecipes>();

            var header = "explore";
            Assert.Equal(header, exploreComp.Find($"#header").TextContent);
        }
    }
}
