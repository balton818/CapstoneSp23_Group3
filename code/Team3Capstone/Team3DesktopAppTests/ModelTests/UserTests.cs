﻿using System.Collections;
using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser()
        {
            User testUser = new User("Testing", "Test", "tester", "test@ok.com", "1234");
            Assert.IsNotNull(testUser);
        }

        [TestMethod]
        public void UpdatePantry()
        {
            User testUser = new User("Testing", "Test", "tester", "test@ok.com", "1234");
            ArrayList testPantry = new ArrayList();
            testPantry.Add("test");
            testUser.UpdateUserPantry(testPantry);
            Assert.AreEqual(testUser.Pantry.Count, testPantry.Count);
        }
        [TestMethod]
        public void UpdateUserInfo()
        {
            User testUser = new User("Testing", "Test", "tester", "test@ok.com", "1234");
            testUser.UpdateUserInfo("updated", "last", "email", "pass");
            Assert.AreEqual("updated", testUser.FirstName);
            Assert.AreEqual("last", testUser.LastName);
            Assert.AreEqual("email", testUser.Email);
            Assert.AreEqual("pass", testUser.Password);


        }

    }
}