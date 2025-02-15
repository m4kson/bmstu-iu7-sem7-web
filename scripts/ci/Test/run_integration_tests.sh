#!/bin/bash
set -e

# echo "Starting integration tests..."

# # 1. Убедитесь, что база данных доступна и готова к работе
# echo "Waiting for PostgreSQL to be available..."

# # Ожидание готовности контейнера PostgreSQL
# until docker exec -it prodmonitor-postgres-container pg_isready -U testuser; do
#   echo "Waiting for PostgreSQL to be ready..."
#   sleep 5
# done

# echo "PostgreSQL is ready."

# # 2. Выполнение миграций базы данных (если нужно)
# echo "Running database migrations..."
# dotnet ef database update --project src/ProdMonitor/ProdMonitor.Infrastructure/ProdMonitor.Infrastructure.csproj --no-build --verbose

# 3. Запуск интеграционных тестов
echo "Running integration tests..."
dotnet test src/ProdMonitor/ProdMonitor.IntegrationTests/ProdMonitor.IntegrationTests.csproj

# Проверка результата тестов
if [ $? -eq 0 ]; then
  echo "Integration tests passed successfully!"
else
  echo "Integration tests failed!"
  exit 1
fi
