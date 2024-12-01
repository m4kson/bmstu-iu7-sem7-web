FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

COPY . .

RUN dotnet restore ProdMonitor.Web/ProdMonitor.Web.csproj

RUN dotnet publish ProdMonitor.Web/ProdMonitor.Web.csproj -c Release -o /app/out

WORKDIR /app/out

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ProdMonitor.Web.dll"]
