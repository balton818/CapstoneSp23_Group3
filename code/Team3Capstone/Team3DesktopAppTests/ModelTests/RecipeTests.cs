using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests
{
    [TestClass]
    public class RecipeTests
    {
        [TestMethod]
        public void CreateRecipe()
        {
            Recipe testRecipe = new Recipe();
            testRecipe.Id = 1;
            testRecipe.Title = "Test";
            Assert.IsNotNull(testRecipe);
            Assert.AreEqual(1, testRecipe.Id);
            Assert.AreEqual("Test", testRecipe.Title);
        }
    }
}
