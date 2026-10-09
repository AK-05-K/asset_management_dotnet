# Asset management system

## About

This project is a small asset management system developed to learn and explore **.NET** and its ecosystem.

## Features

- **Employee Management:** Create and manage employees.
- **Device Management:** Create and manage devices.
- **Device Assignment:** Assign devices to employees and manage their assignments.

## Data Relationships

The system uses a **one-to-many (1:N) relationship** between employees and devices:

- One employee can be assigned multiple devices.
- Each device can be assigned to at most one employee.

## API Endpoints

Endpoints will be documented here as the project develops.

## API Endpoints

| Method | Endpoint | Description |

| GET | `/api/assets` | Get all assets, optionally filtered by status (e.g., `?status=Available`) |
| GET | `/api/assets/{id}` | Get a single asset |
| POST | `/api/assets` | Create a new asset |
| PUT | `/api/assets/{id}` | Update an existing asset |
| DELETE | `/api/assets/{id}` | Delete an asset |
| POST | `/api/assets/{id}/assign` | Assign an asset to an employee |
| POST | `/api/assets/{id}/return` | Return an assigned asset |
| GET | `/api/employees` | Get all employees |
| POST | `/api/employees` | Create a new employee |
| GET | `/api/employees/{id}/assets` | Get all assets assigned to a specific employee |


