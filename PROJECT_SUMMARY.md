# Smart Phone API - Project Summary

## 📱 Project Overview
A RESTful Web API built with ASP.NET Core 8.0 for managing smartphone inventory, manufacturers, and specifications. The API provides comprehensive CRUD operations with advanced search capabilities and duplicate prevention mechanisms.

---

## 🛠️ Technology Stack

### Backend Framework
- **ASP.NET Core 8.0** - Web API framework
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Database management system

### Authentication & Security
- **ASP.NET Core Identity** - User authentication and authorization
- **JWT Token-based Authentication** - Secure API access
- **Role-based Authorization** - Protected endpoints

### API Documentation
- **Swagger/OpenAPI** - Interactive API documentation

---

## 🏗️ Architecture

### Design Pattern: Repository Pattern
The application follows a **3-layer architecture**:

```
┌─────────────────────────────────────┐
│      Controllers (API Layer)       │
│  - SmartPhoneController            │
│  - ManufacturerController          │
│  - SpecificationController         │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│    Repositories (Data Access)        │
│  - ISmartPhoneRepository            │
│  - IManufacturerRepository          │
│  - ISpecificationRepository         │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│    Database Context (EF Core)        │
│  - SmartPhoneDbContext               │
└───────────────────────────────────────┘
```

### Dependency Injection
- All repositories registered as **Scoped** services
- Loose coupling through interface-based design

---

## 📊 Data Models

### 1. SmartPhone
- **Id** (Primary Key)
- **Name** (Required, Max 20 chars)
- **Description** (Max 100 chars)
- **Price** (Required, Range: 1000-100000)
- **MId** (Foreign Key → Manufacturer)
- **SpecId** (Foreign Key → SmartPhoneSpec)
- **Navigation Properties**: Manufacturer, Specification

### 2. Manufacturer
- **MId** (Primary Key)
- **Name** (Required, Max 20 chars)
- **Navigation Property**: SmartPhones (Collection)

### 3. SmartPhoneSpec
- **SpecId** (Primary Key)
- **Processor** (Required, Max 100 chars)
- **RAM** (Required)
- **Storage** (Required)
- **OS** (Required)
- **Navigation Property**: SmartPhones (Collection)

### 4. SearchQuery
- **QueryId** (Primary Key)
- **Processor** (Optional)
- **RAM** (Optional)
- **Storage** (Optional)
- **OS** (Optional)

---

## 🔌 API Endpoints

### SmartPhone Controller (`/api/SmartPhone`)

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/SmartPhone` | No | Get all smartphones with related data |
| GET | `/api/SmartPhone/{id}` | No | Get smartphone by ID |
| POST | `/api/SmartPhone` | Yes | Create new smartphone (duplicate check) |
| PUT | `/api/SmartPhone/{id}` | Yes | Update smartphone |
| DELETE | `/api/SmartPhone/{id}` | Yes | Delete smartphone |
| POST | `/api/SmartPhone/Manufacturer` | No | Search by manufacturer name |
| POST | `/api/SmartPhone/Specification` | No | Advanced search by specifications |

### Manufacturer Controller (`/api/Manufacturer`)

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/Manufacturer` | No | Get all manufacturers |
| GET | `/api/Manufacturer/{id}` | No | Get manufacturer by ID |
| POST | `/api/Manufacturer` | Yes | Create manufacturer (duplicate check) |
| PUT | `/api/Manufacturer/{id}` | Yes | Update manufacturer |
| DELETE | `/api/Manufacturer/{id}` | Yes | Delete manufacturer |

