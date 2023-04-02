using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
    public class ShoppingListService : IShoppingListService {
        private readonly IShoppingListDao _shoppingListDao;

        public ShoppingListService(IShoppingListDao shoppingListDao) {
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

        public List<ShoppingListIngredient> AddToShoppingList(List<ShoppingListIngredient> toAddList, int userId) {
            var shoppingList = this.GetShoppingList(userId);
            foreach (var ingredient in toAddList) {
                var listIngredient = shoppingList.Find(i => i.IngredientId == ingredient.IngredientId || i.IngredientName == ingredient.IngredientName);
                if(listIngredient != null) {
                    ingredient.ShoppingListId = listIngredient.ShoppingListId;
                    ingredient.Quantity += ingredient.Quantity;
                }
            }

            toAddList = toAddList.Where(i => i.Quantity > 1).ToList();

            foreach(var ingredient in toAddList) {
                this.UpsertShoppingListIngredient(ingredient);
            }

            return this.GetShoppingList(userId);
        }

        public void DeleteAllFromShoppingList(List<ShoppingListIngredient> ingredients) {
            foreach (var ingredient in ingredients) {
                if(ingredient.ShoppingListId == null) {
                    continue;
                }

                this.DeleteShoppingListIngredient(ingredient.ShoppingListId.Value);
            }
        }
    }
}
