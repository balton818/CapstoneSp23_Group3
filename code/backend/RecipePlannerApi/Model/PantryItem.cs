using System.Diagnostics.CodeAnalysis;

namespace RecipePlannerApi.Model {
    [ExcludeFromCodeCoverage]
    public class PantryItem {
        /// <summary>
        /// Gets or sets the pantry identifier.
        /// </summary>
        /// <value>The pantry identifier.</value>
        public int? PantryId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int? UserId { get; set; }

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
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit identifier.
        /// </summary>
        /// <value>The unit identifier.</value>
        public AppUnit UnitId { get; set; }
    }
}
