namespace Team3DesktopApp.Model;

/// <summary>
///     The ingredient class that allows the proper formatting for recipe ingredients.
/// </summary>
public class Ingredient
{
    #region Properties

    /// <summary>
    ///     Gets or sets the name of the ingredient.
    /// </summary>
    /// <value>
    ///     The name as a string
    /// </value>
    public string IngredientName { get; set; }


    /// <summary>
    ///     Gets or sets the quantity of the ingredient.
    /// </summary>
    /// <value>
    ///     The quantity as an int
    /// </value>
    public int Quantity { get; set; }

    /// <summary>Gets or sets the unit of measurement for the ingredient.</summary>
    /// <value>The unit.</value>
    public string Unit { get; set; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="Ingredient" /> class.</summary>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
    /// <param name="unit">the unit of measurement</param>
    public Ingredient(string name, int quantity, string unit)
    {
        this.IngredientName = name;
        this.Quantity = quantity;
        this.Unit = unit;
    }

    #endregion
}