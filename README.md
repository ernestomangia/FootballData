# FootballData API

This app allows you to do the following actions:

1) Import a League with its Teams and Players by using this URL:
http://localhost:<port>/import-league/{leagueCode}

2) You can get the total number of players that participate in a League by using this URL:
http://localhost:<port>/total-players/{leagueCode}

## Stack
The app was built using the following technologies:

1. .Net Framework 4.5.2
2. Web Api 2
3. Entity Framework 6
4. MS SQL Server
5. Autommaper
6. Async calls 
7. Swagger
8. Repository pattern
9. MSUnit and Moq

## Setup
1. Clone the repository
2. Open the solution in Visual Studio
3. Set up a DB conection on the Web.config file that belongs to the FootballData.WebApi project. Note: the DB will be auto generated by EF the first time you run the app
4. Do a "Restore Nuget Packages" and build the entire solution
5. Run it!