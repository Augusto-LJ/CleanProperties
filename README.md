# 🏘️ CleanProperties

A property management application built to demonstrate **clean architecture principles**, **SOLID design**, and **production-ready practices** in a .NET environment.

This project showcases a real-world real estate management system, focusing on proper separation of concerns, testability, and scalable backend architecture.

---

### Architectural Principles Applied:

- Clean Architecture
- Separation of Concerns (SoC)
- Dependency Injection
- SOLID Principles
- Query/Command Pattern (CQRS-like)

---

## ⚙️ Technical Decisions

### 🔹 CQRS-Style Pattern with MediatR
- Implemented using **MediatR** for request handling
- Clear separation between Queries (read operations) and Commands (write operations)
- Enables middleware pipelines for cross-cutting concerns (validation, caching, etc.)

### 🔹 Intelligent Caching Strategy
- **Distributed caching** with Redis integration
- Cache invalidation through `ICacheable` interface
- Configurable sliding expiration for optimal memory usage
- Bypass cache when needed for fresh data retrieval

### 🔹 Data Mapping & Validation
- **Mapster** for efficient object mapping between entities and DTOs
- **FluentValidation** for declarative, testable validation rules
- Type-safe mappers with expression-based configuration

### 🔹 Database Design
- **Entity Framework Core 8** for data access
- **SQL Server** for reliable, scalable persistence
- Fluent API configuration for flexible schema management
- Navigation properties for relationship management between Agents and Properties

### 🔹 Comprehensive Testing
- **Unit tests** with AAA (Arrange-Act-Assert) pattern
- **Moq** for service mocking
- **Moq.EntityFrameworkCore** for DbSet mocking
- **FluentAssertions** for readable test assertions

### 🔹 Clean Response Wrapper Pattern
- Standardized API responses with `ResponseWrapper<T>`
- Consistent error handling across all endpoints
- Success/failure states with meaningful messages

---

## 🚀 Technologies Used

**Backend:**
- ASP.NET Core 8
- C#
- Entity Framework Core 8.0.25
- SQL Server
- MediatR 14.1.0
- FluentValidation 12.1.1
- Mapster 7.4.0
- StackExchange.Redis (Caching)

**Testing:**
- xUnit
- Moq
- FluentAssertions
- Moq.EntityFrameworkCore

**Architecture & Tools:**
- Clean Architecture
- Dependency Injection
- Git & GitHub
- Visual Studio 2022

---

## 🎯 Features

### 🏠 Property Management
- Create, read, update, and delete property listings
- Detailed property information (descriptions, pricing, listing dates)
- Relationship between properties and managing agents
- Property search and filtering capabilities

### 👤 Agent Management
- Manage real estate agents
- Track agent contact information (phone, email)
- View agent's property portfolio
- Agent performance and listing counts

### ⚡ Performance & Caching
- Redis-backed distributed caching
- Optimized query performance with Entity Framework
- Configurable cache expiration strategies
- Cache bypass options for critical operations

### ✅ Validation & Error Handling
- Fluent validation rules for all inputs
- Consistent error responses
- Meaningful validation messages
- Type-safe error handling

### 🔄 Clean Request/Response Pattern
- Standardized API response wrapper
- Consistent HTTP response structures
- Clear success/failure indication
- Detailed error context

---

## 🔄 How It Works

### Example: Get Agent by ID with Caching

1. **Client Request** → HTTP GET `/api/agents/1`
2. **MediatR Handler** → Receives `GetAgentByIdQuery` request and triggers caching pipeline
3. **Caching Pipeline** → Checks Redis cache, returns cached result if available
4. **Service Layer** → Queries database via Entity Framework, includes related property listings
5. **Mapping** → Converts `Agent` entity to `AgentResponse` DTO using Mapster
6. **Response Wrapper** → Wraps result in `ResponseWrapper<AgentResponse>`
7. **Client Response** → 200 OK with agent data

---

## 💻 Local Development Setup

### Prerequisites
- .NET 8 SDK
- SQL Server (Express or Developer Edition)
- Visual Studio 2022 (or VS Code with C# extensions)
- Git

### Installation Steps

```bash
1. Clone the repository
git clone https://github.com/Augusto-LJ/CleanProperties.git
cd CleanProperties

2. Configure database connection
Update `appsettings.json` with your SQL Server connection string, then run migrations:
dotnet ef database update --project Infrastructure

3. Restore dependencies
dotnet restore

4. Build the solution
dotnet build
```

### Access the Services
- **API**: http://localhost:5000
- **Swagger/OpenAPI**: http://localhost:5000/swagger

---

## 🔜 Future Enhancements

- **Integration and Unit Tests** → To improve quality
- **API Documentation** → Comprehensive OpenAPI/Swagger specs
- **Audit Logging** → Track changes to properties and agents
- **CI/CD Pipeline** → Automated testing and deployment
- **Docker Support** → Containerization for consistent environments
- **API Versioning** → Multiple API versions for backward compatibility
- **Rate Limiting** → Protect API from abuse
- **Authentication & Authorization** → Role-based access control
- **Caching** → Rewrite in cache when an agent ou a property is updated

---

## 📚 Learning Resources

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [FluentValidation](https://fluentvalidation.net/)
- [Mapster](https://github.com/MapsterMapper/Mapster)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)

---

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/YourFeature`)
3. Commit changes with clear messages
4. Push to the branch (`git push origin feature/YourFeature`)
5. Open a Pull Request with a description of your changes

---

## 📝 License

This project is open source and available under the MIT License.

---

**⭐ If you found this helpful, please consider starring the repository!**
