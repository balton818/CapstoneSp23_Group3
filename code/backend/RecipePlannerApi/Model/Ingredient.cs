using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {
    [ExcludeFromCodeCoverage]
    public class Ingredient {
        public int? IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
}
