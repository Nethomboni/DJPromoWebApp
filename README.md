# DJPromoWebApp (ASP.NET Core MVC + EF Core + SQL Server)

## Run
1. Install the .NET 8 SDK (and SQL Server LocalDB, which ships with Visual Studio).
2. Edit `ConnectionStrings:DJContext` in `appsettings.json` if you are not using LocalDB
   (put your server name / password in there).
3. `dotnet restore` then `dotnet run` (or press the IIS Express / https run button in Visual Studio).

On first start the app calls `EnsureCreated()` and seeds sample DJs, venues and gigs.

## Using migrations instead (optional)
Replace `context.Database.EnsureCreated();` in `Program.cs` with `context.Database.Migrate();`, then:

    dotnet tool install --global dotnet-ef
    dotnet ef migrations add InitialCreate
    dotnet ef database update

## Deploying to Azure App Service
1. Create an Azure SQL database (note server name, admin user, password).
2. SQL server > Networking: tick "Allow Azure services and resources to access this server" and add your own IP.
3. Web App > Settings > Environment variables > Connection strings: add `DJContext` = your Azure SQL connection string, type `SQLAzure`.
   (Or fill in `appsettings.Production.json`, but do not commit real passwords.)
4. Web App > Configuration > Stack settings: .NET 8.
5. Publish, then check Monitoring > Log stream if the site does not load.
