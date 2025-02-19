FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

COPY . .

RUN dotnet restore ProdMonitor.Web/ProdMonitor.Web.csproj

RUN dotnet publish ProdMonitor.Web/ProdMonitor.Web.csproj -c Release -o /app/out

WORKDIR /app/out

EXPOSE 5091
ENV ASPNETCORE_URLS=http://+:5091
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "ProdMonitor.Web.dll"]
