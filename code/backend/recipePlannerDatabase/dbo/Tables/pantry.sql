CREATE TABLE [dbo].[pantry]
(
    [pantry_id] INT IDENTITY(1,1) PRIMARY KEY,
	[user_id] INT NOT NULL , 
    [ingredient_name] NVARCHAR(20) NOT NULL, 
    [quantity] INT NOT NULL, 
    CONSTRAINT [FK_ingredient_user] FOREIGN KEY ([user_id]) REFERENCES [user]([user_id]), 
    UNIQUE ([user_id], [ingredient_name])
)
