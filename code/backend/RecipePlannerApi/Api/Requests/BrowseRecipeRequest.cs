using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Api.Requests {
    [ExcludeFromCodeCoverage]
    public class BrowseRecipeRequest {
        public int UserId { get; set; }
        public string Query { get; set; }
        public string Cuisine { get; set; }
        public string Diet { get; set; }
        public string Type { get; set; }
        public int PageNumber { get; set; }
    }
}
