using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            this._userService = userService;
        }

        [HttpGet("{username},{password}")]
        public ActionResult<int?> ValidateUser(string username, string password) {
            try {
                return Ok(this._userService.ValidateUser(new User() { Username = username, Password = password}));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create")]
        public ActionResult<int?> CreateUser(User user) {
            try {
                return Ok(this._userService.CreateUser(user));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("get-pantry/{userId}")]
        public ActionResult<List<PantryItem>> GetPantry(int userId) {
            try {
                return Ok(this._userService.GetUserPantry(userId));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("add-pantry-item")]
        public ActionResult<PantryItem> AddPantryItem(PantryItem item) {
            try {
                return Ok(this._userService.AddPantryItem(item));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("update-pantry-item")]
        public ActionResult UpdatePantryItem(PantryItem item) {
            try {
                return Ok(this._userService.UpdatePantryItem(item));
            } catch (Exception e) {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("remove-pantry-item/{pantryId}")]
        public ActionResult RemovePantryItem(int pantryId) {
            try {
                this._userService.RemovePantryItem(pantryId);
                return Ok();
            } catch (Exception e) {

                return BadRequest(e.Message);
            }
        }

    }
}
