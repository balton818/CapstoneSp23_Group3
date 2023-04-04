CREATE PROCEDURE [dbo].[update_recipe_ingredient]
  @recipeIngredientId INT
  , @recipeId INT
  , @ingredientName NVARCHAR(40)
  , @quantity INT
  , @unitId INT
AS
  UPDATE [dbo].[recipe_ingredients] 
  SET
      [recipe_id] = @recipeId
      ,[ingredient_name] = @ingredientName
      ,[quantity] = @quantity
      ,[unit_id] = @unitId
  WHERE recipe_ingredient_id = @recipeIngredientId

  EXEC [dbo].[get_recipe_ingredient] @recipeIngredientId
