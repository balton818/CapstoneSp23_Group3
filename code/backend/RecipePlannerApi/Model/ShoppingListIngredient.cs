using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {

    [ExcludeFromCodeCoverage]
    public class ShoppingListIngredient {

        /// <summary>Gets or sets the shopping list identifier.</summary>
        /// <value>The shopping list identifier.</value>
        public int? ShoppingListId { get; set; }

        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }

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
        public AppUnit UnitId { get; set; }
    }
}
