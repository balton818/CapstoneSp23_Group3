namespace Team3DesktopApp.Model;

public class Ingredient
{
    public string Name { get; private set; }
    public int Quantity { get; private set; }

    public Ingredient(string name, int quantity)
    {
        this.Name = name;
        this.Quantity = quantity;
    }

    public void SetQuantity(int quantity)
    {
        this.Quantity = quantity;
    }

    public void SetName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            this.Name = name;
        }

    }
}