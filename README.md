# Asset Management System

## About

A small REST API for managing IT assets (laptops, monitors, smartphones) and assigning them to employees.
Built to learn **.NET** and its ecosystem, inspired by real-world IT asset management tools such as Snipe-IT.

## Tech Stack

- **.NET 10** / **C#**
- **ASP.NET Core Web API** (controller-based)
- **Entity Framework Core** with **SQLite**
- OpenAPI document generation with interactive API reference via **Scalar**

## Architecture

The API follows a classic layered structure:

```
Controllers  →  Services  →  AppDbContext (EF Core)  →  SQLite
   (HTTP)       (business      (data access)
                 rules)
```

- **Controllers** handle routing, model binding and HTTP status codes. They only work with DTOs.
- **Services** contain the business logic and are accessed through interfaces (`IAssetService`, `IEmployeeService`), registered in the DI container as scoped services.
- **DTOs** decouple the public API from the database model. They prevent over-posting and circular references between entities.

## Data Model

One-to-many (1:N) relationship between employees and assets:

- One employee can be assigned multiple assets.
- Each asset is assigned to at most one employee.

An asset has a type (`Laptop`, `Monitor`, `Smartphone`, `Other`) and a status (`Available`, `Assigned`, `Defective`).

### Business rules

- New assets are always created as `Available` and unassigned.
- An asset can only be assigned if it is `Available` and the employee exists.
- Only `Assigned` assets can be returned.
- Status and assignment can never be changed through a regular update (`PUT`), only through assign/return or by deleting the employee. This keeps status and owner consistent.
- Deleting an employee releases all of their assets (status is set back to `Available`).

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### Run the API

```bash
git clone https://github.com/AK-05-K/asset_management_dotnet.git
cd asset_management_dotnet/src/AssetManagement.Api
dotnet ef database update
dotnet run
```

The API is then available at the URL shown in the terminal (e.g. `http://localhost:5101`).

## API Documentation

In development, the API provides an interactive reference powered by [Scalar](https://scalar.com):

- **Scalar UI:** `http://localhost:5101/scalar`
- **OpenAPI document:** `http://localhost:5101/openapi/v1.json`

Scalar reads the OpenAPI document that ASP.NET Core generates from the controllers. It lists all endpoints with their parameters, request bodies and possible responses, and allows sending requests directly from the browser. Because the document is generated from the code, the documentation always stays in sync with the implementation.

Both are only available in the `Development` environment.

## API Endpoints

### Assets

| Method   | Endpoint                       | Description                                   | Responses        |
|----------|--------------------------------|-----------------------------------------------|------------------|
| `GET`    | `/api/assets`                  | Get all assets, optionally filtered by status (`?status=Available`) | 200 |
| `GET`    | `/api/assets/{id}`             | Get a single asset                            | 200, 404         |
| `POST`   | `/api/assets`                  | Create a new asset                            | 201              |
| `PUT`    | `/api/assets/{id}`             | Update name, serial number and type           | 204, 404         |
| `DELETE` | `/api/assets/{id}`             | Delete an asset                               | 204, 404         |
| `POST`   | `/api/assets/{assetId}/assign` | Assign an asset to an employee                | 204, 404, 409    |
| `POST`   | `/api/assets/{assetId}/return` | Return an assigned asset                      | 204, 404, 409    |

### Employees

| Method   | Endpoint                     | Description                            | Responses |
|----------|------------------------------|----------------------------------------|-----------|
| `GET`    | `/api/employees`             | Get all employees                      | 200       |
| `GET`    | `/api/employees/{id}`        | Get a single employee                  | 200, 404  |
| `POST`   | `/api/employees`             | Create a new employee                  | 201       |
| `GET`    | `/api/employees/{id}/assets` | Get all assets assigned to an employee | 200, 404  |
| `DELETE` | `/api/employees/{id}`        | Delete an employee                     | 204, 404  |

### Example requests

Create an asset:
```json
POST /api/assets
{
  "name": "ThinkPad T14",
  "serialNumber": "PF3K8XQ2",
  "type": "Laptop"
}
```

Assign an asset:
```json
POST /api/assets/1/assign
{
  "employeeId": 1
}
```
