using System.Net;
using Moq;
using Moq.Protected;
using NuGet.Frameworks;
using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopAppTests.ViewModelTests;

[TestClass]
public class FoodieViewModelTests
{
    #region Methods

    [TestMethod]
    public void TestCreation()
    {
        var foodieViewModel = new FoodieViewModel();
        Assert.IsNotNull(foodieViewModel);
    }

    [TestMethod]
    public void TestLogin()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent("1")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        var result = foodieViewModel.Login("test", "test");
        Assert.AreEqual(1, result.Result);
    }

    [TestMethod]
    public void TestGetPantry()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "[{'pantryId':1, 'UserId':1, 'ingredientName': 'string', 'quantity': 1 }]")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        var result = foodieViewModel.GetPantry();

        Assert.AreEqual(1, result.Result.Count);
    }

    [TestMethod]
    public void TestGetRecipes()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent("[{'id':1, 'title':'Pasta!', 'image': 1, 'imageType': 1 }]")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        var result = foodieViewModel.GetRecipes();

        Assert.AreEqual("Pasta!", result[0]);
    }

    [TestMethod]
    public void TestRegisterUser()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent("5")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        ;
        var result = foodieViewModel.RegisterUser("Pasta", "password", "2@2.com", "first", "last");

        Assert.AreEqual(5, result.Result);
    }

    [TestMethod]
    public void TestAddIngredient()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'pantryId':5,  'userId': 5,'ingredientName': 'Pasta','quantity': 2, 'unitId':3}")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        var result = foodieViewModel.AddPantryIngredient("Pasta", 2, "g");
        result = foodieViewModel.AddPantryIngredient("Pasta", 2, "Ml");
        result = foodieViewModel.AddPantryIngredient("Pasta", 2, "Fluid_Ounces");
        result = foodieViewModel.AddPantryIngredient("Pasta", 2, "None");
        result = foodieViewModel.AddPantryIngredient("Pasta", 2, "Oz");
        Assert.AreEqual(UnitEnum.Ounces, result.Result.UnitId);

        Assert.AreEqual("Pasta", result.Result.IngredientName);
    }

    [TestMethod]
    public void TestAddGroceryItem()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'ShoppingListId':5,  'userId': 5,'ingredientName': 'Pasta','quantity': 2, 'unitId':3}")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        var result = foodieViewModel.AddGroceryIngredient("Pasta", 2, "g");
        result = foodieViewModel.AddGroceryIngredient("Pasta", 2, "Ml");
        result = foodieViewModel.AddGroceryIngredient("Pasta", 2, "Fluid_Ounces");
        result = foodieViewModel.AddGroceryIngredient("Pasta", 2, "None");
        result = foodieViewModel.AddGroceryIngredient("Pasta", 2, "Oz");
        Assert.AreEqual(UnitEnum.Ounces, result.Result.UnitId);

        Assert.AreEqual("Pasta", result.Result.IngredientName);
    }

    [TestMethod]
    public void TestRecipeDetailNav()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        this.addRecipesForDetailNavTest(foodieViewModel);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'summary':'test', 'ingredients':[ {'name': 'oranges', 'quantity': 5},], 'steps':[{'stepNumber':1, 'instructions': 'Peel Orange'}] }")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        var result = foodieViewModel.RecipeDetailNavFound("oranges");

        Assert.AreEqual(1, result.Result.Ingredients.Count);
    }

    [TestMethod]
    public void TestRecipeDetailNavBrowse()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        this.addRecipesForBrowseNavTest(foodieViewModel);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'summary':'test', 'ingredients':[ {'name': 'oranges', 'quantity': 5},], 'steps':[{'stepNumber':1, 'instructions': 'Peel Orange'}] }")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        var result = foodieViewModel.RecipeDetailNavBrowse("oranges");

        Assert.AreEqual(1, result.Result.Ingredients.Count);
    }

    private void addRecipesForBrowseNavTest(FoodieViewModel foodieViewModel)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'recipes':[{ 'apiid': 634141, 'title': 'oranges', 'image': 'https://spoonacular.com/recipeImages/634141-312x231.jpg', 'imageType': 'jpg'}], 'page':0, 'totalNumberOfRecipes': 1 }")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;

        var result = foodieViewModel.BrowseRecipes();
    }

    private void addRecipesForDetailNavTest(FoodieViewModel foodieViewModel)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent("[{'apiid':1, 'title':'oranges', 'image': 1, 'imageType': 1 }]")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        var result = foodieViewModel.GetRecipes();
    }

    [TestMethod]
    public void TestEditItem()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        this.getPantryForTest(foodieViewModel);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'pantryId':5,  'userId': 5,'ingredientName': 'Pasta','quantity': 5, 'Unit': 'Fluid_Ounces'}")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        var result = foodieViewModel.EditPantryIngredient("Pasta", 5);

        Assert.AreEqual("Pasta", result.Result.IngredientName);
        Assert.AreEqual(5, result.Result.Quantity);
    }


    [TestMethod]
    public void TestEditGroceryItem()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        this.getGroceryForTest(foodieViewModel);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'ShoppingListId':5,  'userId': 5,'ingredientName': 'Pasta','quantity': 5, 'Unit': 'Fluid_Ounces'}")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        var result = foodieViewModel.EditGroceryIngredient("Pasta", 5);

        Assert.AreEqual("Pasta", result.Result.IngredientName);
        Assert.AreEqual(5, result.Result.Quantity);
    }

    private void getPantryForTest(FoodieViewModel foodieViewModel)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "[{'pantryId':5, 'UserId':5, 'ingredientName': 'Pasta', 'quantity': 2, 'unit':'g'}]")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        var result = foodieViewModel.GetPantry();

        Assert.AreEqual(1, result.Result.Count);
    }

    private void getGroceryForTest(FoodieViewModel foodieViewModel)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "[{'ShoppingListId':5, 'UserId':5, 'ingredientName': 'Pasta', 'quantity': 4, 'unit':'g'}]")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        var result = foodieViewModel.GetGroceryList();

        Assert.AreEqual(1, result.Result.Count);
    }

    [TestMethod]
    public void TestGetRecipeIngredients()
    {
        var ingredients = new List<string>();
        var foodieViewModel = new FoodieViewModel();
        this.simulateNavForTest(foodieViewModel);
        ingredients.AddRange(foodieViewModel.GetRecipeIngredients());
        Assert.AreEqual(1, ingredients.Count);
    }

    [TestMethod]
    public void TestGetRecipeSteps()
    {
        var ingredients = new List<string>();
        var foodieViewModel = new FoodieViewModel();
        this.simulateNavForTest(foodieViewModel);
        ingredients.AddRange(foodieViewModel.GetRecipeSteps());
        Assert.AreEqual(1, ingredients.Count);
        Assert.AreEqual("oranges", foodieViewModel.GetRecipeTitle());
    }

    [TestMethod]
    public void TestGetRecipeStepsMultiStep()
    {
        var ingredients = new List<string>();
        var foodieViewModel = new FoodieViewModel();
        this.simulateNavForTestMultiStep(foodieViewModel);
        ingredients.AddRange(foodieViewModel.GetRecipeSteps());
        Assert.AreEqual(2, ingredients.Count);
        Assert.AreEqual("oranges", foodieViewModel.GetRecipeTitle());
    }

    private void simulateNavForTestMultiStep(FoodieViewModel foodieViewModel)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        this.addRecipesForDetailNavTest(foodieViewModel);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'summary':'test', 'ingredients':[ {'name': 'oranges', 'quantity': 5},], 'steps':[{'stepNumber':1, 'instructions': 'Peel Orange'}, {'stepNumber':2, 'instructions': 'Eat Orange'},] }")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        var result = foodieViewModel.RecipeDetailNavFound("oranges");
    }

    private void simulateNavForTest(FoodieViewModel foodieViewModel)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        this.addRecipesForDetailNavTest(foodieViewModel);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'summary':'test', 'ingredients':[ {'name': 'oranges', 'quantity': 5},], 'steps':[{'stepNumber':1, 'instructions': 'Peel Orange'}] }")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        var result = foodieViewModel.RecipeDetailNavFound("oranges");
    }

    [TestMethod]
    public void TestRemoveIngredient()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        this.getPantryForTest(foodieViewModel);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent("true")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        var result = foodieViewModel.RemoveIngredient("Pasta", 5, true);

        Assert.AreEqual(true, result.Result);
    }
    [TestMethod]
    public void TestRemoveGroceryIngredient()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        this.getGroceryForTest(foodieViewModel);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent("true")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };

        var result = foodieViewModel.RemoveIngredient("Pasta", 5, false);

        Assert.AreEqual(true, result.Result);
    }

    [TestMethod]
    public void TestBrowseRecipes()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'recipes':[{ 'id': 634141, 'title': 'Pasta!', 'image': 'https://spoonacular.com/recipeImages/634141-312x231.jpg', 'imageType': 'jpg'}], 'page':0, 'totalNumberOfRecipes': 1 }")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;

        var result = foodieViewModel.BrowseRecipes();

        Assert.AreEqual("Pasta!", result[0]);
        foodieViewModel.IncrementPage();
        Assert.AreEqual("1 of 1", foodieViewModel.GetPageInfo());
        foodieViewModel.DecrementPage();
        Assert.AreEqual("1 of 1", foodieViewModel.GetPageInfo());
    }

    [TestMethod]
    public void TestBrowseRecipesDifferentNumberOfPages()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'recipes':[{ 'id': 634141, 'title': 'Pasta!', 'image': 'https://spoonacular.com/recipeImages/634141-312x231.jpg', 'imageType': 'jpg'}], 'page':0, 'totalNumberOfRecipes': 5000 }")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;

        var result = foodieViewModel.BrowseRecipes();

        Assert.AreEqual("Pasta!", result[0]);
        foodieViewModel.IncrementPage();
        Assert.AreEqual("2 of 46", foodieViewModel.GetPageInfo());
        foodieViewModel.DecrementPage();
        Assert.AreEqual("1 of 46", foodieViewModel.GetPageInfo());
        foodieViewModel.ResetBrowse();
        Assert.AreEqual("1 of 1", foodieViewModel.GetPageInfo());
    }

    [TestMethod]
    public void TestBrowseRecipesNoPages()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'recipes':[{ 'id': 634141, 'title': 'Pasta!', 'image': 'https://spoonacular.com/recipeImages/634141-312x231.jpg', 'imageType': 'jpg'}], 'page':0, 'totalNumberOfRecipes': 0 }")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;

        var result = foodieViewModel.BrowseRecipes();

        Assert.AreEqual("Pasta!", result[0]);
        foodieViewModel.IncrementPage();
        Assert.AreEqual("1 of 1", foodieViewModel.GetPageInfo());
        foodieViewModel.DecrementPage();
        Assert.AreEqual("1 of 1", foodieViewModel.GetPageInfo());
    }

    [TestMethod]
    public void TestGetRecipeTypes()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "['breakfast']")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        var result = foodieViewModel.GetRecipeTypes();

        Assert.AreEqual(2, result.Count);
        foodieViewModel.ResetBrowse();
    }

    [TestMethod]
    public void TestGetDietTypes()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "['vegan']")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        var result = foodieViewModel.GetDietTypes();

        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public void TestSetFilters()
    {
        var foodieViewModel = new FoodieViewModel();
        foodieViewModel.SetFilters("breakfast", "vegan", "");

        Assert.AreEqual("breakfast", foodieViewModel.GetFilters().Item1);
        Assert.AreEqual("vegan", foodieViewModel.GetFilters().Item2);
    }

    [TestMethod]
    public void TestSetSearchName()
    {
        var foodieViewModel = new FoodieViewModel();
        foodieViewModel.SetSearchName("Pasta");
        Assert.AreEqual("Pasta", foodieViewModel.GetSearchName());
    }

    [TestMethod]
    public void TestGetCuisines()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "['English']")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        var result = foodieViewModel.GetCuisineTypes();

        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public void TestGetPlans()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'mealPlanId':9, 'page':0, 'mealPlanDate': '2023-03-12T00:00:00', 'userId':1, 'meals':{'Sunday':[{}]} }")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        var result = foodieViewModel.GetPlans();

        var test = foodieViewModel.GetMealPlan(true, DayOfWeek.Sunday);
        var test2 = foodieViewModel.GetMealPlan(false, DayOfWeek.Sunday);
        Assert.IsNotNull(test);
        Assert.IsFalse(foodieViewModel.GetCurrentWeek());

    }

    private void getPlansForTesting(FoodieViewModel foodieViewModel)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'recipes': [2], 'mealPlanId':9, 'page':0, 'mealPlanDate': '2023-03-12T00:00:00', 'userId':1, 'meals':{'Sunday':[{'mealId':9, 'date': '2023-03-12T00:00:00', 'dayOfWeek':0, 'mealType':0,'recipe':{'apiid':1, 'title':'Pasta!', 'image': 1, 'imageType': 1 }  }]} }")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        var result = foodieViewModel.GetPlans();

        var test = foodieViewModel.GetMealPlan(true, DayOfWeek.Sunday);
        Tuple<DayOfWeek?, MealType?> planTypeAndDateToAdd = new Tuple<DayOfWeek?, MealType?>(DayOfWeek.Sunday, MealType.Breakfast);
        foodieViewModel.PlanTypeAndDateToAdd = planTypeAndDateToAdd;
        foodieViewModel.Userid = 1;
    }

    [TestMethod]
    public void TestAddToPlan()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        foodieViewModel.GetPlanDate(true);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "{'mealId':9, 'mealPlanId':0, 'date': '2023-03-12T00:00:00', 'dayOfWeek':0, 'mealType':0, 'recipe':{}")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        this.simulateNavForTest(foodieViewModel);
        this.getPlansForTesting(foodieViewModel);
        Tuple<DayOfWeek?, MealType?> planTypeAndDateToAdd = new Tuple<DayOfWeek?, MealType?>(DayOfWeek.Sunday, MealType.Breakfast);
        foodieViewModel.PlanTypeAndDateToAdd = planTypeAndDateToAdd;
        foodieViewModel.Userid = 1;
        foodieViewModel.AddToMealPlan(true);
        foodieViewModel.AddToMealPlan(false);
        Assert.IsTrue(foodieViewModel.GetMealPlan(true, DayOfWeek.Sunday).Count == 3);

    }

    [TestMethod]
    public void TestRemoveFromPlan()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        this.simulateNavForTest(foodieViewModel);
        this.getPlansForTesting(foodieViewModel);
        foodieViewModel.AddToMealPlan(false);
        foodieViewModel.RemoveMealFromPlan("oranges", DayOfWeek.Sunday, MealType.Breakfast);
        foodieViewModel.RemoveMealFromPlan("oranges", DayOfWeek.Sunday, MealType.Breakfast);
        Assert.IsTrue(foodieViewModel.GetMealPlan(true, DayOfWeek.Sunday).Count == 3);

        Assert.IsNotNull(foodieViewModel.GetPlanDate(true));
        Assert.IsNotNull(foodieViewModel.GetPlanDate(false));
    }

    [TestMethod]
    public void TestMealPlanContainsRecipe()
    {
        var foodieViewModel = new FoodieViewModel();
        this.getPlansForTesting(foodieViewModel);
        Assert.IsTrue(foodieViewModel.MealPlanContainsRecipe(MealType.Breakfast, DayOfWeek.Sunday, true));
        Assert.IsTrue(foodieViewModel.MealPlanContainsRecipe(MealType.Breakfast, DayOfWeek.Sunday, false));
    }

    [TestMethod]
    public void TestRecipeDetailNavPlan()
    {
        var foodieViewModel = new FoodieViewModel();
        this.simulateNavForTest(foodieViewModel);
        this.getPlansForTesting(foodieViewModel);
        Assert.IsNotNull(foodieViewModel.RecipeDetailNavPlan(DayOfWeek.Sunday, MealType.Breakfast));
        foodieViewModel.AddToMealPlan(false);
        Assert.IsNotNull(foodieViewModel.RecipeDetailNavPlan(DayOfWeek.Sunday, MealType.Breakfast));
    }
    [TestMethod]
    public void TestUpdatePlan()
    {
        var foodieViewModel = new FoodieViewModel();
        this.simulateNavForTest(foodieViewModel);
        this.getPlansForTesting(foodieViewModel);

        foodieViewModel.UpdatePlan(true);
        foodieViewModel.UpdatePlan(false);
        Assert.IsTrue(foodieViewModel.GetMealPlan(true, DayOfWeek.Sunday).Count == 3);

    }

    [TestMethod]
    public void TestAddNeededGroceries()
    {
        var foodieViewModel = new FoodieViewModel();
        this.getPlansForTesting(foodieViewModel);
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "[{'ShoppingListId':1, 'UserId':1, 'ingredientName': 'string', 'quantity': 1 }]")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        Task task = foodieViewModel.AddedNeededGroceries();
        Assert.AreEqual(true, task.IsCompletedSuccessfully);
    }

    [TestMethod]
    public void TestPurchaseIngredients()
    {

        var foodieViewModel = new FoodieViewModel();
        var GroceryViewModel = new GroceryListViewModel();

        Dictionary<string, int> testing = new Dictionary<string, int>();
        this.getGroceryForTest(foodieViewModel);
        testing.Add("Pasta", 2);
        GroceryViewModel.GroceryList = new List<GroceryListItem>() { new GroceryListItem() { IngredientName = "Pasta", Quantity = 2, UnitId = 0 } };
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "[{'pantryId':5, 'UserId':5, 'ingredientName': 'Pasta', 'quantity': 2, 'unit':'g'}]")
                       })
                   .Verifiable();
        HttpClient Client = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        GroceryViewModel.BuyGroceryItems(testing, 5, Client);

    }

    [TestMethod]
    public void TestClearGroceryList()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        this.getGroceryForTest(foodieViewModel);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent("true")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        var task = foodieViewModel.ClearGroceryList();
        Assert.AreEqual(true, task.IsCompletedSuccessfully);

    }

    [TestMethod]
    public void TestGetIngredientsUsed()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var foodieViewModel = new FoodieViewModel();
        this.getPantryForTest(foodieViewModel);
        this.addRecipesForDetailNavTest(foodieViewModel);
        this.simulateNavForTestMultiStep(foodieViewModel);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                       new HttpResponseMessage
                       {
                           StatusCode = HttpStatusCode.OK,
                           Content = new StringContent(
                               "[{'pantryId':1, 'UserId':1, 'ingredientName': 'orange', 'quantity': 1 }]")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.PrepareMeal();


    }
    #endregion
}