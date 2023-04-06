using Bunit;
using RecipePlannerWebApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using RecipePlannerApi.Model;
using RecipePlannerWebApp.LocalServices;

namespace RecipePlannerWebAppTESTS.PageTests
{
    public class RecipesTest
    {
        [Fact]
        public void TestPageInitializesProperly()
        {
            using var ctx = new TestContext();
            User currUser = new User() { Id = 3 };
            ctx.Services.AddSingleton(new UserSessionData() { CurrentUser = currUser });
            var recipeComp = ctx.RenderComponent<RecipeDetails>();

            var header = "recipes";
            Assert.Equal(header, recipeComp.Find($"#header").TextContent);
        }
    }
}
