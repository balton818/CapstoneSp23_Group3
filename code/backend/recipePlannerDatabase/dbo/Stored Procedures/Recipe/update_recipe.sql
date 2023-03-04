CREATE PROCEDURE [dbo].[update_recipe]
	@recipeId INT,
	@apiId INT,
	@title NVARCHAR(150),
	@imageUrl NVARCHAR(150),
	@imageType NVARCHAR(50)
AS
	UPDATE [dbo].[recipe]
	SET title = @title,
		image_url = @imageUrl,
		image_type = @imageType,
		api_id = @apiId
	WHERE recipe_id = @recipeId

	EXEC [dbo].[get_recipe] @recipeId
