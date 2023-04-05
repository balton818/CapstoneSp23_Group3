using Bunit;
using RecipePlannerWebApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using RecipePlannerApi.Model;
using RecipePlannerWebApp.LocalServices;

namespace RecipePlannerWebAppTESTS
{
    public class RegisterTest
    {
        [Fact]
        public void TestPageInitializesProperly()
        {
            using var ctx = new TestContext();
            var registerComp = ctx.RenderComponent<Register>();

            var usernameFieldText = "";
            Assert.Equal(usernameFieldText, registerComp.Find($"#usernameField").TextContent);
        }

        [Fact]
        public void TestShouldNotAllowRegisterWithNonEqualPasswords()
        {
            using var ctx = new TestContext();
            User testUser = new User { Username = "blah1", Password = "blah1" };
            var registerComp = ctx.RenderComponent<Register>(parameters => parameters.Add(p => p.user, testUser));
            registerComp.Find($"#passwordReentryField").TextContent = "halb";
            var registerButton = registerComp.Find($"#submitButton");
            registerButton.Click();

            Assert.False(registerComp.Instance.ValidFields);
        }

        [Fact]
        public void TestShouldRegisterWhenPasswordsMatch()
        {
            using var ctx = new TestContext();
            User testUser = new User { Username = "blah1", Password = "blah1" };
            var registerComp = ctx.RenderComponent<Register>(parameters => parameters.Add(p => p.user, testUser));
            registerComp.Find($"#passwordReentryField").TextContent = "blah1";
            var registerButton = registerComp.Find($"#submitButton");
            registerButton.Click();

            Assert.True(registerComp.Instance.ValidFields);
        }

        [Fact]
        public void TestShouldNotAllowRegisterWithExistingUser()
        {
            using var ctx = new TestContext();
            User testUser = new User { Username = "ba", Password = "ab" };
            var registerComp = ctx.RenderComponent<Register>(parameters => parameters.Add(p => p.user, testUser));
            registerComp.Find($"#passwordReentryField").TextContent = "ab";
            var registerButton = registerComp.Find($"#submitButton");
            registerButton.Click();

            Assert.False(registerComp.Instance.ValidFields);
        }
    }
}
