using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
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
    /// <param name="client"></param>
    /// <returns>
    ///     <br />
    /// </returns>
    public async Task<List<PantryItem>> GetPantry(int userId, HttpClient client)
    {
        this.Pantry = new List<PantryItem>();
        var connection = new HttpClientConnection();
        var retrieved = await connection.GetPantry(userId, client);
        this.Pantry.AddRange(retrieved);
        return this.Pantry;
    }

    /// <summary>Adds the ingredient.</summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
    /// <param name="client"></param>
    public async Task<PantryItem> AddIngredient(int userId, string name, int quantity, HttpClient client, string unit)
    {
        var pantryItem = new PantryItem();
        pantryItem.UserId = userId;
        pantryItem.IngredientName = name;
        pantryItem.Quantity = quantity;
        pantryItem.Unit = this.getUnit(unit);
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
            default:
                return UnitEnum.None;
        }
    }

    /// <summary>Edits the ingredient amount.</summary>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
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

    public Task<bool> RemoveIngredient(string name, int quantity, HttpClient client)
    {

        var pantryItem = this.getItem(name);
        pantryItem.Quantity = quantity;
        var connection = new HttpClientConnection();
        return connection.RemovePantryItem(pantryItem, client);
    }

    #endregion
}