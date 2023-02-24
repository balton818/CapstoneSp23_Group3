using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipePlannerApi.Dao;
using RecipePlannerApi.Service;

namespace RecipePlannerApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase {
        [HttpGet("cuisines")]
        public ActionResult<List<string>> getAppCuisines() {
            try {
                return Ok(AppService.getAppCuisines());
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("diet")]
        public ActionResult<List<string>> getAppDiets() {
            try {
                return Ok(AppService.getAppDiets());
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("meal-types")]
        public ActionResult<List<string>> getAppMealTypes() {
            try {
                return Ok(AppService.getAppMealTypes());
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}
