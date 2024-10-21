How to Run the Project:

1.	Go to the BracketGenerator.Backend folder.
2.	Open BracketGenerator.Backend.sln in Visual Studio (I created the projects using VS 2022 and .NET 8).
3.	Run the project.
4.	In the console, you can choose the tournament type (1 for World Cup, 2 for NCAA Soccer).
5.	This will read data from the Data/countries32.json and Data/countries64.json files.
6.	If you unload those files, it will automatically create teams using a number.
7.	You can run the UnitTests by right click on TournamentBracketGenerator.Tests

(All the points are covered in the code, including bonus points. Extend the code to support a 64-team bracket similar to NCAA soccer and to include the group stage of the World Cup. Additionally, the code contains Solid Principles, Dependency Injection, and include unit tests with at least 80% coverage, along with Design Patterns.)

Project structure:

1.	The solution file (.sln) is at the root.
2.	The main project folder "TournamentBracketGenerator" contains:
   
    a.	The project file (.csproj)
  	
    b.	Program.cs (contains the Main method)
  	
    c.	Subfolders for organizing code:
  	
        i.	Interfaces: Contains all interface definitions.
        ii.	Models: Contains the concrete implementations of ITeam, IMatch, and IGroup.
        iii. Services: Contains the concrete implementations of other interfaces and main logic classes.
        iv.	Data: Contains the JSON files for team data.
  	
4.	A separate project for unit tests (TournamentBracketGenerator.Tests)
