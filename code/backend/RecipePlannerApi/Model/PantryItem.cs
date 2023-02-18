namespace RecipePlannerApi.Model {
    public class PantryItem {
        public int? PantryId { get; set; }
        public int? UserId { get; set; }
        public string IngredientName { get; set; }
        public int Quantity { get; set; }
        public AppUnits unit { get; set; }
    }
}
