using System.Collections.Generic;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

/// <summary>
///     <br />
/// </summary>
public class PantryViewModel
{
    #region Properties

    /// <summary>Gets or sets the pantry.</summary>
    /// <value>The pantry.</value>
    public List<PantryItem> Pantry { get; set; }

    #endregion

    #region Methods

    /// <summary>Gets the pantry.</summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>
    ///     <br />
    /// </returns>
    public List<PantryItem> getPantry(int userId)
    {
        this.Pantry = new List<PantryItem>();
        var connection = new HttpClientConnection();
        var retrieved = connection.GetPantry(userId);
        this.Pantry.AddRange(retrieved.Result);
        return this.Pantry;
    }

    /// <summary>Adds the ingredient.</summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
    public async Task addIngredient(int userId, string name, int quantity)
    {
        var pantryItem = new PantryItem();
        pantryItem.UserId = userId;
        pantryItem.IngredientName = name;
        pantryItem.Quantity = quantity;
        var connection = new HttpClientConnection();
        await connection.AddPantryItem(pantryItem);
    }

    /// <summary>Edits the ingredient amount.</summary>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
    public async Task editIngredientAmount(string name, int quantity)
    {
        this.getItem(name);
        var pantryItem = this.getItem(name);
        pantryItem.Quantity = quantity;
        var connection = new HttpClientConnection();
        await connection.EditPantryItem(pantryItem);
    }

    private PantryItem getItem(string name)
    {
        foreach (var item in this.Pantry)
        {
            if (item.IngredientName.Equals(name))
            {
                return item;
            }
        }

        return new PantryItem();
    }

    #endregion
}