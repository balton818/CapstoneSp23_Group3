CREATE PROCEDURE [dbo].[create_meal_plan]
	@mealPlanDate DATE,
	@userId INT
AS
	DECLARE @firstOfWeek DATE; 
	EXEC @firstOfWeek = [dbo].[get_first_of_week] @date = @mealPlanDate;

	INSERT INTO [meal_plan](meal_plan_date, [user_id]) VALUES(@firstOfWeek, @userId);
	EXEC [dbo].[get_meal_plan] @firstOfWeek, @userId;

