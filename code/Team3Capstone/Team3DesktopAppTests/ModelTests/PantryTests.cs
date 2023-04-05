using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests;
[TestClass]
public class PantryTests
{
    [TestMethod]
    public void CreatePantry()
    {
        var testPantry = new Pantry();

        Assert.IsNotNull(testPantry);
        Assert.IsNotNull(testPantry.Ingredients);
    }
}