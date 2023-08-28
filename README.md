MenuListApp is created using Microsoft Visual Studio 2022 Community Edition and Microsoft SQL Server 2019 Developer Edition.
The main layout of the client is based on the standard application layout of Radzen Blazor Studio application.

Listed below are three alternative ways to build and run this app:

## Installation Instructions with cmd.

### Required Resources:
1. [SDK .net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. [SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### To build and run this app: 
1. Clone the repository `git clone https://github.com/KwnGou/MenuListApp.git`
2. Create the DB using the Microsoft SQL Server Management Studio.
   1. Open the Microsoft SQL Server Management Studio.
   2. Drag and drop the file `MenuListDBScript.sql` into the editor.
   3. Execute the entire file.
3. Run the following in a CMD line:
   1. `cd MenuListApp`
   2. `dotnet run .\MenuListApp.csproj` (Keep this open)
4. In a separate CMD line, run the following:
   1. `cd Server`
   2. `dotnet run .\MenuListApp.Server.csproj`

## Alternate Instructions (with Visual Studio).

### Required Resources:
1. [SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/downloads/)

### To build and run this app: 
1. Clone the repository `git clone https://github.com/KwnGou/MenuListApp.git`
2. Create the DB using the Microsoft SQL Server Management Studio.
    1. Open the Microsoft SQL Server Management Studio.
    2. Drag and drop the file `MenuListDBScript.sql` into the editor.
    3. Execute the entire file.
3. Open the solution MenuListApp.sln.
    1. Run the Server project.
  
## Alternate Instructions (with IIS).

### Required Resources:
1. [SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/downloads/)
3. [Internet Information Services (IIS)](https://www.how2shout.com/how-to/how-to-enable-iis-internet-information-services-on-windows-11.html)

### To build and run this app:
1. Clone the repository `git clone https://github.com/KwnGou/MenuListApp.git`
2. Create the DB using the Microsoft SQL Server Management Studio.
    1. Open the Microsoft SQL Server Management Studio.
    2. Drag and drop the file `MenuListDBScript.sql` into the editor.
    3. Execute the entire file.

3. Publish the server project to a folder.
4. Create a user account with a fixed password (check "user cannot change password" and write down the password).
5. Grant to the user account access to the database (Data reader and Data writer roles on the Db).

6. In IIS Manager create an application pool and set the pool identity to the account created above.
      1. Create a website using the folder where u published the project running under the application pool created above.
      2. Bind the site to a port different than the default or disable the default site.
      3. Use the browser and navigate to the site.

### Screenshots:

Menu grid with inline add, edit and delete.
![image](https://github.com/KwnGou/MenuListApp/assets/110529457/193f75ca-13d3-4449-aa1a-a3b8386487ad)

Plate edit dialogue.
![image](https://github.com/KwnGou/MenuListApp/assets/110529457/eff5120c-3b92-43dc-aedd-373f196acff0)

Shopping list dialogue.
![image](https://github.com/KwnGou/MenuListApp/assets/110529457/9b26f61f-1956-43e8-802c-d824e3c77222)
![image](https://github.com/KwnGou/MenuListApp/assets/110529457/d6368621-a8db-49b9-8daf-338cc3ceb6b6)

