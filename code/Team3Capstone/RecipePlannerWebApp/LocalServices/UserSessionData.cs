using RecipePlannerApi.Api.Requests;
using RecipePlannerApi.Model;

namespace RecipePlannerWebApp.LocalServices
{
    public class UserSessionData
    {
        public User CurrentUser { get; set; } = new User();
        
        public string? CurrentRecipeTitle { get; set; }
        public List<string>? RecipeIngredients { get; set; }
        public List<string>? RecipeSteps { get; set; }

        public BrowseRecipeRequest? lastExplorePageRequest { get; set; }
    }
}
