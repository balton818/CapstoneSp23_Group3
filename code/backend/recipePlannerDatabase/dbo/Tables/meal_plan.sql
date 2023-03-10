CREATE TABLE [dbo].[meal_plan]
(
	[meal_plan_id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [start_date] DATE NOT NULL, 
    [user_id] INT NOT NULL,
    CONSTRAINT [fk_meal_plan_user] FOREIGN KEY ([user_id]) REFERENCES [user]([user_id]) ON UPDATE CASCADE ON DELETE CASCADE,
    UNIQUE([user_id], [start_date]),
)
