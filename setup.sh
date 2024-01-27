#!/bin/bash


SQL_SERVER_HOST="=DESKTOP-0G4DA0T\\SQLEXPRESS"
SQL_SERVER_USER="=DESKTOP-0G4DA0T\\SQLEXPRESS\\Hernan"
SQL_SERVER_PASSWORD=""
SQL_SCRIPT_PATH="Documentos/script.sql"

# Configuración de la aplicación React y .NET Core
NET_CORE_APP_PATH="C:/Users/Hernan/source/repos/NotasEnsolvers/NotasEnsolvers/NotasEnsolvers.csproj"
REACT_APP_PATH="C:/Users/Hernan/source/repos/NotasEnsolvers/NotasEnsolvers/ClientApp/index.js"


echo "Creando la base de datos y registros en SQL Server..."
sqlcmd -S $SQL_SERVER_HOST -U $SQL_SERVER_USER -P $SQL_SERVER_PASSWORD -i $SQL_SCRIPT_PATH


echo "Iniciando la aplicación .NET Core..."
cd $NET_CORE_APP_PATH
dotnet run &

sleep 20

echo "Iniciando la aplicación React..."
cd $REACT_APP_PATH
npm start


# Fin del script