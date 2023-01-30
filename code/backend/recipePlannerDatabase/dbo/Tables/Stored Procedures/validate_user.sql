CREATE PROCEDURE [dbo].[validate_user]
	@username nvarchar(20),
	@password nvarchar(20)
AS
	SELECT username from [user] where username = @username and [password] = @password
