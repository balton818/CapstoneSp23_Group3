using System.Net;
using Moq.Protected;
using Moq;
using Team3DesktopApp.Model;
using Team3DesktopApp.ViewModel;

namespace Team3DesktopAppTests.ViewModelTests;
[TestClass]
public class TestGroceryListViewModel
{
    [TestMethod]
    public void TestGetGroceryList()
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
                               "[{'ShoppingListId':1, 'UserId':1, 'ingredientName': 'string', 'quantity': 1 }]")
                       })
                   .Verifiable();
        foodieViewModel.ClientToSet = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com/")
        };
        foodieViewModel.Userid = 1;
        Task<List<GroceryListItem>> task = foodieViewModel.GetGroceryList();
        Assert.IsNotNull(task.Result);
        Assert.AreEqual(1, task.Result.Count);
    }

}