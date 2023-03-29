CREATE PROCEDURE [dbo].[update_meal]
	@mealId INT,
	@dayOfWeek INT,
	@mealType INT,
	@apiId INT,
	@title NVARCHAR(150),
	@imageUrl NVARCHAR(150),
	@imageType NVARCHAR(50)
AS
	DECLARE @recipeId INT = (SELECT recipe_id FROM [dbo].[recipe] WHERE api_id = @apiId)

	IF @recipeId IS NULL 
		EXEC @recipeId = [dbo].[create_recipe] @apiId, @title, @imageUrl, @imageType
		
	UPDATE [dbo].[meal]
	SET day_of_week 	= @dayOfWeek,
		meal_type 		= @mealType,
		recipe_id 		= @recipeId
	WHERE meal_id = @mealId

	EXEC [dbo].[get_meal] @mealId
