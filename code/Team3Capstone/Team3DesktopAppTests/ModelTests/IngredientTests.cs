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
            Ingredient testIngredient = new Ingredient("Test", 1);
            testIngredient.name = "Test";
            testIngredient.quanitiy = 1;
            Assert.IsNotNull(testIngredient);
            Assert.AreEqual("Test", testIngredient.name);
            Assert.AreEqual(1, testIngredient.quanitiy);

            testIngredient.name = "Test2";
            testIngredient.quanitiy = 2;
            Assert.AreEqual("Test2", testIngredient.name);
            Assert.AreEqual(2, testIngredient.quanitiy);
        }
    }
}
