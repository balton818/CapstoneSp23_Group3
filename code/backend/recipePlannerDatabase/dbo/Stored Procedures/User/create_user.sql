CREATE PROCEDURE [dbo].[create_user]
    @username NVARCHAR (20),
    @password NVARCHAR (20),
    @first_name NVARCHAR (50),
    @last_name NVARCHAR (50),
    @email NVARCHAR (30)
AS
	INSERT INTO [user] ([username], [password], [first_name], [last_name], [email]) 
    VALUES (@username, @password, @first_name, @last_name, @email)

    SELECT user_id as Id from [user] where user_id = SCOPE_IDENTITY()
