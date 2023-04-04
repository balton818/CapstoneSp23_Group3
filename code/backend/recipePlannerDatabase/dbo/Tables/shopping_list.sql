CREATE TABLE [dbo].[shopping_list] (
    [shopping_list_id] INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY CLUSTERED ([shopping_list_id] ASC),
    [user_id]          INT           NOT NULL,
    [ingredient_name]  NVARCHAR (40) NOT NULL,
    [ingredient_id]    INT           DEFAULT NULL,
    [quantity]         INT           NOT NULL,
    [unit_id]          INT           DEFAULT 0 NOT NULL,
    CONSTRAINT [FK_shopping_list_measurement_unit] FOREIGN KEY ([unit_id]) REFERENCES [dbo].[measurment_unit] ([unit_id]) ON UPDATE CASCADE,
    CONSTRAINT [FK_shopping_list_user] FOREIGN KEY ([user_id]) REFERENCES [dbo].[user] ([user_id]) ON DELETE CASCADE ON UPDATE CASCADE,
    UNIQUE NONCLUSTERED ([user_id] ASC, [ingredient_name] ASC)
);
