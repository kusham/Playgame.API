# Play Game API

## Play.Catalog.Service
```powershell
dotnet new webapi -n Play.Catalog.Service
dotnet add package MongoDB.Driver
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Binder
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Http.Polly
dotnet add package MassTransit.AspNetCore
dotnet add package MassTransit.RabbitMQ
```

## Docker For the service
```powershell
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
docker compose up
docker compose down
docker compose up -d
```

## See what ports are currently in use
```powershell
 Get-NetTCPConnection -State Listen
```

## Create the Common Service
```powershell
dotnet new classlib -n Play.Common --framework net5.0
```

## Create a Nuget package
```powershell
dotnet pack -o ../../../packages
dotnet pack -p:PackageVersion=1.0.1 -o ../../../packages
```

## Add Nuget package to the Play.Catalog.Service
```powershell
dotnet nuget add source F:\.NET\Playgame.API\packages -n Play.Common
dotnet add package Play.Common
```

## Add refference of a project
```powershell
dotnet add reference ..\Play.Catalog.Constract\Play.Catalog.Constract.csproj
```