# Run the script with & ".\dbSetupID.ps1 -m <mode>" 
# Where <mode> is "dbonly" for database creation only or "razoronly". Default is "all" which does both

param (
    [string]$m = "all"    
 )

if($m -eq "h"){
    Write-Output "Run the script with & .\dbSetupID.ps1 -m <mode>" 
    Write-Output "Where <mode> is 'dbonly' for database creation only or 'razoronly'. Default is 'all' which does both"
}

function dbCommandsIdentity{
    dotnet ef migrations add InitialCreate --context ApplicationDbContext
    dotnet ef database update --context ApplicationDbContext
}

if($m -eq "all"){
    dbCommandsIdentity    
}
if($m -eq "dbonly"){
    dbCommandsIdentity
}
if($m -eq "razoronly"){
}
    
 
