CREATE PROCEDURE [dbo].[create_user]
    @username NVARCHAR (20),
    @password NVARCHAR (20),
    @first_name NVARCHAR (50),
    @last_name NVARCHAR (50),
    @email NVARCHAR (30),
    @address NVARCHAR (30),
    @address_two NVARCHAR (30), 
    @city NVARCHAR(50), 
    @state NCHAR(2), 
    @zipcode NCHAR(5)
AS
	INSERT INTO [user] ([username], [password], [first_name], [last_name], [email], [address], [address_two], [city], [state], [zipcode]) 
    VALUES (@username, @password, @first_name, @last_name, @email, @address, @address_two, @city, @state, @zipcode)

    SELECT user_id as Id from [user] where user_id = SCOPE_IDENTITY()
