#!/bin/bash


# 3. Запуск интеграционных тестов
echo "Running integration tests..."
dotnet test src/ProdMonitor/ProdMonitor.IntegrationTests/ProdMonitor.IntegrationTests.csproj

