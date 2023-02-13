using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Team3DesktopApp.Dal;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

public class PantryViewModel
{
    public List<PantryItem> Pantry { get; set; }

    public List<PantryItem> getPantry(int userId)
    {
        this.Pantry = new List<PantryItem>();
        HttpClientConnection connection = new HttpClientConnection();
        var retrieved = connection.GetPantry(userId);
        this.Pantry.AddRange(retrieved.Result);
        return this.Pantry;
    }

    public async Task addIngredient(int userId, string name, int quantity)
    {
        PantryItem pantryItem = new PantryItem();
        pantryItem.UserId = userId;
        pantryItem.IngredientName = name;
        pantryItem.Quantity = quantity;
        HttpClientConnection connection = new HttpClientConnection();
        await connection.AddPantryItem(pantryItem);
    }

    public async Task editIngredientAmount(string name, int quantity)
    {
        this.getItem(name);
        PantryItem pantryItem = this.getItem(name);
        pantryItem.Quantity = quantity;
        HttpClientConnection connection = new HttpClientConnection();
        await connection.EditPantryItem(pantryItem);

    }

    private PantryItem getItem(string name)
    {
        foreach (PantryItem item in this.Pantry)
        {
            if (item.IngredientName.Equals(name))
            {
                return item;
            }
        }
        return new PantryItem();
    }
}