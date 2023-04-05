using Bunit;
using RecipePlannerWebApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using RecipePlannerApi.Model;
using RecipePlannerWebApp.LocalServices;

namespace RecipePlannerWebAppTESTS.PageTests
{
    public class PlannerTest
    {
        [Fact]
        public void TestPlannerInitializesProperly()
        {
            using var ctx = new TestContext();
            User currUser = new User() { Id = 3 };
            ctx.Services.AddSingleton(new UserSessionData() { CurrentUser = currUser });
            var plannerComp = ctx.RenderComponent<Planner>();

            var header = "planner";
            Assert.Equal(header, plannerComp.Find($"#header").TextContent);
        }
    }
}
