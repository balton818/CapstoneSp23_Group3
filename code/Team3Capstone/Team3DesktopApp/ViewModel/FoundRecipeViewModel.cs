using System.Collections.Generic;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

/// <summary>
///     <br />
/// </summary>
public class FoundRecipeViewModel
{
    #region Properties

    /// <summary>Gets or sets the recipes.</summary>
    /// <value>The recipes.</value>
    public List<Recipe> Recipes { get; set; }

    /// <summary>Gets or sets the selected recipe title.</summary>
    /// <value>The selected recipe title.</value>
    public string SelectedRecipeTitle { get; set; }

    #endregion

    #region Methods

    /// <summary>Gets the recipes.</summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>
    ///     <br />
    /// </returns>
    public List<Recipe> GetRecipes(int userId)
    {
        this.Recipes = new List<Recipe>();
        var connection = new HttpClientConnection();
        var retrieved = connection.GetRecipes(userId);
        if (retrieved.Result != null)
        {
            this.Recipes.AddRange(retrieved.Result);
        }

        return this.Recipes;
    }

    #endregion
}