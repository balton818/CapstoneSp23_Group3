using System.Collections;

namespace Team3DesktopApp.Model;

/// <summary>
///     The pantry class.
/// </summary>
public class Pantry
{
    #region Properties

    /// <summary>Gets the ingredients.</summary>
    /// <value>The collection of ingredients in the pantry.</value>
    public ArrayList Ingredients { get; }

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="Pantry" /> class.</summary>
    public Pantry()
    {
        this.Ingredients = new ArrayList();
    }

    #endregion

    #region Methods

    /// <summary>Adds the ingredient.</summary>
    /// <param name="ingredient">The ingredient.</param>
    public void AddIngredient(PantryItem ingredient)
    {
        this.Ingredients.Add(ingredient);
    }

    /// <summary>Gets the ingredient.</summary>
    /// <param name="name">The name.</param>
    /// <returns>
    ///     The ingredient with the given name, or null if no ingredient with that name exists.
    /// </returns>
    public Ingredient? GetIngredient(string name)
    {
        foreach (Ingredient ingredient in this.Ingredients)
        {
            if (ingredient.IngredientName == name)
            {
                return ingredient;
            }
        }

        return null;
    }

    #endregion
}