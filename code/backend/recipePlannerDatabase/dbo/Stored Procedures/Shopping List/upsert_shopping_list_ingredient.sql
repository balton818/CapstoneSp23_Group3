CREATE PROCEDURE [dbo].[upsert_shopping_list_ingredient]
  @userId INT
  , @shoppingListId INT = NULL
  , @ingredientName NVARCHAR(40)
  , @ingredientId INT = NULL
  , @quantity INT
  , @unitId INT
AS

  IF @shoppingListId IS NULL
    BEGIN
      INSERT INTO [dbo].[shopping_list]
      (
          [user_id]
          ,[ingredient_name]
          ,[ingredient_id]
          ,[quantity]
          ,[unit_id]
      )
      VALUES
      (
          @userId
          , @ingredientName
          , @ingredientId
          , @quantity
          , @unitId
      )
      SET @shoppingListId = SCOPE_IDENTITY()
    END
  ELSE
    UPDATE [dbo].[shopping_list] 
    SET
      [ingredient_name] = @ingredientName
      ,[ingredient_id] = @ingredientId
      ,[quantity] = @quantity
      ,[unit_id] = @unitId
    WHERE shopping_list_id = @shoppingListId


  EXEC [dbo].[get_shopping_list_ingredient] @shoppingListId