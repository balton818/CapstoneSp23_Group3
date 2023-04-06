using Moq;
using RecipePlannerApi.Dao.Interface;
using RecipePlannerApi.Model;
using RecipePlannerApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipePlannerApiTests.TestServices.TestUserService {
    public class TestUpdatePantryItems {
        [Fact]
        public void TestValidPantryItems() {
            var userDao = new Mock<IUserDao>();
            var pantryDao = new Mock<IPantryDao>();
            var pantryItem = new List<PantryItem>{ new PantryItem {
                IngredientId = null,
                IngredientName = "apple",
                Quantity = 1,
                PantryId = 2,
                UnitId = AppUnit.NONE,
                UserId = 1
            } };

            pantryDao.Setup(x => x.UpdatePantryItem(It.IsAny<PantryItem>())).Returns(new PantryItem());
            pantryDao.Setup(x => x.AddPantryItem(It.IsAny<PantryItem>())).Returns(new PantryItem());
            pantryDao.Setup(x => x.GetUserPantry(It.IsAny<int>())).Returns(new List<PantryItem>());
            var userService = new UserService(userDao.Object, pantryDao.Object);
            var result = userService.UpdatePantryItems(pantryItem, 1);

            Assert.NotNull(result);
        }
    }
}
