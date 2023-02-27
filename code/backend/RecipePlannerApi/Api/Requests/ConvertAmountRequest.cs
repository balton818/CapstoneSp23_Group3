using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Api.Requests {
    [ExcludeFromCodeCoverage]
    public class ConvertAmountRequest {
        public string IngredientName { get; set; }
        public decimal SourceAmount { get; set; }
        public string SourceUnit { get; set; }
        public string TargetUnit { get; set; }
    }
}
