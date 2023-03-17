using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {
    [ExcludeFromCodeCoverage]
    public class RecipesStep {
        /// <summary>
        /// Gets or sets the step number.
        /// </summary>
        /// <value>The step number.</value>
        public int stepNumber { get; set; }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>The instructions.</value>
        public string instructions { get; set; }
    }
}
