using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service.Interface;

namespace RecipePlannerApi.Service
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IShoppingListDao _shoppingListDao;

        public ShoppingListService(IShoppingListDao shoppingListDao)
        {
            _shoppingListDao = shoppingListDao;
        }

        public ShoppingListIngredient UpsertShoppingListIngredient(ShoppingListIngredient ingredient)
        {
            return _shoppingListDao.UpsertShoppingListIngredient(ingredient);
        }

        public List<ShoppingListIngredient> CompareLists(List<PantryItem> pantry, List<ShoppingListIngredient> shopping)
        {
            List<ShoppingListIngredient> editedList = new List<ShoppingListIngredient>();
            foreach (var item in shopping)
            {
                var pantryItem = pantry.Find(i => i.IngredientName.Equals(item.IngredientName));
                if (pantryItem != null)
                {
                    if (pantryItem.UnitId != item.UnitId)
                    {
                        MeasurementService mesureMeasureService = new MeasurementService();
                        item.UnitId = (AppUnit)mesureMeasureService.Convert(item.Quantity, item.UnitId.ToString(), pantryItem.UnitId);
                    }
                    if (pantryItem.Quantity >= item.Quantity)
                    {
                        continue;
                    }
                    else
                    {
                        item.Quantity -= pantryItem.Quantity;
                        editedList.Add(item);
                    }
                }
                else
                {
                    editedList.Add(item);
                }
            }
            return editedList;
        }

        public void DeleteShoppingListIngredient(int shoppingListId)
        {
            _shoppingListDao.DeleteShoppingListIngredient(shoppingListId);
        }

        public List<ShoppingListIngredient> GetShoppingList(int userId)
        {
            return _shoppingListDao.GetShoppingList(userId);
        }

        public List<ShoppingListIngredient> AddToShoppingList(List<ShoppingListIngredient> toAddList, int userId)
        {
            var shoppingList = this.GetShoppingList(userId);
            foreach (var ingredient in toAddList)
            {
                var listIngredient = shoppingList.Find(i => i.IngredientId == ingredient.IngredientId || i.IngredientName == ingredient.IngredientName);
                if (listIngredient != null)
                {
                    ingredient.ShoppingListId = listIngredient.ShoppingListId;
                    ingredient.Quantity += ingredient.Quantity;
                }
            }

            foreach (var ingredient in toAddList)
            {
                this.UpsertShoppingListIngredient(ingredient);
            }

            return this.GetShoppingList(userId);
        }

        public void DeleteAllFromShoppingList(List<ShoppingListIngredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient.ShoppingListId == null)
                {
                    continue;
                }

                this.DeleteShoppingListIngredient(ingredient.ShoppingListId.Value);
            }
        }
    }
}
