using Microsoft.AspNetCore.Mvc;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService) {
            this._recipeService = recipeService;
        }

        [HttpGet("test/search")]
        public ActionResult<List<SearchRecipesByIngredients200ResponseInner>> SearchRecipes([FromQuery] SearchRecipesByIngredientsRequest request) {
            try {
                return Ok(this._recipeService.SearchRecipes(request));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-by-ingredients")]
        public ActionResult<List<Recipe>> SearchRecipesByIngredients([FromQuery] SearchRecipesByIngredientsRequest request) {
            try {
                return Ok(this._recipeService.SearchRecipesByIngredients(request));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-by-pantry/{userId}")]
        public ActionResult<List<Recipe>> GetRecipesByUserPantry(int userId) {
            try {
                return Ok(this._recipeService.GetRecipesByUserPantry(userId));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-recipe-infromation/{id}")]
        public ActionResult<RecipeInformation> GetRecipeInformation(int id) {
            try {
                return Ok(this._recipeService.GetRecipeInformation(id));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("search-ingredient")]
        public ActionResult<List<Ingredient>> SearchIngredients([FromQuery] string search) {
            try {
                return Ok(this._recipeService.SearchIngredient(search));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("browse")]
        public ActionResult<BrowseRecipeResponse> BrowseRecipes([FromQuery] BrowseRecipeRequest request) {
            try {
                return Ok(this._recipeService.BrowseRecipes(request));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}
