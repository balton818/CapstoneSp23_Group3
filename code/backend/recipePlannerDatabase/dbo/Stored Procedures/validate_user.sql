CREATE PROCEDURE [dbo].[validate_user]
	@username NVARCHAR(20),
	@password NVARCHAR(20)
AS
	SELECT user_id AS Id FROM [user] WHERE username = @username and [password] = @password
