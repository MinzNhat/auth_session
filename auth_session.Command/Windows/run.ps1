dotnet clean
dotnet restore

Write-Host ""
Write-Host "Running migrations for all databases..."
Write-Host ""

# Chạy Migration cho tất cả DbContext
Write-Host "AuthDb..."
dotnet ef migrations add InitialCreate_Auth --context AuthDbContext --project auth_session.Infrastructure --startup-project auth_session.API
Write-Host ""
Write-Host "SalesProjDb..."
dotnet ef migrations add InitialCreate_SalesProj --context SalesProjDbContext --project auth_session.Infrastructure --startup-project auth_session.API
Write-Host ""
Write-Host "DataWarehouseDb..."
dotnet ef migrations add InitialCreate_DataWarehouse --context DataWarehouseDbContext --project auth_session.Infrastructure --startup-project auth_session.API

Write-Host ""
Write-Host "Updating for all databases..."
Write-Host ""
# Cập nhật database
Write-Host "AuthDb..."
dotnet ef database update --context AuthDbContext --project auth_session.Infrastructure --startup-project auth_session.API
Write-Host ""
Write-Host "SalesProjDb..."
dotnet ef database update --context SalesProjDbContext --project auth_session.Infrastructure --startup-project auth_session.API
Write-Host ""
Write-Host "DataWarehouseDb..."
dotnet ef database update --context DataWarehouseDbContext --project auth_session.Infrastructure --startup-project auth_session.API

Write-Host ""
Write-Host "Migrations applied!"
Write-Host ""

Write-Host ""
Write-Host "Running project!"
Write-Host ""
dotnet run --project auth_session.API