using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

public class RecipeDetailViewModel
{
    public RecipeInformation RecipeInfo { get; set; }
    public async void RecipeDetailNav(int recipeId)
    {
        HttpClientConnection connection = new HttpClientConnection();
        this.RecipeInfo = await connection.GetRecipeDetail(recipeId);
    }

}