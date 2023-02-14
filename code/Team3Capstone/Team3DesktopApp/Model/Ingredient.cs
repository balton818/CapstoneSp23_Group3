namespace Team3DesktopApp.Model;

public class Ingredient
{
    public string name { get; set; }
    public int quanitiy { get; set; }

    public Ingredient(string name, int quantity)
    {
        this.name = name;
        this.quanitiy = quantity;
    }
}