using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopAppTests.ViewModelTests;
[TestClass]
public class TestMealPlanViewModel
{
    [TestMethod]
    public void TestGetRecipe()
    {
        MealPlanViewModel testMealPlanViewModel = new MealPlanViewModel();
        testMealPlanViewModel.NextWeekPlan = new MealPlan();
        testMealPlanViewModel.FirstWeekPlan = new MealPlan();
        List<Meal?> meals = new List<Meal>();
        Meal toAdd = new Meal();
        toAdd.MealId = 1;
        toAdd.Date = DateTime.Now.Date;
        toAdd.DayOfWeek = DayOfWeek.Friday;
        toAdd.MealType = MealType.Breakfast;
        toAdd.Recipe = new Recipe();
        toAdd.Recipe.ApiId = 1;
        toAdd.Recipe.Title = "Test";
        toAdd.Recipe.Image = "test";
        toAdd.Recipe.ImageType = "test";
        meals.Add(toAdd);
        testMealPlanViewModel.FirstWeekPlan.Meals.Add(DayOfWeek.Friday, meals);
        testMealPlanViewModel.NextWeekPlan.Meals.Add(DayOfWeek.Friday, meals);
        Assert.IsNotNull(testMealPlanViewModel.GetRecipe(DayOfWeek.Friday, MealType.Breakfast));
        testMealPlanViewModel.CurrentWeek = false;
        Assert.IsNotNull(testMealPlanViewModel.GetRecipe(DayOfWeek.Friday, MealType.Breakfast));
        Assert.IsTrue(testMealPlanViewModel.CheckForRecipe(MealType.Breakfast, DayOfWeek.Friday));
    }

}