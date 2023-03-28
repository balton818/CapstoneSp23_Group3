using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests;

[TestClass]
public class MealTests
{
    [TestMethod]
    public void CreateMeal()
    {
        var testMeal = new Meal
        {
            MealId = 1,
            Date = DateTime.Now.Date,
            DayOfWeek = DayOfWeek.Friday,
            MealType = MealType.Breakfast,
            Recipe = new Recipe()
        };
        Assert.IsNotNull(testMeal);
        Assert.AreEqual(1, testMeal.MealId);
        Assert.AreEqual(DateTime.Now.Date, testMeal.Date);
        Assert.AreEqual(DayOfWeek.Friday, testMeal.DayOfWeek);
        Assert.AreEqual(MealType.Breakfast, testMeal.MealType);
        Assert.IsNotNull(testMeal.Recipe);
    }
}