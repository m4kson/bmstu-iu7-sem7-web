#!/bin/bash
set -e

echo "Starting integration tests..."

# 1. Убедитесь, что база данных доступна и готова к работе
echo "Waiting for PostgreSQL to be available..."

# Проверка на доступность порта 5432 для PostgreSQL (можно настроить под контейнер, если используется)
until nc -z -v -w30 localhost 5432; do
  echo "Waiting for PostgreSQL to be ready..."
  sleep 5
done

# 2. Выполнение миграций базы данных (если нужно)
echo "Running database migrations..."
dotnet ef database update --project src/ProdMonitor/ProdMonitor.Infrastructure/ProdMonitor.Infrastructure.csproj

# 3. Запуск интеграционных тестов
echo "Running integration tests..."
dotnet test src/ProdMonitor/ProdMonitor.IntegrationTests/ProdMonitor.IntegrationTests.csproj --logger:trx

# 4. Проверка результата тестов
if [ $? -eq 0 ]; then
  echo "Integration tests passed successfully!"
else
  echo "Integration tests failed!"
  exit 1
fi
