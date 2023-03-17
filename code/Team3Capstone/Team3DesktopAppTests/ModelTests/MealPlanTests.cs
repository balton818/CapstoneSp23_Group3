using Team3DesktopApp.Model;

namespace Team3DesktopAppTests.ModelTests;

[TestClass]
public class MealPlanTests
{
    [TestMethod]
    public void CreateMealPlan()
    {
        var testPlan = new MealPlan();
        testPlan.MealPlanId = 1;
        testPlan.MealPlanDate = DateTime.Now.Date;
        Meal testMeal = new Meal();
        testPlan.Meals.Add(DayOfWeek.Friday, new List<Meal?> { testMeal });
        Assert.IsNotNull(testPlan);
        Assert.AreEqual(1, testPlan.MealPlanId);
        Assert.AreEqual(DateTime.Now.Date, testPlan.MealPlanDate);
        Assert.AreEqual(testMeal, testPlan.Meals[DayOfWeek.Friday][0]);
    }
}