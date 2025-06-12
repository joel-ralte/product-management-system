# Product Management System (PMS)

This solution is a modular, testable Product Management System built with ASP.NET Core (.NET 8), Entity Framework Core, and xUnit for testing. 
It demonstrates clean architecture principles, separation of concerns, and code-first database management with EF Core migrations.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQLite](https://www.sqlite.org/download.html) (if using SQLite provider)
- (Optional) [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd <repository-folder>
```

### 2. Database Setup

#### **Option A: Using Visual Studio 2022**

If you are using Visual Studio 2022, simply build and run the **PMS.WebAPI** project.   Visual Studio will automatically restore NuGet packages, build the solution, and launch the Web API.  

The database will be created and seeded with initial data on first run—no manual steps required.

#### **Option B: Using Command Line (.NET CLI)**

If you are not using Visual Studio, you can set up the database using the .NET CLI:

1. **Restore NuGet packages:**

    ```bash
    dotnet restore
    ```
    
2. **Build the solution:**
    
    ```bash
    dotnet build
    ```

3. **Apply EF Core migrations and create the database:**
    
    ```bash
    dotnet ef database update --project Source/PMS.Repository --startup-project Source/PMS.WebAPI
    ```

4. **Run the Web API:**

    ```bash
    dotnet run --project Source/PMS.WebAPI
    ```

The application will seed the database with initial data on startup.

---

**Note:**  
- Ensure you have the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed.
- If using SQLite, no additional setup is required; the database file will be created automatically.
- If you encounter issues with EF Core tools, install them globally with:
    
    ```bash
    dotnet tool install --global dotnet-ef
    ```
    
### 3. Accessing API Endpoints with Swagger

After running the application, you can explore and test all API endpoints using the integrated Swagger UI.
For this application, Swagger will be available at:

https://localhost:5001/swagger

or

https://localhost:5000/swagger

Swagger provides an interactive interface to view, test, and document the API endpoints without needing external tools.

---

### Entity Validation, Error Handling, and Auto-Generated Fields

When adding or updating products, you only need to provide the relevant business data (e.g., Name, Description, Price, Stock). 

The system takes care of generating the Id, as well as setting the creation and update timestamps, and handles validation and error reporting to maintain data integrity and provide a robust API experience.

---
## Solution Structure

This solution is a modular monolith implemented in repository pattern with the following projects:

- **Source/PMS.WebAPI**  
  ASP.NET Core Web API project exposing endpoints for product management.

- **Source/PMS.Business**  
  Contains business logic, service interfaces, and unit of work implementation.

- **Source/PMS.Repository**  
  Data access layer using Entity Framework Core and database context.

- **Source/PMS.DTO**  
  Data Transfer Objects for API and service communication.

- **Source/PMS.Common**  
  Shared/common code and utilities.

- **Tests/PMS.Business.Tests**  
  Unit tests for business logic using xUnit and Moq.

- **Tests/PMS.Repository.Tests**  
  Unit tests for repository logic using EF Core InMemory provider.

- **Tests/PMS.WebAPI.Tests**
  Unit tests for controller logic using xUnit.




