using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests
{
    [TestClass]
    public class RecipeStepsTests
    {
        [TestMethod]
        public void CreateRecipeSteps()
        {
            RecipeStep testRecipeSteps = new RecipeStep();
            testRecipeSteps.stepNumber = 1;
            testRecipeSteps.instructions = "Test";
            Assert.IsNotNull(testRecipeSteps);
            Assert.AreEqual(1, testRecipeSteps.stepNumber);
            Assert.AreEqual("Test", testRecipeSteps.instructions);

        }
    }
}
