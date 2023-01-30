# Group 3 - Brianna Irie, Jordan Wagner, and Ben Alton



# Instructions to Run Locally

## Database

### Tools

- Visual Studio
- Local SQL Server Database Server
- Azure Data Studio
  - Extension: SQL Server Dacpac

### To Get Data From Live Server

1. Open Azure Data Studio and press Add Connection
3. Enter info and press Connect
   - Server : mysqlserver33.database.windows.net
   - Authentication type : SQL Login
   - User name : azureuser
   - Password : Group32023
   - Database: capstone
4. Right click live server connection then select Data-tier Application Wizard
5. Select 4th option: Export the schema and data from a database to the logical .bacpac file format then Export
7. Go to Data-tier Wizard on your local server and select the third option to import .bacpac file
8. Change connection string in the server to the connection string for your new local database

### Update Local Server With repo schema

1. In Visual Studio right click RecipePlannerDatabase and select Publish
2. Target Database to local database  then publish

