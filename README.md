# Northwind App
## About the app
<br />
Warehouse application with the usage of Northwind sample DB.

## Tech used 

- C#/Net
  - .Net Core 5
  - MVC
  - Vanilla JS
  - Bootstrap
  - MSSQL


## Preview
<br />
Live demo: // coming soon...
<br />
Deployed to: Azure(coming soon...)
<br />


## Features of the application
- List of Tables of the DB
- CRUD functionality for the tables;
- User interface;
- Searching filters;

## Installation
- download this sample DB from this repo [https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs]
- Import the DB to your MSSQL server and then connect the .Net core with connection string.
- Add .env varible to LaunSettings.json (change the DB with your connection string ) ;
- cd to projects directory
-dotnet run (should open localhost:5001 if you havent change it from launchsettings.json);
