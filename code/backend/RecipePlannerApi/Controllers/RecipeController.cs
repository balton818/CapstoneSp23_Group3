using Microsoft.AspNetCore.Mvc;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase {
        public RecipeController() { }

        [HttpGet("get-by-ingredients")]
        public List<Recipe> SearchRecipesByIngredients([FromQuery]SearchRecipesByIngredientsRequest request) =>
            RecipeService.SearchRecipesByIngredients(request);

        [HttpGet("get-by-pantry/{userId}")]
        public List<Recipe> GetRecipesByUserPantry(int userId) =>
            RecipeService.GetRecipesByUserPantry(userId);
        [HttpGet("get-recipe-infromation/{id}")]
        public RecipeInformation GetRecipeInformation(int id) =>
            RecipeService.GetRecipeInformation(id);
    }
}
