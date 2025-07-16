dotnet dev-certs https --trust

dotnet clean
dotnet build
dotnet run

dotnet ef migrations add InitialCreate

dotnet ef database update

DROP TABLE IF EXISTS Movimientos;
DROP TABLE IF EXISTS CuentasBancarias;
DROP TABLE IF EXISTS Usuarios;