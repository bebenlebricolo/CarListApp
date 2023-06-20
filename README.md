# CarListApp
CarListApp as per developped following Udemy training + personnal touch

# Getting started with the CarListApi

## Database (SQLite) deployment
This REST api uses .Net core 7+ and connects to a local database [carlist.db](carlist.db), which needs to be deployed at :
`C:\CarListApi\carlist.db` on Windows and `/var/db/CarListApi/carlist.db` on Linux machines.
Note that you will need to make those files readable/writable by at least your username when running the server locally.
```bash
sudo chown -R <username>:wheel /var/db/CarListApi
sudo chmod +w /var/db/CarListApi/carlist.db # or chmod 777 because that's for demonstration purposes!
```

## Building the server
You can use Visual Studio to achieve these tasks easily, but on Linux we can use `dotnet` directly :
```bash
dotnet build -c <Configuration : Debug|Release> CarListApi/
dotnet run -c <Configuration : Debug|Release> CarListApi/
```

Or using tasks from VSCode.

## Server configuration
Server port configuration is done in the [launchSettings.json](CarListApi/Properties/launchSettings.json)

# Getting started with CarListApp (.net Maui)
For now, this one needs Visual Studio ... so it needs Windows.
Unless we find the proper workaround (and some seems to exist on the web, but not supported yet).
