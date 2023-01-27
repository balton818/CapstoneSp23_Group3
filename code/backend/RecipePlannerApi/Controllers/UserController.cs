using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        public UserController() { }

        [HttpGet("{username}, {password}")]
        public ActionResult<bool> ValidateUser(string username, string password) =>
            UserService.ValidateUser(new User() { Username = username, Password = password});
    }
}
