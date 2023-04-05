CREATE PROCEDURE [dbo].[remove_meal]
	@mealId INT
AS
	DELETE FROM [dbo].[meal] WHERE meal_id = @mealId
