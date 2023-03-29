using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests;

[TestClass]
public class RecipeStepsTests
{
    #region Methods

    [TestMethod]
    public void CreateRecipeSteps()
    {
        var testRecipeSteps = new RecipeStep();
        testRecipeSteps.StepNumber = 1;
        testRecipeSteps.Instructions = "Test";
        Assert.IsNotNull(testRecipeSteps);
        Assert.AreEqual(1, testRecipeSteps.StepNumber);
        Assert.AreEqual("Test", testRecipeSteps.Instructions);
    }

    #endregion
}