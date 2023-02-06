using System.Collections;
using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser()
        {
            User testUser = new User("Testing", "Test", "tester", "test@ok.com", "1234", "123 ok st", "apt 1", "oklahoma city", "ok", "73120");
            Assert.IsNotNull(testUser);
        }

        [TestMethod]
        public void UpdatePantry()
        {
            User testUser = new User("Testing", "Test", "tester", "test@ok.com", "1234", "123 ok st", "apt 1", "oklahoma city", "ok", "73120");
            ArrayList testPantry = new ArrayList();
            testPantry.Add("test");
            testUser.UpdateUserPantry(testPantry);
            Assert.AreEqual(testUser.Pantry.Count, testPantry.Count);
        }
        [TestMethod]
        public void UpdateUserInfo()
        {
            User testUser = new User("Testing", "Test", "tester", "test@ok.com", "1234", "123 ok st", "apt 1", "oklahoma city", "ok", "73120");
            testUser.UpdateUserInfo("updated", "last", "email", "pass", "234 ok st", "apt 2", "Atlanta", "GA", "55555");
            Assert.AreEqual("updated", testUser.FirstName);
            Assert.AreEqual("last", testUser.LastName);
            Assert.AreEqual("email", testUser.Email);
            Assert.AreEqual("pass", testUser.Password);
            Assert.AreEqual("234 ok st", testUser.Address);
            Assert.AreEqual("apt 2", testUser.AddressTwo);
            Assert.AreEqual("Atlanta", testUser.City);
            Assert.AreEqual("GA", testUser.State);
            Assert.AreEqual("55555", testUser.Zipcode);

        }

    }
}