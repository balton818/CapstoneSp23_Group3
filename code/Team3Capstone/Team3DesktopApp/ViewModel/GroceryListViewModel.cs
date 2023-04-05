using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

public class GroceryListViewModel
{
    #region Properties

    /// <summary>Gets or sets the pantry.</summary>
    /// <value>The pantry.</value>
    public List<GroceryListItem>? GroceryList { get; set; } = new();

    #endregion

    #region Methods

    /// <summary>Gets the grocery list of the current user.</summary>
    /// <param name="userId">The current user id.</param>
    /// <param name="client"> the client used to connect to the backend</param>
    /// <returns>
    ///     the users previously entered pantry or an empty list
    /// </returns>
    public async Task<List<GroceryListItem>?> GetGroceryList(int userId, HttpClient client)
    {
        this.GroceryList = new List<GroceryListItem>();
        var connection = new HttpClientConnection();
        this.GroceryList = await connection.GetGroceryList(userId, client);
        return this.GroceryList;
    }

    /// <summary>Adds the ingredient entered by the user.</summary>
    /// <param name="userId">The currently logged in user.</param>
    /// <param name="name">The name of the ingredient being added.</param>
    /// <param name="quantity">The quantity of the addition.</param>
    /// <param name="unit">The unit of measurement for the addition.</param>
    /// <param name="client"> the client used to connect to the backend</param>
    public async Task<GroceryListItem> AddIngredient(int userId, string? name, int quantity, HttpClient client,
        string unit)
    {
        var groceryListItem = new GroceryListItem();
        groceryListItem.UserId = userId;
        groceryListItem.IngredientName = name;
        groceryListItem.Quantity = quantity;
        groceryListItem.UnitId = this.getUnit(unit);
        var connection = new HttpClientConnection();
        return await connection.AddGroceryItem(groceryListItem, client);
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
                return UnitEnum.FluidOunces;
            default:
                return UnitEnum.None;
        }
    }

    /// <summary>Edits the ingredient amount.</summary>
    /// <param name="name">The name of the ingredient being edited.</param>
    /// <param name="quantity">The new quantity of the ingredient.</param>
    /// <param name="client">Client to connect to backend</param>
    public async Task<GroceryListItem> EditIngredientAmount(string? name, int quantity, HttpClient client)
    {
        this.getItem(name);
        var groceryItem = this.getItem(name);
        if (groceryItem != null)
        {
            groceryItem.Quantity = quantity;
            var connection = new HttpClientConnection();
            return await connection.EditGroceryItem(groceryItem, client);
        }

        return null!;
    }

    private GroceryListItem? getItem(string? name)
    {
        foreach (var item in this.GroceryList!)
        {
            if (item.IngredientName!.Equals(name))
            {
                return item;
            }
        }

        return null;
    }

    /// <summary>Removes the ingredient from the users grocery list.</summary>
    /// <param name="name">The name of the ingredient.</param>
    /// <param name="quantity">The quantity being removed.</param>
    /// <param name="client">The client used to connect to the backend.</param>
    /// <returns>
    ///     true if the ingredient was removed successfully false otherwise
    /// </returns>
    public Task<bool> RemoveIngredient(string? name, int quantity, HttpClient client)
    {
        var groceryItem = this.getItem(name);
        groceryItem.Quantity = quantity;
        var connection = new HttpClientConnection();
        return connection.RemoveGroceryItem(groceryItem, client);
    }

    public async Task AddNeededGroceriesToList(List<int> recipeIds, int userId,
        HttpClient client)
    {
        var connection = new HttpClientConnection();
        this.GroceryList = await connection.AddToGroceryListByRecipeId(recipeIds, userId, client);
    }

    /// <summary>Buys the grocery items marked by the use and adds them to the pantry.</summary>
    /// <param name="ingredients">The ingredients purchased.</param>
    /// <param name="userId">The current users id.</param>
    /// <param name="client">The client to connect to the backend.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public void BuyGroceryItems(Dictionary<string, int> ingredients, int userId, HttpClient client)
    {
        var itemsToBuy = new List<GroceryListItem>();
        var difference = new GroceryListItem();
        var connection = new HttpClientConnection();
        foreach (var currentIngredient in ingredients.Keys)
        {
            if (this.getItem(currentIngredient) != null)
            {
                var itemToBuy = this.getItem(currentIngredient)!;
                if (ingredients[currentIngredient] < itemToBuy.Quantity)
                {
                    difference.IngredientName = itemToBuy.IngredientName;
                    difference.Quantity = itemToBuy.Quantity - ingredients[currentIngredient];
                    itemToBuy.Quantity = ingredients[currentIngredient];
                    difference.UnitId = itemToBuy.UnitId;
                    difference.UserId = itemToBuy.UserId;
                }

                if (itemToBuy.Quantity > 0)
                {
                    itemsToBuy.Add(itemToBuy);
                }
            }

            if (difference.IngredientName != null && difference.Quantity > 0)
            {
                connection.AddGroceryItem(difference, client);
            }
        }

        if (itemsToBuy.Count > 0)
        {
            connection.BuyIngredientsFromList(itemsToBuy, userId, client);
        }
    }

    /// <summary>Clears the current grocery list.</summary>
    /// <param name="userid">The userid for the logged in user.</param>
    /// <param name="clientToSet">The client to set to connect to the backend.</param>
    public async Task ClearGroceryList(int userid, HttpClient clientToSet)
    {
        if (this.GroceryList == null)
        {
            await this.GetGroceryList(userid, clientToSet);
        }

        var groceryListItems = this.GroceryList;
        if (groceryListItems != null)
        {
            foreach (var item in groceryListItems)
            {
                await this.RemoveIngredient(item.IngredientName, item.Quantity, clientToSet);
            }
        }
    }

    #endregion
}