namespace RecipePlannerApi.Model
{
    public class Ingredient
    {
        public object name;
        public object quantity;
        public int? IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
}