### Specification Controller (`/api/Specification`)

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/Specification` | No | Get all specifications |
| GET | `/api/Specification/{id}` | No | Get specification by ID |
| POST | `/api/Specification` | Yes | Create specification (duplicate check) |
| PUT | `/api/Specification/{id}` | Yes | Update specification |
| DELETE | `/api/Specification/{id}` | Yes | Delete specification |

---

## ✨ Key Features

### 1. **Duplicate Prevention**
- **SmartPhone**: Prevents duplicate entries by name (case-insensitive)
- **Manufacturer**: Prevents duplicate entries by name (case-insensitive)
- **Specification**: Prevents duplicate entries by complete spec combination
- Returns **HTTP 409 Conflict** with descriptive error messages

### 2. **Advanced Search Capabilities**
- **Search by Manufacturer**: Filter smartphones by manufacturer name (partial match, case-insensitive)
- **Search by Specifications**: Multi-criteria search with optional filters:
  - Processor
  - RAM
  - Storage
  - Operating System
- All filters are optional and combined with AND logic

### 3. **Eager Loading**
- Uses Entity Framework's `Include()` for efficient data loading
- Related entities (Manufacturer, Specification) loaded in single query
- Prevents N+1 query problems

### 4. **Data Validation**
- Model validation using Data Annotations:
  - Required fields
  - String length constraints
  - Range validations
  - Custom error messages

### 5. **Response Formatting**
- Custom JSON serialization
- Null values ignored in responses
- Structured response objects with success messages

### 6. **Error Handling**
- Proper HTTP status codes:
  - `200 OK` - Success
  - `404 Not Found` - Resource not found
  - `409 Conflict` - Duplicate entry
  - `400 Bad Request` - Invalid input
- Descriptive error messages

---

## 🔒 Security Features

### Authentication
- **ASP.NET Core Identity** integration
- JWT token-based authentication
- Identity API endpoints for user management

### Authorization
- Protected endpoints require `[Authorize]` attribute:
  - POST (Create) operations
  - PUT (Update) operations
  - DELETE operations
- Public endpoints:
  - GET (Read) operations
  - Search operations

---

## 🗄️ Database Design

### Relationships
```
Manufacturer (1) ────< (Many) SmartPhone
SmartPhoneSpec (1) ────< (Many) SmartPhone
```

### Tables
1. **TblManufacturer** - Stores manufacturer information
2. **TblSpecification** - Stores phone specifications
3. **TblSmartPhone** - Main table with foreign keys to Manufacturer and Specification
4. **TblSearchQuery** - Stores search query history
5. **AspNetUsers** - Identity user management (inherited from IdentityDbContext)

### Entity Framework Migrations
- Code-first approach
- Multiple migrations for schema evolution
- Foreign key relationships properly configured

---

## 📈 API Response Examples

### Success Response
```json
{
  "smartPhoneAdded": { ... },
  "msg": "Successfully created Smart Phone"
}
```

### Duplicate Error Response
```json
{
  "error": "A Smart Phone with the name 'iPhone 15' already exists."
}
```

### Search Response
```json
{
  "result": [
    {
      "SmartPhoneId": 1,
      "SmartPhoneName": "iPhone 15",
      "Price": 79999,
      "Manufacturer": "Apple",
      "Processor": "A17 Pro",
      "RAM": "8GB",
      "Storage": "256GB",
      "Operating_System": "iOS 17"
    }
  ]
}
```

---

## 🎯 Best Practices Implemented

1. **Repository Pattern** - Separation of concerns, testability
2. **Dependency Injection** - Loose coupling, maintainability
3. **Interface-based Design** - Easy to mock and test
4. **Data Validation** - Input validation at model level
5. **Error Handling** - Consistent error responses
6. **Eager Loading** - Optimized database queries
7. **Case-insensitive Comparisons** - Better user experience
8. **Null Safety** - Null checks for navigation properties

---

## 📝 Code Statistics

- **Controllers**: 3 (SmartPhone, Manufacturer, Specification)
- **Repositories**: 3 (with interfaces)
- **Models**: 4 (SmartPhone, Manufacturer, SmartPhoneSpec, SearchQuery)
- **Total API Endpoints**: 17
- **Protected Endpoints**: 9
- **Public Endpoints**: 8

---

## 🚀 Future Enhancements (Potential)

1. Pagination for large result sets
2. Caching for frequently accessed data
3. Logging and monitoring
4. Unit and integration tests
5. API versioning
6. Rate limiting
7. File upload for phone images
8. Advanced filtering and sorting

---

## 📚 Technologies & Libraries

- **Microsoft.AspNetCore.Identity** - Authentication
- **Microsoft.EntityFrameworkCore** - ORM
- **Microsoft.EntityFrameworkCore.SqlServer** - SQL Server provider
- **Swashbuckle.AspNetCore** - Swagger/OpenAPI

---

## 🎓 Learning Outcomes

This project demonstrates:
- RESTful API design principles
- Entity Framework Core best practices
- Repository pattern implementation
- Authentication and authorization
- Data validation and error handling
- Advanced querying with LINQ
- Dependency injection
- Clean code architecture

---

**Project Name**: Preethu.Phone.API  
**Framework**: ASP.NET Core 8.0  
**Database**: SQL Server  
**Architecture**: Repository Pattern with 3-Layer Design

