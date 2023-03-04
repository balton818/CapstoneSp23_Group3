CREATE PROCEDURE [dbo].[create_meal]
	@mealPlanId INT,
	@dayOfWeek INT,
	@mealType NVARCHAR(50),
	@apiId INT,
	@title NVARCHAR(150),
	@imageUrl NVARCHAR(150),
	@imageType NVARCHAR(50)
AS
	DECLARE @recipeId INT = (SELECT recipe_id FROM [dbo].[recipe] WHERE api_id = @apiId)

	IF @recipeId IS NULL 
		EXEC @recipeId = [dbo].[create_recipe] @apiId, @title, @imageUrl, @imageType

	INSERT INTO [dbo].[meal] 
	([meal_plan_id],[day_of_week], [meal_type], [recipe_id]) VALUES (@mealPlanId, @dayOfWeek, @mealType, @recipeId)

	DECLARE @mealId INT = SCOPE_IDENTITY()
	EXEC [dbo].[get_meal] @mealId
