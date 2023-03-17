using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests;

[TestClass]
public class RecipeTests
{
    #region Methods

    [TestMethod]
    public void CreateRecipe()
    {
        var testRecipe = new Recipe();
        testRecipe.Id = 1;
        testRecipe.ApiId = 1;
        testRecipe.Title = "Test";
        testRecipe.Image = "test";
        testRecipe.ImageType = "test";
        Assert.IsNotNull(testRecipe);
        Assert.AreEqual(1, testRecipe.Id);
        Assert.AreEqual(1, testRecipe.ApiId);
        Assert.AreEqual("Test", testRecipe.Title);
        Assert.AreEqual("test", testRecipe.Image);
        Assert.AreEqual("test", testRecipe.ImageType);
    }

    #endregion
}