using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

internal class GroceryListViewModel
{
    #region Properties

    /// <summary>Gets or sets the pantry.</summary>
    /// <value>The pantry.</value>
    public List<GroceryListItem>? GroceryList { get; set; } = new List<GroceryListItem>();

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

    public Task<List<PantryItem>> BuyGroceryItems(List<Ingredient> ingredients, int userId, HttpClient client)
    {
        List<GroceryListItem> itemsToBuy = new List<GroceryListItem>();
        var connection = new HttpClientConnection();
        foreach (var currentIngredient in ingredients)
        {
            if (this.getItem(currentIngredient.IngredientName) != null)
            {
                GroceryListItem itemToBuy = this.getItem(currentIngredient.IngredientName)!;
                itemsToBuy.Add(itemToBuy);
            }
        }
        var pantry = connection.BuyIngredientsFromList(itemsToBuy, userId, client);
        return pantry;
    }
    public Task<List<PantryItem>> BuyGroceryItems(List<string> ingredients, int userId, HttpClient client)
    {
        List<GroceryListItem> itemsToBuy = new List<GroceryListItem>();
        var connection = new HttpClientConnection();
        foreach (var currentIngredient in ingredients)
        {
            if (this.getItem(currentIngredient) != null)
            {
                GroceryListItem itemToBuy = this.getItem(currentIngredient)!;
                itemsToBuy.Add(itemToBuy);
            }
        }
        var pantry = connection.BuyIngredientsFromList(itemsToBuy, userId, client);
        return pantry;
    }
    #endregion

    public void RemoveIngredientsOnRemoveMeal(List<Ingredient> ingredients, HttpClient client)
    {
        foreach (var ingredient in ingredients)
        {
            var groceryItem = this.getItem(ingredient.IngredientName);
            if (groceryItem != null)
            {
                this.RemoveIngredient(groceryItem.IngredientName, groceryItem.Quantity, client);
            }
        }
    }
}