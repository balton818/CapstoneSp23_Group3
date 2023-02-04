using Bunit;
using RecipePlannerWebApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using RecipePlannerApi.Model;

namespace RecipePlannerWebAppTESTS
{
	public class LoginTest
	{
		[Fact]
		public void TestLoginInitializesProperly()
		{
			using var ctx = new TestContext();
			var loginComp = ctx.RenderComponent<Login>();

			var loginMessage = "";
			Assert.Equal(loginMessage, loginComp.Find($"#loginResultMessage").TextContent);
		}

		[Fact]
		public void TestLoginValidationDoesNotAllowEmptyFields()
		{
			using var ctx = new TestContext();
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

			var loginComp = ctx.RenderComponent<Login>(parameters => parameters.Add(p => p.user, testUser));

			var loginButton = loginComp.Find($"#loginButton");
			loginButton.Click();

			Assert.Equal("Invalid Credentials", loginComp.Find($"#loginResultMessage").TextContent);
		}

		[Fact]
		public void TestLoginValidationSuccess()
		{
			using var ctx = new TestContext();
			User testUser = new User { Username = "test", Password = "test" };

			var loginComp = ctx.RenderComponent<Login>(parameters => parameters.Add(p => p.user, testUser));

			var loginButton = loginComp.Find($"#loginButton");
			loginButton.Click();

			Assert.True(loginComp.Instance.ValidFields);
		}

	}
}