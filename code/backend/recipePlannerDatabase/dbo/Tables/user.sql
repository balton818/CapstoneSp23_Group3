CREATE TABLE [dbo].[user] (
    [user_id] int PRIMARY KEY IDENTITY(1,1),
    [username] NVARCHAR (20) UNIQUE NOT NULL,
    [password] NVARCHAR (20) NOT NULL,
    [first_name] NVARCHAR (50) NOT NULL,
    [last_name] NVARCHAR (50) NOT NULL,
    [email] NVARCHAR (30) UNIQUE
);

