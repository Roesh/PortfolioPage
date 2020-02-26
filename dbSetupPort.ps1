# Run the script with & ".\dbSetupPort.ps1 -m <mode>" 
# Where <mode> is "dbonly" for database creation only or "razoronly". Default is "all" which does both

param (
    [string]$m = "all"        
)
 
if($m -eq "h"){
    Write-Output "Run the script with & .\dbSetupPort.ps1 -m <mode>" 
    Write-Output "Where <mode> is 'dbonly' for database creation only or 'razoronly'. Default is 'all' which does both"
}

function dbCommandsPortfolio{
    dotnet ef migrations add InitialCreate --context PortfolioPageContext
    dotnet ef database update --context PortfolioPageContext 
}

function razorCommandsPortfolio{
    dotnet aspnet-codegenerator razorpage -m project -dc PortfolioPageContext -udl -outDir Pages\ProjectTracker
    dotnet aspnet-codegenerator razorpage -m projectComponent -dc PortfolioPageContext -udl -outDir Pages\ProjectTracker\Component
    dotnet aspnet-codegenerator razorpage -m projectUpdate -dc PortfolioPageContext -udl -outDir Pages\ProjectTracker\Update
}

if($m -eq "all"){
    dbCommands
    razorCommandsPortfolio
}
if($m -eq "dbonly"){
    dbCommands
}
if($m -eq "razoronly"){
    razorCommandsPortfolio
}
    
 
