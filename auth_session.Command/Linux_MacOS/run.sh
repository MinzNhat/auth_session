#!/bin/bash

dotnet clean
dotnet restore

echo ""
echo "Running migrations for all databases..."
echo ""

# Chạy Migration cho tất cả DbContext
echo "AuthDb..."
dotnet ef migrations add InitialCreate_Auth --context AuthDbContext --project auth_session.Infrastructure --startup-project auth_session.API
echo ""
echo "SalesProjDb..."
dotnet ef migrations add InitialCreate_SalesProj --context SalesProjDbContext --project auth_session.Infrastructure --startup-project auth_session.API
echo ""
echo "DataWarehouseDb..."
dotnet ef migrations add InitialCreate_DataWarehouse --context DataWarehouseDbContext --project auth_session.Infrastructure --startup-project auth_session.API

echo ""
echo "Updating for all databases..."
echo ""
# Cập nhật database
echo "AuthDb..."
dotnet ef database update --context AuthDbContext --project auth_session.Infrastructure --startup-project auth_session.API
echo ""
echo "SalesProjDb..."
dotnet ef database update --context SalesProjDbContext --project auth_session.Infrastructure --startup-project auth_session.API
echo ""
echo "DataWarehouseDb..."
dotnet ef database update --context DataWarehouseDbContext --project auth_session.Infrastructure --startup-project auth_session.API

echo ""
echo "Migrations applied!"
echo ""

echo ""
echo "Running project!"
echo ""
dotnet run --project auth_session.API