using Microsoft.AspNetCore.Mvc;
using Org.OpenAPITools.Model;
using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;
using System.Linq.Expressions;

namespace RecipePlannerApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase {
        public RecipeController() { }

        [HttpGet("search")]
        public ActionResult<List<SearchRecipesByIngredients200ResponseInner>> SearchRecipes([FromQuery] SearchRecipesByIngredientsRequest request) {
            try {
                return Ok(RecipeService.SearchRecipes(request));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-by-ingredients")]
        public ActionResult<List<Recipe>> SearchRecipesByIngredients([FromQuery] SearchRecipesByIngredientsRequest request) {
            try {
                return Ok(RecipeService.SearchRecipesByIngredients(request));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-by-pantry/{userId}")]
        public ActionResult<List<Recipe>> GetRecipesByUserPantry(int userId) {
            try {
                return Ok(RecipeService.GetRecipesByUserPantry(userId));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-recipe-infromation/{id}")]
        public ActionResult<RecipeInformation> GetRecipeInformation(int id) {
            try {
                return Ok(RecipeService.GetRecipeInformation(id));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("test/{to}/{from}/{value}")]
        public ActionResult<int?> GetRecipeInformation(string from, int to, decimal value) {
            try {
                return Ok(MeasurementService.Convert(value, from, (AppUnit)to));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("search-ingredient")]
        public ActionResult<List<Ingredient>> SearchIngredients([FromQuery] string search) {
            try {
                return Ok(RecipeService.SearchIngredient(search));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("browse")]
        public ActionResult<BrowseRecipeResponse> BrowseRecipes([FromQuery] BrowseRecipeRequest request) {
            try {
                return Ok(RecipeService.BrowseRecipes(request));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}
