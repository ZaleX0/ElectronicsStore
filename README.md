# Electronics Store
This project is a simple store application with cart and authorization.

### Features
- JWT Bearer Authorization
- Adding multiple products to cart
- Sorting and filtering store items by brand, category, price etc.
- Pagination
- Checking status of your orders (accepted by admin or not)
- Admin panel where you can accept other users orders

# Configuration
## 1. Startup project
Right click on the `ElectronicsStore.Api` project in Solution Explorer and select `Set as Startup Project`

## 2. Database
1. Connection String
    - Change your connection string in [appsettings.json](ElectronicsStore.Api/appsettings.json)
```json
"ConnectionStrings": {
    "Default": "Server=(localdb)\\mssqllocaldb;Database=ElectronicsStore;Trusted_Connection=True;"
}
```

2. Initiate database
    - Open `Package Manager Console`.
    - Run `update-database` command.
> Sample data would be inserted into database automatically by using the [BrandAndProductSeeder](ElectronicsStore.Services/BrandAndProductSeeder.cs)

Default admin user - email: `admin@admin.com`, password: `ZAQ!2wsx`

### You can start the API now

## 3. React Client
The way I open client project is to open `electronicsstore.client` in Visual Studio Code.
- From there run those two commands in terminal.
    - `npm install`
    - `npm start`
>**Note**
> There might be an windows specific error saying [Plugin "react" was conflicted between "package.json » eslint-config-react-app »](https://stackoverflow.com/questions/70377211/error-when-deploying-react-app-and-it-keeps-sayings-plugin-react-was-confli)

# Architecture
- Solution contains four layers ([Api](ElectronicsStore.Api), [Data](ElectronicsStore.Data), [Services](ElectronicsStore.Services), [Client](electronicsstore.client))
- Authorization is done with JWT Bearer tokens
- I used Unit of Work pattern to inject all repositories into a single [class](ElectronicsStore.Data/ElectronicsStoreUnitOfWork.cs)
- [Controllers](ElectronicsStore.Api/Controllers) are used for handling requests from the client.
- [Services](ElectronicsStore.Services) handle all necessary logic like mapping DTO models using AutoMapper, calling repositories or getting the authorized user info.
- [Repositories](ElectronicsStore.Data/Repositories) handle database operations, each repository corresponds to its own table from database.
- Project uses FluentValidation library
- For UI styling I used Bootstrap and react-bootstrap

# Database
![Database Schema](dbSchema.jpg)
