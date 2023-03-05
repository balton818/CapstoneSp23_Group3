CREATE TABLE [dbo].[meal]
(
	[meal_id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [meal_plan_id] INT NOT NULL,
    [day_of_week] INT NOT NULL,
    [meal_type] INT NOT NULL, 
    [recipe_id] INT NULL, 
    CONSTRAINT [fk_meal_meal_plan] FOREIGN KEY ([meal_plan_id]) REFERENCES [meal_plan]([meal_plan_id]),
    CONSTRAINT [fk_meal_recipe] FOREIGN KEY ([recipe_id]) REFERENCES [recipe]([recipe_id]),
    UNIQUE([meal_plan_id], [day_of_week], [meal_type])
)
