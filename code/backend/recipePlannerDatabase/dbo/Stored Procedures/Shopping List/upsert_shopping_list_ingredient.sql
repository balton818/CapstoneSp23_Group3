CREATE PROCEDURE [dbo].[upsert_shopping_list_ingredient]
  @userId INT
  , @shoppingListId INT = NULL
  , @ingredientName NVARCHAR(40)
  , @quantity INT
  , @unitId INT
AS

  IF @shoppingListId IS NULL
    BEGIN
      INSERT INTO [dbo].[shopping_list]
      (
          [user_id]
          ,[ingredient_name]
          ,[quantity]
          ,[unit_id]
      )
      VALUES
      (
          @userId
          , @ingredientName
          , @quantity
          , @unitId
      )
      SET @shoppingListId = SCOPE_IDENTITY()
    END
  ELSE
    UPDATE [dbo].[shopping_list] 
    SET
      [ingredient_name] = @ingredientName
      ,[quantity] = @quantity
      ,[unit_id] = @unitId
    WHERE shopping_list_id = @shoppingListId


  EXEC [dbo].[get_shopping_list_ingredient] @shoppingListId