git clone <url-del-repositorio>
cd PersonasApi
# PersonasAPI - .NET 8 Backend

REST API built with .NET 8 and integrated with Supabase for managing people and user authentication.

## Table of Contents

- [Description](#description)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Project Structure](#project-structure)
- [Features](#features)

---

## Description

PersonasAPI is a RESTful API that provides full management of people records and user authentication. It uses Supabase (PostgreSQL) as the cloud database and follows clean architecture principles and separation of concerns.

### Main Features

- Full CRUD for people
- Authentication (Register / Login)
- Password hashing with BCrypt
- Data validation using Data Annotations
- Integration with Supabase (PostgreSQL)
- Automatic API documentation with Swagger/OpenAPI
- Error handling and logging

---

## Technologies

### Framework and Runtime
- **.NET 8.0** - primary runtime
- **ASP.NET Core** - web framework

### NuGet Packages
| Package | Version | Purpose |
|---------|---------|---------|
| `Supabase` | 1.1.1 | Supabase client |
| `BCrypt.Net-Next` | 4.0.3 | Password hashing |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | 8.0.11 | PostgreSQL provider |
| `Swashbuckle.AspNetCore` | 10.1.0 | Swagger documentation |
| `Microsoft.AspNetCore.OpenApi` | 10.0.2 | OpenAPI support |

### Database
- **Supabase (PostgreSQL)** - cloud-hosted database

---

## Architecture

The project follows a layered architecture with clear separation of concerns.

![Layers](./images/capas.png)

### Applied Principles
- Separation of Concerns
- Dependency Injection
- DTOs for API boundaries
- Repository Pattern for data access
- Clean, maintainable code

---

## Prerequisites

Make sure you have installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or [VS Code](https://code.visualstudio.com/)
- Supabase account
- Git (optional)

---

## Installation

### 1. Clone the repository

```bash
git clone <repository-url>
cd PersonasApi
```

## Project Structure

PersonasApi/
│
├── Configuration/              # Configuration
│   └── SupabaseSettings.cs     # Supabase settings
│
├── Controllers/                # API controllers
│   ├── AuthController.cs       # Authentication endpoints
│   └── PersonasController.cs   # People endpoints
│
├── Data/                       # Data layer
│   ├── SupabaseContext.cs      # Supabase client wrapper
│   └── Repositories/           # Repositories
│       ├── PersonaRepository.cs
│       └── UserRepository.cs
│
├── Models/                     # Domain models
│   ├── Persona.cs              # Person entity
│   ├── User.cs                 # User entity
│   └── DTOs/                   # Data Transfer Objects
│       ├── PersonaDTO.cs
│       ├── CreatePersonaDTO.cs
│       ├── UpdatePersonaDTO.cs
│       ├── LoginDTO.cs
│       ├── RegisterDTO.cs
│       └── UserDTO.cs
│
├── Services/                   # Business logic
│   ├── PersonaService.cs       # People service
│   └── AuthService.cs          # Authentication service
│
├── Properties/
│   └── launchSettings.json     # Launch settings
│
├── Program.cs                  # Application entry point
├── appsettings.json            # Application configuration
└── PersonasApi.csproj          # Project file

## Features

### Security
- Passwords hashed with BCrypt
- Input validation on all endpoints
- Duplicate prevention (email, identification number, username)
- CORS configured for development

### Validation
- Data Annotations on DTOs
- Automatic model validation
- Business validations in services
- Clear error messages

### Database
- Computed columns (full name, full identification)
- Unique constraints
- Foreign keys with cascade delete
- Automatic timestamps

### Development
- Hot reload enabled
- Logging configured
- Environment-based configuration
- Swagger/OpenAPI 3.0
