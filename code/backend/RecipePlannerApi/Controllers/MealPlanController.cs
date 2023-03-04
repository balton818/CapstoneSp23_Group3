using Microsoft.AspNetCore.Mvc;
using RecipePlannerApi.Controllers.Requests;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase {

        private IMealPlanService mealPlanService;

        public MealPlanController(IMealPlanService mealPlanService) {
            this.mealPlanService = mealPlanService;
        }

        [HttpGet("get-meal-plan")]
        public ActionResult<MealPlan> GetMealPlan([FromQuery] GetMealPlanRequest request) {
            try {
                return Ok(this.mealPlanService.GetMealPlan(request));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }
    }
}
