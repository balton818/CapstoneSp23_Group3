using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests;

[TestClass]
public class RecipeInformationTests
{
    #region Methods

    [TestMethod]
    public void CreateRecipeInformation()
    {
        var testRecipeInformation = new RecipeInformation();
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

    #endregion
}