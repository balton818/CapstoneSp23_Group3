using System.Collections.Generic;
using System.Net.Http;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

/// <summary>
///     Class that allows the user to view the recipes they can complete
/// </summary>
public class FoundRecipeViewModel
{
    #region Properties

    /// <summary>Gets or sets the recipes that can be completed.</summary>
    /// <value>a list of The recipes.</value>
    public List<Recipe>? Recipes { get; set; }

    /// <summary>Gets or sets the selected recipe title.</summary>
    /// <value>The selected recipe title.</value>
    public string? SelectedRecipeTitle { get; set; }

    #endregion

    #region Methods

    /// <summary>Gets the recipes the user can currently complete with their pantry.</summary>
    /// <param name="userId">The id of the current user.</param>
    /// <param name="client"> the client to connect to the backend</param>
    /// <returns>
    ///     a list of recipes that the user can make with their current pantry ingredients or an empty list
    /// </returns>
    public List<Recipe>? GetRecipes(int userId, HttpClient client)
    {
        this.Recipes = new List<Recipe>();
        var connection = new HttpClientConnection();
        var retrieved = connection.GetRecipes(userId, client);
        this.Recipes.AddRange(retrieved.Result);

        return this.Recipes;
    }

    #endregion
}