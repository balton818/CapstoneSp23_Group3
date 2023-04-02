namespace RecipePlannerApi.Model {
    public class ShoppingListIngredient: Ingredient {

        /// <summary>Gets or sets the shopping list identifier.</summary>
        /// <value>The shopping list identifier.</value>
        public int? ShoppingListId { get; set; }

        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the quantity unit.
        /// </summary>
        /// <value>The unit.</value>
        public AppUnit UnitId { get; set; }
    }
}
