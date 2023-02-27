using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests
{
    [TestClass]
    public class IngredientTests
    {
        [TestMethod]
        public void CreateIngredient()
        {
            Ingredient testIngredient = new Ingredient("Test", 1, UnitEnum.None.ToString());
            testIngredient.IngredientName = "Test";
            testIngredient.Quantity = 1;
            Assert.IsNotNull(testIngredient);
            Assert.AreEqual("Test", testIngredient.IngredientName);
            Assert.AreEqual(1, testIngredient.Quantity);
            Assert.AreEqual(UnitEnum.None.ToString(), testIngredient.Unit);

            testIngredient.IngredientName = "Test2";
            testIngredient.Quantity = 2;
            Assert.AreEqual("Test2", testIngredient.IngredientName);
            Assert.AreEqual(2, testIngredient.Quantity);
        }
    }
}
