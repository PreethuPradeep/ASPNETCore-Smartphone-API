# ASP.NET Core Smartphone API

This project is a secure and robust Smartphone Management System built with ASP.NET Core Web API, Entity Framework Core (Code-First), and ASP.NET Identity for authentication. It serves as a comprehensive case study to demonstrate a maintainable, enterprise-ready RESTful API.

## Core Features

* **Secure Authentication & Authorization:** Uses ASP.NET Identity Framework for user registration (`/register`), login (`/login`), and token-based (JWT) security. All API endpoints are secured and require authentication.
* **Full CRUD Operations:** Provides `POST`, `GET`, `PUT`, and `DELETE` endpoints for managing Smartphones, Manufacturers, and Specifications.
* **Search Functionality:** Includes dedicated endpoints to search for smartphones by manufacturer name and by specific technical specifications.
* **Database Code-First:** Uses Entity Framework Core's Code-First approach, allowing the database schema to be generated and managed directly from C# model classes.

## Technology Stack

* **.NET 8 / ASP.NET Core Web API**
* **Entity Framework Core 8**
* **ASP.NET Core Identity** (for token-based authentication)
* **Microsoft SQL Server**
* **Swagger / OpenAPI** (for API documentation and testing)
* **Repository Pattern** (for decoupled data access)

## Getting Started

Follow these instructions to get the project up and running on your local machine.

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (Express, Developer, or LocalDB)
* A tool for API testing (e.g., [Postman](https://www.postman.com/))

### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/your-username/ASPNETCore-Smartphone-API.git](https://github.com/your-username/ASPNETCore-Smartphone-API.git)
    ```

2.  **Navigate to the project directory:**
    ```bash
    cd ASPNETCore-Smartphone-API/Preethu.Phone.API
    ```

3.  **Configure your database connection:**
    * Open the `Preethu.Phone.API/appsettings.json` file.
    * Update the `Default` connection string with your local SQL Server details.
    * Example for SQL Server LocalDB:
        ```json
        "ConnectionStrings": {
          "Default": "Server=(localdb)\\mssqllocaldb;Database=SmartPhoneDb;Trusted_Connection=True;"
        }
        ```

4.  **Apply database migrations:**
    * Run the following command in your package manager console to create the database and its tables based on the models:
    ```bash
    Add-Migration Filename
    update-database
    ```

5.  **Run the application:**

6.  **Access the API:**
    * The API will be running on the port specified in your `Properties/launchSettings.json` file (e.g., `http://localhost:5000`).
    * You can access the **Swagger UI** for testing and documentation by navigating to `http://localhost:5000/swagger` in your browser or use **postman**

## API Endpoints

All endpoints (except `/register` and `/login`) require a Bearer Token in the Authorization header.

### Authentication (Identity)

* `POST /register` - Register a new user.
* `POST /login` - Log in and receive a JWT.

### Smartphone (`/api/SmartPhone`)

* `GET /` - Get all smartphones.
* `GET /{id}` - Get a single smartphone by its ID.
* `POST /` - Add a new smartphone.
* `PUT /{id}` - Update an existing smartphone.
* `DELETE /{id}` - Delete a smartphone.
* `POST /Manufacturer` - Search for smartphones by manufacturer name.
* `POST /Specification` - Search for smartphones by specifications.

### Manufacturer (`/api/Manufacturer`)

* `GET /` - Get all manufacturers.
* `GET /{id}` - Get a single manufacturer by its ID.
* `POST /` - Add a new manufacturer.
* `PUT /{id}` - Update an existing manufacturer.
* `DELETE /{id}` - Delete a manufacturer.

### Specification (`/api/Specification`)

* `GET /` - Get all specifications.
* `GET /{id}` - Get a single specification by its ID.
* `POST /` - Add a new specification.
* `PUT /{id}` - Update an existing specification.
* `DELETE /{id}` - Delete a specification.

## Project Architecture

* **Repository Pattern:** The project uses the repository pattern to decouple the data access logic from the controllers. Each entity has its own repository interface (e.g., `ISmartPhoneRepository`) and implementation (e.g., `SmartPhoneRepository`).
* **Dependency Injection (DI):** Services like the `SmartPhoneDbContext` and the repositories are registered with the DI container in `Program.cs` and injected into controllers, promoting loose coupling.
* **Entity Relationships:** The models are configured to handle database relationships:
    * **One-to-Many:** One `Manufacturer` can have many `SmartPhone`s.
    * **One-to-One:** One `SmartPhone` has one `SmartPhoneSpec`.
