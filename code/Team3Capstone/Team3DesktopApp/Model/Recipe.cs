namespace Team3DesktopApp.Model;

/// <summary>
/// The recipe class which defines a recipe
/// </summary>
public class Recipe
{
    #region Properties

    /// <summary>Gets or sets the identifier for recipe.</summary>
    /// <value>The database recipe identifier.</value>
    public int? Id { get; set; }

    /// <summary>Gets or sets the title of the recipe.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }


    #endregion
}