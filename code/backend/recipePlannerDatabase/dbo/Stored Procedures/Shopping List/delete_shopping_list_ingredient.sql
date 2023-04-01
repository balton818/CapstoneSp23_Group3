CREATE PROCEDURE [dbo].[delete_shopping_list_ingredient]
  @shoppingListId INT
AS
  DELETE FROM [dbo].[shopping_list] WHERE shopping_list_id = @shoppingListId
