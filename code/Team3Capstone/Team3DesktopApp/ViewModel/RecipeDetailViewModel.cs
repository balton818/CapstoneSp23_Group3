using System.Net.Http;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

public class RecipeDetailViewModel
{
    #region Properties

    /// <summary>Gets or sets the recipe information.</summary>
    /// <value>The recipe information.</value>
    public RecipeInformation RecipeInfo { get; set; }
    public Recipe currentRecipe { get; set; }

    #endregion

    #region Methods

    /// <summary>gets the recipe details upon user navigation to the recipe detail page.</summary>
    /// <param name="recipeId">The recipe identifier.</param>
    public async Task RecipeDetailNav(int recipeId, HttpClient client)
    {
        var connection = new HttpClientConnection();
        this.RecipeInfo = await connection.GetRecipeDetail(recipeId, client);

    }

    #endregion
}