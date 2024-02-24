# Play Game API

## Play.Catalog.Service
```powershell
dotnet new webapi -n Play.Catalog.Service
dotnet add package MongoDB.Driver
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Binder
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Http.Polly
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
```

## Add Nuget package to the Play.Catalog.Service
```powershell
dotnet nuget add source F:\.NET\Playgame.API\packages -n Play.Common
dotnet add package Play.Common
```