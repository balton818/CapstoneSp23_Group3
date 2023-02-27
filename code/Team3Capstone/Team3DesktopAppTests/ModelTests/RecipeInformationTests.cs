using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
            testRecipeInformation.Image = null;

            Assert.IsNotNull(testRecipeInformation);
            Assert.AreEqual("Test", testRecipeInformation.Summary);
            Assert.AreEqual(0, testRecipeInformation.Ingredients.Count);
            Assert.AreEqual(0, testRecipeInformation.Steps.Count);
            Assert.AreEqual(null, testRecipeInformation.Image);


        }
    }
}
