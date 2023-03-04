CREATE TABLE [dbo].[meal_plan]
(
	[meal_plan_id] INT NOT NULL PRIMARY KEY, 
    [meal_plan_date] DATE NOT NULL, 
    [user_id] INT NOT NULL,
    CONSTRAINT [fk_meal_plan_user] FOREIGN KEY ([user_id]) REFERENCES [user]([user_id]),
    UNIQUE([user_id], [meal_plan_date])
)
