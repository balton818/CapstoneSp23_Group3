CREATE PROCEDURE [dbo].[get_meal_plan]
	@date DATE
AS
	DECLARE @firstOfWeek DATE = get_first_of_week(@date)
	SELECT 
		  meal_plan_id AS mealPlanId
		, meal_plan_date AS mealPlanDate
		, [user_id] AS userId
	FROM [dbo].[meal_plan]
	WHERE @firstOfWeek = meal_plan_date
