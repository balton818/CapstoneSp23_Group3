using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests;

[TestClass]
public class GroceryItemTests
{
    #region Methods

    [TestMethod]
    public void CreateGroceryItem()
    {
        var testItem = new GroceryListItem();
        testItem.IngredientName = "Test";
        testItem.Quantity = 1;
        testItem.UserId = 1;
        testItem.ShoppingListId = 1;
        Assert.IsNotNull(testItem);
        Assert.AreEqual("Test", testItem.IngredientName);
        Assert.AreEqual(1, testItem.Quantity);
        Assert.AreEqual(1, testItem.UserId);
        Assert.AreEqual(1, testItem.ShoppingListId);
    }

    #endregion
}