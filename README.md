# Play Game API

## Play.Catalog.Service
```powershell
dotnet new webapi -n Play.Catalog.Service
dotnet add package MongoDB.Driver
```

## Docker For the service
```powershell
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
```

## See what ports are currently in use
```powershell
 Get-NetTCPConnection -State Listen
```