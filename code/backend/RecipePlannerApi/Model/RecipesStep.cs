using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {
    [ExcludeFromCodeCoverage]
    public class RecipesStep {
        public int stepNumber { get; set; }
        public string instructions { get; set; }
    }
}
