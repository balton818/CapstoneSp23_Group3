namespace Team3DesktopApp.Model;

/// <summary>
///     The pantry Item class which defines items in the user pantry IE what the user has on hand
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
    public string? IngredientName { get; set; }

    /// <summary>
    ///     Gets or sets the quantity.
    /// </summary>
    /// <value>
    ///     The quantity of the ingredient.
    /// </value>
    public int Quantity { get; set; }

    /// <summary>Gets or sets the unit of measurement.</summary>
    /// <value>The unit of measurement.</value>
    public UnitEnum UnitId { get; set; }

    #endregion
}