namespace Team3DesktopApp.Model;

/// <summary>
///     <br />
/// </summary>
public class Ingredient
{
    #region Properties

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    /// <value>
    ///     The name.
    /// </value>
    public string ingredientName { get; set; }

    /// <summary>
    ///     Gets or sets the quanitiy.
    /// </summary>
    /// <value>
    ///     The quanitiy.
    /// </value>
    public int quantity { get; set; }

    public UnitEnum UnitId { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="Ingredient" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
    public Ingredient(string name, int quantity)
    {
        this.ingredientName = name;
        this.quantity = quantity;
    }

    #endregion
}