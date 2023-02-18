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
    }
}
