using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Api.Requests {
    [ExcludeFromCodeCoverage]
    public class ConvertAmountRequest {
        /// <summary>
        /// Gets or sets the name of the ingredient.
        /// </summary>
        /// <value>The name of the ingredient.</value>
        public string IngredientName { get; set; }
        /// <summary>
        /// Gets or sets the source amount.
        /// </summary>
        /// <value>The source amount.</value>
        public decimal SourceAmount { get; set; }
        /// <summary>
        /// Gets or sets the source unit.
        /// </summary>
        /// <value>The source unit.</value>
        public string SourceUnit { get; set; }
        /// <summary>
        /// Gets or sets the target unit.
        /// </summary>
        /// <value>The target unit.</value>
        public string TargetUnit { get; set; }
    }
}
