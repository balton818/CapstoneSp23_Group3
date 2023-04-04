CREATE PROCEDURE [dbo].[create_recipe_ingredient]
  @recipeId INT
  , @recipeIngredientName NVARCHAR(40)
  , @quantity INT
  , @unitId INT
AS
  INSERT INTO [dbo].[recipe_ingredients]
    (
        [recipe_id]
        ,[ingredient_name]
        ,[quantity]
        ,[unit_id]
    )
    VALUES
    (
        @recipeId
        , @ingredientName
        , @quantity
        , @unitId
    )

  DECLARE @recipeIngredientId INT = SCOPE_IDENTITY()
  EXEC [dbo].[get_recipe_ingredient] @recipeIngredientId
  
