# 🚀 Blazor CRUD Application

Web application developed in **Blazor** to demonstrate full CRUD (Create, Read, Update, Delete) operations using a layered and well-structured architecture in .NET.

## 📌 About the Project

This project is an interactive application designed to showcase best practices for data management within a **Blazor** interface. It implements software design patterns using DTOs, Services, Interfaces, and Entity Framework Core Migrations to maintain clean code separation and maintainability.

### ✨ Key Features

* **Record Listing:** Clear data visualization using dynamic components.

* **Creation:** Validated forms for adding new records via Data Transfer Objects (DTOs) and Request models.

* **Editing:** Smooth workflow to update existing records.

* **Deletion:** Safe removal process with confirmation handling.

* **Layered Architecture:** Decoupled business logic through Interfaces, DTOs, and Service contracts.

## 🛠️ Technologies Used

* **Framework:** [.NET / Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor?utm_source=gemini)

* **Language:** C#

* **OR/M & Persistence:** Entity Framework Core & EF Core Migrations

* **Styling:** MudBlazor / Bootstrap / Static Web Assets (`wwwroot`)

## 📂 Project Architecture

The repository is structured with clear separation of concerns, decoupling UI components, business logic, data models, and database migrations:

```
blazor-crud/
├── wwwroot/           # Static web assets (CSS, JavaScript, Images)
├── Components/        # Blazor Razor components and UI views
├── Data/              # DbContext and Database connection setup
├── Domain/            # Core business entities and domain rules
├── Dtos/              # Data Transfer Objects for clean data mapping
├── Interface/         # Abstractions, contracts, and service interfaces
├── Migrations/        # Entity Framework Core database migration files
├── Request/           # Input request payloads and validation models
├── Service/           # Business logic and service implementations
└── Program.cs         # Application entry point & Dependency Injection (DI) configuration

```

## ⚙️ Running the Project Locally

### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download?utm_source=gemini) (Version 8.0 or higher recommended).

* IDE or editor: Visual Studio, Visual Studio Code, or JetBrains Rider.

### Step-by-Step Guide

1. **Clone the repository:**

   ```
   git clone https://github.com/LuisFernando-hub/blazor-crud.git
   
   ```

2. **Navigate to the project directory:**

   ```
   cd blazor-crud
   
   ```

3. **Restore dependencies:**

   ```
   dotnet restore
   
   ```

4. **Update/Apply Database Migrations:**

   ```
   dotnet ef database update
   
   ```

5. **Run the application:**

   ```
   dotnet run
   
   ```

6. **Open in browser:**
   Navigate to the local URL shown in your terminal (e.g., `https://localhost:7...` or `http://localhost:5...`).

## 🤝 Contributing

Contributions are always welcome! Follow these steps to contribute:

1. **Fork** the repository.

2. Create a **Branch** for your feature (`git checkout -b feature/MyNewFeature`).

3. **Commit** your changes (`git commit -m 'Add some feature'`).

4. **Push** to your remote branch (`git push origin feature/MyNewFeature`).

5. Open a **Pull Request**.

## 📝 License

This project is licensed under the MIT License. For more details, see the [LICENSE](LICENSE) file in the repository.