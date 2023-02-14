﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests
{
    [TestClass]
    public class PantryItemTests
    {
        [TestMethod]
        public void CreatePantryItem()
        {
            PantryItem testItem = new PantryItem();
            testItem.IngredientName = "Test";
            testItem.Quantity = 1;
            testItem.UserId = 1;
            testItem.PantryId = 1;
            Assert.IsNotNull(testItem);
            Assert.AreEqual("Test", testItem.IngredientName);
            Assert.AreEqual(1, testItem.Quantity);
            Assert.AreEqual(1, testItem.UserId);
            Assert.AreEqual(1, testItem.PantryId);
        }
    }
}
