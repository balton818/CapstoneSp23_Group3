using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

/// <summary>
///     the viewmodel for the pantrypage
/// </summary>
public class PantryViewModel
{
    #region Properties

    /// <summary>Gets or sets the pantry.</summary>
    /// <value>The pantry.</value>
    public List<PantryItem> Pantry { get; set; }

    #endregion

    #region Methods

    /// <summary>Gets the pantry of the current user.</summary>
    /// <param name="userId">The current user id.</param>
    /// <param name="client"> the client used to connect to the backend</param>
    /// <returns>
    ///     the users previously entered pantry or an empty list
    /// </returns>
    public async Task<List<PantryItem>> GetPantry(int userId, HttpClient client)
    {
        this.Pantry = new List<PantryItem>();
        var connection = new HttpClientConnection();
        var retrieved = await connection.GetPantry(userId, client);
        this.Pantry.AddRange(retrieved);
        return this.Pantry;
    }

    /// <summary>Adds the ingredient entered by the user.</summary>
    /// <param name="userId">The currently logged in user.</param>
    /// <param name="name">The name of the ingredient being added.</param>
    /// <param name="quantity">The quantity of the addition.</param>
    /// <param name="unit">The unit of measurement for the addition.</param>
    /// <param name="client"> the client used to connect to the backend</param>
    public async Task<PantryItem> AddIngredient(int userId, string name, int quantity, HttpClient client, string unit)
    {
        var pantryItem = new PantryItem();
        pantryItem.UserId = userId;
        pantryItem.IngredientName = name;
        pantryItem.Quantity = quantity;
        pantryItem.UnitId = this.getUnit(unit);
        var connection = new HttpClientConnection();
        return await connection.AddPantryItem(pantryItem, client);
    }

    private UnitEnum getUnit(string unit)
    {
        switch (unit)
        {
            case "Ml":
                return UnitEnum.Milliliters;
            case "g":
                return UnitEnum.Grams;
            case "Oz":
                return UnitEnum.Ounces;
            case "Fluid Oz":
                return UnitEnum.Fluid_Ounces;
            default:
                return UnitEnum.None;
        }
    }

    /// <summary>Edits the ingredient amount.</summary>
    /// <param name="name">The name of the ingredient being edited.</param>
    /// <param name="quantity">The new quantity of the ingredient.</param>
    public async Task<PantryItem> EditIngredientAmount(string name, int quantity, HttpClient client)
    {
        this.getItem(name);
        var pantryItem = this.getItem(name);
        pantryItem.Quantity = quantity;
        var connection = new HttpClientConnection();
        return await connection.EditPantryItem(pantryItem, client);
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

    /// <summary>Removes the ingredient from the users pantry.</summary>
    /// <param name="name">The name of the ingredient.</param>
    /// <param name="quantity">The quantity being removed.</param>
    /// <param name="client">The client used to connect to the backend.</param>
    /// <returns>
    ///   true if the ingredient was removed successfully false otherwise
    /// </returns>
    public Task<bool> RemoveIngredient(string name, int quantity, HttpClient client)
    {

        var pantryItem = this.getItem(name);
        pantryItem.Quantity = quantity;
        var connection = new HttpClientConnection();
        return connection.RemovePantryItem(pantryItem, client);
    }

    #endregion
}