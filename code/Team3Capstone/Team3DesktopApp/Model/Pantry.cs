using System.Collections;

namespace Team3DesktopApp.Model
{
    public class Pantry
    {
        public ArrayList Ingredients { get; private set; }

        public Pantry()
        {
            this.Ingredients = new ArrayList();
        }


        public void AddIngredient(Ingredient ingredient)
        {
            this.Ingredients.Add(ingredient);
        }

        public Ingredient? GetIngredient(string name)
        {
            foreach (Ingredient ingredient in this.Ingredients)
            {
                if (ingredient.Name == name)
                {
                    return ingredient;
                }
            }
            return null;
        }
    }
}
