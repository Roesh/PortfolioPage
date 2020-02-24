#Run the script with & ".\databaseSetup.ps1 -m <mode>"

param (
    [string]$m = "all"    
 )

 
 function dbCommands{
    dotnet ef migrations add InitialCreate --context PortfolioPageContext
    dotnet ef database update --context PortfolioPageContext 
 }
 function razorCommands{
    dotnet aspnet-codegenerator razorpage -m project -dc PortfolioPageContext -udl -outDir Pages\ProjectTracker
    dotnet aspnet-codegenerator razorpage -m projectComponent -dc PortfolioPageContext -udl -outDir Pages\ProjectTracker\Component
    dotnet aspnet-codegenerator razorpage -m projectUpdate -dc PortfolioPageContext -udl -outDir Pages\ProjectTracker\Update
 }

if($m -eq "all"){
    dbCommands
    razorCommands
}
if($m -eq "dbonly"){
    dbCommands
}
if($m -eq "razoronly"){
    razorCommands
}
    
 
