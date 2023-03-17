using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {
    [ExcludeFromCodeCoverage]
    public class Ingredient {
        /// <summary>
        /// Gets or sets the ingredient identifier.
        /// </summary>
        /// <value>The ingredient identifier.</value>
        public int? IngredientId { get; set; }
        /// <summary>
        /// Gets or sets the name of the ingredient.
        /// </summary>
        /// <value>The name of the ingredient.</value>
        public string IngredientName { get; set; }
        /// <summary>
        /// Gets or sets the quantity of ingredients.
        /// </summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the quantity unit.
        /// </summary>
        /// <value>The unit.</value>
        public string Unit { get; set; }
    }
}
