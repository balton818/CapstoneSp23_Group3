using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests;

[TestClass]
public class UserTests
{
    #region Methods

    [TestMethod]
    public void CreateUser()
    {
        var testUser = new User("Testing", "Test", "tester", "test@ok.com", "1234");
        Assert.IsNotNull(testUser);
    }

    [TestMethod]
    public void UpdateUserInfo()
    {
        var testUser = new User("Testing", "Test", "tester", "test@ok.com", "1234");
        testUser.UpdateUserInfo("updated", "last", "email", "pass");
        Assert.AreEqual("updated", testUser.FirstName);
        Assert.AreEqual("last", testUser.LastName);
        Assert.AreEqual("email", testUser.Email);
        Assert.AreEqual("pass", testUser.Password);
        Assert.AreEqual("Testing", testUser.Username);
    }

    #endregion
}