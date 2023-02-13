using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests
{
    [TestClass]
    public class RecipeInformationTests
    {
        [TestMethod]
        public void CreateRecipeInformation()
        {
            RecipeInformation testRecipeInformation = new RecipeInformation();
            testRecipeInformation.Ingredients = new List<Ingredient>();
            testRecipeInformation.Steps = new List<RecipeStep>();
            testRecipeInformation.Summary = "Test";
            Assert.IsNotNull(testRecipeInformation);
            Assert.AreEqual("Test", testRecipeInformation.Summary);
            Assert.AreEqual(0, testRecipeInformation.Ingredients.Count);
            Assert.AreEqual(0, testRecipeInformation.Steps.Count);


        }
    }
}
