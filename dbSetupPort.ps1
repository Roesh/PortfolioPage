# Run the script with & ".\dbSetupPort.ps1 -m <mode>" 
# Where <mode> is "dbonly" for database creation only or "razoronly". Default is "all" which does both

param (
    [string]$m = "all"        
)
 
if($m -eq "h"){
    Write-Output "Run the script with & .\dbSetupPort.ps1 -m <mode>" 
    Write-Output "Where <mode> is 'dbonly' for database creation only or 'razoronly'. Default is 'all' which does both"
}

function dbCommands{
    dotnet ef migrations add InitialCreate
    dotnet ef database update
}

function razorCommands{
    dotnet aspnet-codegenerator razorpage -m project -dc ApplicationDbContext -udl -outDir Pages\ProjectTracker
    dotnet aspnet-codegenerator razorpage -m projectComponent -dc ApplicationDbContext -udl -outDir Pages\ProjectTracker\Component
    dotnet aspnet-codegenerator razorpage -m projectUpdate -dc ApplicationDbContext -udl -outDir Pages\ProjectTracker\Update
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
    
 
