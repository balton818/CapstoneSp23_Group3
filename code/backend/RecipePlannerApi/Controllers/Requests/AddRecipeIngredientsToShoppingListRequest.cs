using RecipePlannerApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Controllers.Requests {
    [ExcludeFromCodeCoverage]
    public class AddRecipeIngredientsToShoppingListRequest {
        public List<Ingredient> Ingredients { get; set; }
        public int UserId { get; set; }
    }
}
