using Bunit;
using RecipePlannerWebApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using RecipePlannerApi.Model;
using RecipePlannerWebApp.LocalServices;

namespace RecipePlannerWebAppTESTS.PageTests
{
    public class LoginTest
    {
        [Fact]
        public void TestLoginInitializesProperly()
        {
            using var ctx = new TestContext();
            ctx.Services.AddSingleton(new UserSessionData());
            var loginComp = ctx.RenderComponent<Login>();

            var loginMessage = "";
            Assert.Equal(loginMessage, loginComp.Find($"#loginResultMessage").TextContent);
        }

        [Fact]
        public void TestLoginValidationDoesNotAllowEmptyFields()
        {
            using var ctx = new TestContext();
            ctx.Services.AddSingleton(new UserSessionData());
            var loginComp = ctx.RenderComponent<Login>();


            var loginButton = loginComp.Find($"#loginButton");
            loginButton.Click();

            Assert.Equal("Please enter a username and password", loginComp.Find($"#loginResultMessage").TextContent);
        }

        [Fact]
        public void TestLoginValidationFailsOnInvalidUserData()
        {
            using var ctx = new TestContext();
            User testUser = new User { Username = "blah", Password = "blah" };
            ctx.Services.AddSingleton(new UserSessionData() { CurrentUser = testUser });

            var loginComp = ctx.RenderComponent<Login>(parameters => parameters.Add(p => p.user, testUser));

            var loginButton = loginComp.Find($"#loginButton");
            loginButton.Click();

            Assert.Equal("Invalid Credentials", loginComp.Find($"#loginResultMessage").TextContent);
        }

        [Fact]
        public void TestLoginValidationSuccess()
        {
            using var ctx = new TestContext();
            User testUser = new User { Username = "ba", Password = "ab" };
            ctx.Services.AddSingleton(new UserSessionData() { CurrentUser = testUser });

            var loginComp = ctx.RenderComponent<Login>(builder => { builder.Add(c => c.user, testUser); });

            var loginButton = loginComp.Find($"#loginButton");
            loginButton.Click();

            Assert.True(loginComp.Instance.ValidFields);
        }

    }
}