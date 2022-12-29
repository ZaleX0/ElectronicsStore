# Electronics Store
TODO: database and architecture descriptions

# Configuration
## 1. Startup project
Right click on the `ElectronicsStore.Api` project in Solution Explorer and select `Set as Startup Project`

## 2. Database
1. Connection String
    - Change your connection string in [appsettings.json](ReadingList/appsettings.json)
```json
"ConnectionStrings": {
    "Default": "Server=(localdb)\\mssqllocaldb;Database=ElectronicsStore;Trusted_Connection=True;"
}
```

2. Initiate database
    - Run `update-database` command.
> Sample data would be inserted into database automatically by using the [BrandAndProductSeeder](ElectronicsStore.Services/BrandAndProductSeeder.cs)

### You can start the API now

## 3. React Client
The way I open client project is to open `electronicsstore.client` in Visual Studio Code.
- From there run those two commands in terminal.
    - `npm install`
    - `npm start`
>**Note**
> There might be an error saying [Plugin "react" was conflicted between "package.json » eslint-config-react-app »](https://stackoverflow.com/questions/70377211/error-when-deploying-react-app-and-it-keeps-sayings-plugin-react-was-confli)
