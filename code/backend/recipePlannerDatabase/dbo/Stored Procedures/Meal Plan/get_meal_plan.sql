CREATE PROCEDURE [dbo].[get_meal_plan]
	@date DATE,
	@userId INT
AS
	DECLARE @firstOfWeek DATE; 
	EXEC @firstOfWeek = [dbo].[get_first_of_week] @date = @date;
	
	SELECT 
		  meal_plan_id AS mealPlanId
		, meal_plan_date AS mealPlanDate
		, [user_id] AS userId
	FROM [dbo].[meal_plan]
	WHERE meal_plan_date = @firstOfWeek
		AND [user_id] = @userId
