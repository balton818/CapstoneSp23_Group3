using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase {
        public RecipeController() { }

        [HttpGet("by-ingredients")]
        public List<Recipe> SearchRecipesByIngredients([FromQuery]SearchRecipesByIngredientsRequest request) =>
            RecipeService.SearchRecipesByIngredients(request);
    }
}
