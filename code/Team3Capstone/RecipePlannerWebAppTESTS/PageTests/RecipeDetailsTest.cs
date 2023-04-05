using Bunit;
using RecipePlannerWebApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using RecipePlannerApi.Model;
using RecipePlannerWebApp.LocalServices;

namespace RecipePlannerWebAppTESTS.PageTests
{
    public class RecipeDetailsTest
    {
        [Fact]
        public void TestPageInitializesProperly()
        {
            using var ctx = new TestContext();
            User currUser = new User() { Id = 3 };
            ctx.Services.AddSingleton(new UserSessionData() { CurrentUser = currUser });
            var detailsComp = ctx.RenderComponent<RecipeDetails>(p => p.Add(p => p.RecipeId, "5").Add(p => p.RecipeTitle, "testAdd"));


            var header = "recipes";
            Assert.Equal(header, detailsComp.Find($"#header").TextContent);
        }
    }

}
