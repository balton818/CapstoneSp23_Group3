CREATE TABLE [dbo].[pantry]
(
    [pantry_id] INT IDENTITY(1,1) PRIMARY KEY,
	[user_id] INT NOT NULL , 
    [ingredient_id] INT NULL, 
    [ingredient_name] NVARCHAR(40) NOT NULL, 
    [quantity] INT NOT NULL, 
    [unit_id] INT DEFAULT 0 NOT NULL, 
    CONSTRAINT [FK_pantry_user] FOREIGN KEY ([user_id]) REFERENCES [user]([user_id]) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT [FK_pantry_measurement_unit] FOREIGN KEY ([unit_id]) REFERENCES [measurment_unit]([unit_id]) ON UPDATE CASCADE ON DELETE NO ACTION,
    UNIQUE ([user_id], [ingredient_name])
)
