using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
    public class ShoppingListService : IShoppingListService {
        private readonly IUserDao _userDao;
        private readonly IShoppingListDao _shoppingListDao;

        public ShoppingListService(IUserDao userDao, IShoppingListDao shoppingListDao) {
            _userDao = userDao;
            _shoppingListDao = shoppingListDao;
        }

        public ShoppingListIngredient UpsertShoppingListIngredient(ShoppingListIngredient ingredient) {
            return _shoppingListDao.UpsertShoppingListIngredient(ingredient);
        }

        public void DeleteShoppingListIngredient(int shoppingListId) {
            _shoppingListDao.DeleteShoppingListIngredient(shoppingListId);
        }

        public List<ShoppingListIngredient> GetShoppingList(int userId) {
            return _shoppingListDao.GetShoppingList(userId);
        }

        public List<ShoppingListIngredient> CompleteShoppingList(List<ShoppingListIngredient> list, int userId) {
            foreach (ShoppingListIngredient ingredient in list) {
                if (ingredient.ShoppingListId.HasValue) {
                    _shoppingListDao.DeleteShoppingListIngredient(ingredient.ShoppingListId.Value);
                }
            }

            return _shoppingListDao.GetShoppingList(userId);
        }
    }
}
