CREATE TABLE [dbo].[user] (
    [user_id] int PRIMARY KEY IDENTITY(1,1),
    [username] NVARCHAR (20) UNIQUE NOT NULL,
    [password] NVARCHAR (20) NOT NULL,
    [first_name] NVARCHAR (50) NOT NULL,
    [last_name] NVARCHAR (50) NOT NULL,
    [email] NVARCHAR (30),
    [address] NVARCHAR (30),
    [address_two] NVARCHAR (30), 
    [city] NVARCHAR(50), 
    [state] NCHAR(2), 
    [zipcode] NCHAR(5),
);

