using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipePlannerApi.Dao;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase {
        private IAppService _appService;

        public AppController(IAppService appService) {
            this._appService = appService;
        }

        [HttpGet("cuisines")]
        public ActionResult<List<string>> getAppCuisines() {
            try {
                return Ok(this._appService.getAppCuisines());
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("diet")]
        public ActionResult<List<string>> getAppDiets() {
            try {
                return Ok(this._appService.getAppDiets());
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("meal-types")]
        public ActionResult<List<string>> getAppMealTypes() {
            try {
                return Ok(this._appService.getAppMealTypes());
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}
