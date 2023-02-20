namespace Team3DesktopApp.Model;

/// <summary>
/// </summary>
public class PantryItem
{
    #region Properties

    /// <summary>
    ///     Gets or sets the pantry identifier.
    /// </summary>
    /// <value>
    ///     The pantry identifier.
    /// </value>
    public int? PantryId { get; set; }

    /// <summary>
    ///     Gets or sets the user identifier.
    /// </summary>
    /// <value>
    ///     The user identifier.
    /// </value>
    public int? UserId { get; set; }

    /// <summary>
    ///     Gets or sets the name of the ingredient.
    /// </summary>
    /// <value>
    ///     The name of the ingredient.
    /// </value>
    public string IngredientName { get; set; }

    /// <summary>
    ///     Gets or sets the quantity.
    /// </summary>
    /// <value>
    ///     The quantity.
    /// </value>
    public int Quantity { get; set; }

    #endregion
}