using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IShoppingListService _shoppingListService;
        public UserController(IUserService userService, IShoppingListService shoppingListService)
        {
            this._userService = userService;
            this._shoppingListService = shoppingListService;
        }

        [HttpGet("{username},{password}")]
        public ActionResult<int?> ValidateUser(string username, string password)
        {
            try
            {
                return Ok(this._userService.ValidateUser(username, password));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create")]
        public ActionResult<int?> CreateUser(User user)
        {
            try
            {
                return Ok(this._userService.CreateUser(user));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("get-pantry/{userId}")]
        public ActionResult<List<PantryItem>> GetPantry(int userId)
        {
            try
            {
                return Ok(this._userService.GetUserPantry(userId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("add-pantry-item")]
        public ActionResult<PantryItem> AddPantryItem(PantryItem item)
        {
            try
            {
                return Ok(this._userService.AddPantryItem(item));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("update-pantry-item")]
        public ActionResult<PantryItem> UpdatePantryItem(PantryItem item)
        {
            try
            {
                return Ok(this._userService.UpdatePantryItem(item));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("remove-pantry-item/{pantryId}")]
        public ActionResult RemovePantryItem(int pantryId)
        {
            try
            {
                this._userService.RemovePantryItem(pantryId);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("get-shopping-list/{userId}")]
        public ActionResult<List<ShoppingListIngredient>> GetShoppingList(int userId)
        {
            try
            {
                return Ok(this._shoppingListService.GetShoppingList(userId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("add-shopping-list-ingredient")]
        public ActionResult<ShoppingListIngredient> AddShoppingListIngredient(ShoppingListIngredient ingredient)
        {
            try
            {
                return Ok(this._shoppingListService.UpsertShoppingListIngredient(ingredient));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("update-shopping-list-ingredient")]
        public ActionResult<ShoppingListIngredient> UpdateShoppingListIngredient(ShoppingListIngredient ingredient)
        {
            try
            {
                return Ok(this._shoppingListService.UpsertShoppingListIngredient(ingredient));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("remove-shopping-list-ingredient/{shoppinglistId}")]
        public ActionResult RemoveShoppingListIngredient(int shoppinglistId)
        {
            try
            {
                this._shoppingListService.DeleteShoppingListIngredient(shoppinglistId);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}
