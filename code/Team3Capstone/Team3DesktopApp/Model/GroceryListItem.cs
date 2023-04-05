namespace Team3DesktopApp.Model;

/// <summary>
///   the grocery list item class for use with the grocery list.
/// </summary>
public class GroceryListItem : ExpanderItem
{
    /// <summary>
    ///     Gets or sets the grocery list identifier.
    /// </summary>
    /// <value>
    ///     The grocery list id
    /// </value>
    public int? ShoppingListId { get; set; }
}