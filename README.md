MenuListApp is created using Microsoft Visual Studio 2022 Community Edition and Microsoft SQL Server 2019 Developer Edition.
The main layout of the client is based on the standard application layout of Radzen Blazor Studio application.

Listed below are three alternative ways to build and run this app:

### Required Resources:
[SDK .net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
[SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

Clone the repository `git clone https://github.com/KwnGou/MenuListApp.git`
Create the DB using the Microsoft SQL Server Management Studio.
   Open the Microsoft SQL Server Management Studio.
   Drag and drop the file `MenuListDBScript.sql` into the editor.
   Execute the entire file.
Run the following in a CMD line:
   `cd MenuListApp`
   `dotnet run .\MenuListApp.csproj` (Keep this open)
In a separate CMD line, run the following:
   `cd Server`
   `dotnet run .\MenuListApp.Server.csproj`

Alternatively

### Required Resources:
[SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
[Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/downloads/)
Clone the repository `git clone https://github.com/KwnGou/MenuListApp.git`
Create the DB using the Microsoft SQL Server Management Studio.
   Open the Microsoft SQL Server Management Studio.
   Drag and drop the file `MenuListDBScript.sql` into the editor.
   Execute the entire file.
Open the solution MenuListApp.sln.
   Run the Server project.
  
Alternatively

### Required Resources:
[SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
[Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/downloads/)
[Internet Information Services (IIS)](Install from windows features)
Clone the repository `git clone https://github.com/KwnGou/MenuListApp.git`
Create the DB using the Microsoft SQL Server Management Studio.
   Open the Microsoft SQL Server Management Studio.
   Drag and drop the file `MenuListDBScript.sql` into the editor.
   Execute the entire file.

Publish the server project to a folder.
Create a user account with a fixed password (check "user cannot change password" and write down the password).
Grant to the user account access to the database (Data reader and Data writer roles on the Db).

In IIS Manager create an application pool and set the pool identity to the account created above.
Create a website using the folder where u published the project running under the application pool created above.
Bind the site to a port different than the default or disable the default site.
Use the browser and navigate to the site.


