using System.Collections.Generic;
using System.Windows.Media;

namespace Team3DesktopApp.Model;

/// <summary>
///     Class that defines and formats recipe information
/// </summary>
public class RecipeInformation
{
    #region Properties

    /// <summary>
    ///     Gets or sets the summary which is brief info about the recipe.
    /// </summary>
    /// <value>
    ///     The summary of the recipe.
    /// </value>
    public string? Summary { get; set; }

    /// <summary>
    ///     Gets or sets the ingredients.
    /// </summary>
    /// <value>
    ///     The ingredients required for making the recipe and the amounts needed.
    /// </value>
    public List<Ingredient>? Ingredients { get; set; }

    /// <summary>
    ///     Gets or sets the steps.
    /// </summary>
    /// <value>
    ///     A list of steps for cooking the recipe.
    /// </value>
    public List<RecipeStep>? Steps { get; set; }

    /// <summary>Gets or sets the image.</summary>
    /// <value>The image of the recipe.</value>
    public ImageSource? Image { get; set; }

    #endregion
}