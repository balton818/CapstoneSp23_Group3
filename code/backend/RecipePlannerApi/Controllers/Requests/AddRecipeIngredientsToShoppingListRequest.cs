using RecipePlannerApi.Model;

namespace RecipePlannerApi.Controllers.Requests {
    public class AddRecipeIngredientsToShoppingListRequest {
        public List<Ingredient> Ingredients { get; set; }
        public int UserId { get; set; }
    }
}
