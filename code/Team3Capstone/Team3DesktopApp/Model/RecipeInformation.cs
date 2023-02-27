using System.Collections.Generic;
using System.Security.Permissions;
using System.Windows.Media;

namespace Team3DesktopApp.Model;

/// <summary>
/// </summary>
public class RecipeInformation
{
    #region Properties

    /// <summary>
    ///     Gets or sets the summary.
    /// </summary>
    /// <value>
    ///     The summary.
    /// </value>
    public string Summary { get; set; }

    /// <summary>
    ///     Gets or sets the ingredients.
    /// </summary>
    /// <value>
    ///     The ingredients.
    /// </value>
    public List<Ingredient> Ingredients { get; set; }

    /// <summary>
    ///     Gets or sets the steps.
    /// </summary>
    /// <value>
    ///     The steps.
    /// </value>
    public List<RecipeStep> Steps { get; set; }

    public ImageSource Image { get; set; }

    #endregion
}