using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

public class RecipeDetailViewModel
{
    #region Properties

    /// <summary>Gets or sets the recipe information.</summary>
    /// <value>The recipe information.</value>
    public RecipeInformation RecipeInfo { get; set; }

    #endregion

    #region Methods

    /// <summary>Recipes the detail nav.</summary>
    /// <param name="recipeId">The recipe identifier.</param>
    public async void RecipeDetailNav(int recipeId)
    {
        var connection = new HttpClientConnection();
        this.RecipeInfo = await connection.GetRecipeDetail(recipeId);
    }

    #endregion
}