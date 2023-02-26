namespace RecipePlannerApi.Api.Requests {
    public class ConvertAmountRequest {
        public string IngredientName { get; set; }
        public decimal SourceAmount { get; set; }
        public string SourceUnit { get; set; }
        public string TargetUnit { get; set; }
    }
}
