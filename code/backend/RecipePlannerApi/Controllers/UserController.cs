using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;

namespace RecipePlannerApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        public UserController() { }

        [HttpGet("{username},{password}")]
        public ActionResult<bool> ValidateUser(string username, string password) =>
            UserService.ValidateUser(new User() { Username = username, Password = password});

        [HttpPost("create")]
        public ActionResult<int?> CreateUser(User user) =>
            UserService.CreateUser(user);

        [HttpPost("get-pantry/{userId}")]
        public ActionResult<List<PantryItem>> GetPantry(int userId) =>
            UserService.GetUserPantry(userId);

        [HttpPost("add-pantry-item")]
        public ActionResult<PantryItem> AddPantryItem(PantryItem item) =>
            UserService.AddPantryItem(item);

        [HttpPost("update-pantry-item")]
        public void UpdatePantryItem(PantryItem item) =>
            UserService.UpdatePantryItem(item);

        [HttpPost("remove-pantry-item/{pantryId}")]
        public void RemovePantryItem(int pantryId) =>
            UserService.RemovePantryItem(pantryId);

    }
}
