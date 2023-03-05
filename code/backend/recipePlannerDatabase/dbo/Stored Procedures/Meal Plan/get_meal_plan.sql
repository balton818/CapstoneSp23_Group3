CREATE PROCEDURE [dbo].[get_meal_plan]
	@date DATE,
	@userId INT
AS
	DECLARE @firstOfWeek DATE; 
	EXEC @firstOfWeek = [dbo].[get_first_of_week] @date = @date;
	
	SELECT 
		  meal_plan_id		AS MealPlanId
		, meal_plan_date	AS MealPlanDate
		, [user_id]			AS UserId
	FROM [dbo].[meal_plan]
	WHERE meal_plan_date = @firstOfWeek
		AND [user_id] = @userId
