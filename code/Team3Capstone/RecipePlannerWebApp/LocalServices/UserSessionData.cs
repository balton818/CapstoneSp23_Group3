using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerWebApp.LocalServices
{
    public class UserSessionData
    {
        public User CurrentUser { get; set; } = new User();
        
        public string? CurrentRecipeTitle { get; set; }

        public BrowseRecipeRequest? lastExplorePageRequest { get; set; }

        public Meal? SelectedMeal { get; set; }
        public bool NewMeal { get; set; }
        public bool UpdateMeal { get; set; }
        public Weeks SelectedWeek { get; set; } = Weeks.WEEK1;
        public enum Weeks { WEEK1, WEEK2 };
        public Dictionary<ShoppingListIngredient, bool> userShoppingSelection { get; set; } = new Dictionary<ShoppingListIngredient, bool>();
        public Dictionary<int, bool> userSelectedIDs { get; set; } = new Dictionary<int, bool>();
    }
}
