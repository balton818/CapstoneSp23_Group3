CREATE PROCEDURE [dbo].[get_shopping_list_ingredient]
  @shoppingListId INT
AS
  SELECT 
		 [shopping_list_id]   AS ShoppingListId
		,[user_id]       	  AS UserId
		,[ingredient_name]    AS IngredientName
		,[ingredient_id]	  AS IngredientId
		,[quantity]           AS Quantity
		,[unit_id]            AS UnitId
  FROM [dbo].[shopping_list] 
  WHERE shopping_list_id = @shoppingListId
