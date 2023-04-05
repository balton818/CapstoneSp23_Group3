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

        [HttpGet("add-meal")]
        public ActionResult<Meal> AddMeal([FromQuery] Meal meal) {
            try {
                return Ok(this.mealPlanService.AddMeal(meal));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-this-week")]
        public ActionResult<MealPlan> GetThisWeek([FromQuery] int userId) {
            try {
                return Ok(this.mealPlanService.GetThisWeeksMealPlan(userId));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-next-week")]
        public ActionResult<MealPlan> GetNextWeek([FromQuery] int userId) {
            try {
                return Ok(this.mealPlanService.GetNextWeeksMealPlan(userId));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("update-meal")]
        public ActionResult<Meal> UpdateMeal([FromQuery] Meal meal) {
            try {
                return Ok(this.mealPlanService.UpdateMeal(meal));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("remove-meal")]
        public ActionResult RemoveMeal([FromQuery] int mealId) {
            try {
                this.mealPlanService.RemoveMeal(mealId);
                return Ok();
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }
    }
}
