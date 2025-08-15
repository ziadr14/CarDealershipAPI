# Car Dealership API

This is a multi-layer ASP.NET Core Web API project for managing a car dealership system.  
It follows a clean architecture using **Repository Pattern**, **Unit of Work**, and **AutoMapper** to separate concerns between layers.  

The system supports managing:
- Cars and Brands
- Customers
- Orders
- Employees
- Branches

It also includes:
- Role-based Authentication and Authorization
- DTOs for clean data transfer
- EF Core for database operations
- AutoMapper for object mapping

The project is divided into three main layers:
1. **API Layer** - Handles HTTP requests and responses (Controllers).
2. **Business Logic Layer (BLL)** - Contains service logic and business rules.
3. **Data Access Layer (DAL)** - Manages database models, context, and repositories.

---

## 🛠 Technologies Used

- **ASP.NET Core Web API (.NET 8)**
- **Entity Framework Core** for ORM
- **SQL Server** as the database
- **AutoMapper** for object mapping
- **Repository Pattern** & **Unit of Work**
- **JWT Authentication** & **Role-based Authorization**
- **Swagger / OpenAPI** for API documentation

---

## 🔑 Key Features

1. **User Authentication & Authorization**
   - JWT-based authentication
   - Role-based access control (Admin, Seller, Buyer)
   - Secure password hashing using `PasswordHasher<T>`

2. **Cars Management**
   - CRUD operations for cars
   - Brand relationship (FK constraint)
   - Image upload support

3. **Sales Management**
   - Linked to Cars & Customers
   - Foreign key constraints to maintain data integrity

4. **Repository & Unit of Work**
   - Clean separation between data access and business logic
   - Centralized transaction handling

5. **Error Handling**
   - Foreign key constraint validation
   - Business rules validation before deletion or updates

---

## 📄 API Endpoints Overview

### Authentication
| Method | Endpoint              | Description            | Role Required |
|--------|----------------------|------------------------|---------------|
| POST   | `/api/auth/register` | Register new user       | Public        |
| POST   | `/api/auth/login`    | Login and get JWT token | Public        |

### Cars
| Method | Endpoint           | Description            | Role Required |
|--------|-------------------|------------------------|---------------|
| GET    | `/api/cars`       | Get all cars           | Any           |
| GET    | `/api/cars/{id}`  | Get car by ID          | Any           |
| POST   | `/api/cars`       | Add a new car          | Admin, Seller |
| PUT    | `/api/cars/{id}`  | Update a car           | Admin, Seller |
| DELETE | `/api/cars/{id}`  | Delete a car (if no sales) | Admin |


### Brands
| Method | Endpoint | Description | Role Required |
|--------|----------|-------------|---------------|
| GET    | `/api/brands` | Get all brands | Any |
| GET    | `/api/brands/{id}` | Get brand by ID | Any |
| POST   | `/api/brands` | Add a new brand | Admin |
| PUT    | `/api/brands/{id}` | Update a brand | Admin |
| DELETE | `/api/brands/{id}` | Delete a brand | Admin |

### Customers
| Method | Endpoint | Description | Role Required |
|--------|----------|-------------|---------------|
| GET    | `/api/customers` | Get all customers | Admin, Seller |
| GET    | `/api/customers/{id}` | Get customer by ID | Admin, Seller |
| POST   | `/api/customers` | Add a new customer | Admin, Seller |
| PUT    | `/api/customers/{id}` | Update customer info | Admin, Seller |
| DELETE | `/api/customers/{id}` | Delete a customer | Admin |

### Employees
| Method | Endpoint | Description | Role Required |
|--------|----------|-------------|---------------|
| GET    | `/api/employees` | Get all employees | Admin |
| GET    | `/api/employees/{id}` | Get employee by ID | Admin |
| POST   | `/api/employees` | Add a new employee | Admin |
| PUT    | `/api/employees/{id}` | Update an employee | Admin |
| DELETE | `/api/employees/{id}` | Delete an employee | Admin |

### Orders
| Method | Endpoint | Description | Role Required |
|--------|----------|-------------|---------------|
| GET    | `/api/orders` | Get all orders | Admin, Seller |
| GET    | `/api/orders/{id}` | Get order by ID | Admin, Seller |
| POST   | `/api/orders` | Place a new order | Buyer, Seller |
| PUT    | `/api/orders/{id}` | Update an order | Admin, Seller |
| DELETE | `/api/orders/{id}` | Cancel an order | Admin, Seller |

---
---

## ⚙️ How to Run

1. **Clone the repository**
   ```bash
   git clone https://github.com/ziadr14/CarDealershipAPI.git

## Project Structure


CarDealershipAPI/
│
├── CarDealershipAPI/ # API Layer (Controllers, Startup, Program.cs)
│ ├── Controllers/ # API endpoints
│ ├── DTOs/ # Data Transfer Objects
│ ├── Properties/
│ ├── obj/
│ └── Program.cs
│
├── CarDealershipBLL/ # Business Logic Layer
│ ├── Interfaces/ # Service interfaces
│ ├── Services/ # Service implementations
│ ├── DTOs/ # DTO definitions
│ └── Mappings/ # AutoMapper profiles
│
├── CarDealershipDAL/ # Data Access Layer
│ ├── Models/ # Entity models
│ ├── Interfaces/ # Repository interfaces
│ ├── Repositories/ # Repository implementations
│ ├── Data/ # DbContext
│ └── Migrations/ # EF Core migrations
│
└── CarDealershipAPI.sln # Solution file
