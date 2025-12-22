                           *Hahn Software Morocco - End of studies internship 2026*

📌 Project Management System

📘 Project Overview

This is a high-performance, containerized Task Management application designed to demonstrate enterprise-grade software development practices.

Beyond basic CRUD functionality, this project serves as a showcase for:

Clean Architecture: Enforcing a strict separation of concerns.

SOLID Principles: Ensuring long-term maintainability and testability.

Modern DevOps: A fully containerized workflow for seamless environment setup.

The system is built as a realistic backend-frontend ecosystem where scalability and code organization are treated as core requirements from day one.

                                                *🛠️ Tech Stack*

Backend

Framework: .NET 10 (ASP.NET Core Web API)

ORM: Entity Framework Core

Auth: JWT Authentication

    *Testing: xUnit*

Containerization: Docker

Frontend

Framework: React with TypeScript

Routing: React Router

API Client: Axios

Containerization: Docker

Infrastructure

Database: MySQL 8.0

Orchestration: Docker Compose

    *🚀 Getting Started*

The project is fully Dockerized, allowing you to spin up the entire stack (Backend, Frontend, and Database) with a single command.

🔹 Prerequisites

Docker Desktop

Docker Compose

▶️ Run with Docker (Recommended)

Clone the repository.

From the root directory, run:

    *docker compose up --build*


Access the application:

Frontend: http://localhost:3000

Backend API: http://localhost:7033

    *▶️ Local Development (Optional)*

If you prefer running the services outside of Docker:

Backend:

      *cd BackendProjectManagement
      dotnet restore
      dotnet run*


Frontend:

      *cd frontend
       npm install
       npm run dev*


    *🗄️ Database Configuration*

MySQL runs inside a dedicated Docker container. The schema is automatically migrated and seeded on startup.

Default Credentials (Dev Environment):

Database: projecttasksdb

Username: root

Password: souhil

I konw that Database credentials are hardcoded for development and testing convenience only. For production environments, use Environment Variables or Secrets Management.

    *🧱 Architecture & Design*

This project implements Clean Architecture inspired by Domain-Driven Design (DDD) to ensure the business logic remains independent of frameworks and UI.

     *🗂️ Project Structure*

    
     BackendProjectManagement/
                            
                            ├── API/             # Controllers, Middlewares, Program.cs
                            
                            ├── Application/     # Business Logic, DTOs, Interfaces, Services
                            
                            ├── Domain/          # Core Business Entities (No Dependencies)
                            
                            └── Infrastructure/  # EF Core, Repositories, JWT Implementation


                            

    *🔄 Dependency Flow*

All dependencies flow inward:
API → Application → Domain
Infrastructure → Application → Domain

    *🧠 Applied SOLID Principles*

S (Single Responsibility): Specific separation between Controllers, Services, and Repositories.

O (Open/Closed): Logic is extensible through interface abstractions.

L (Liskov Substitution): Seamless interchangeability between interfaces and implementations.

I (Interface Segregation): Small, focused contracts to avoid "fat" interfaces.

D (Dependency Inversion): 

High-level modules depend on abstractions, not concrete implementations.


    *📌 DDD Implementation Status*


**Concept        Status

Entities       ✅ Implemented

Repositories   ✅ Implemented

Services       ✅ Implemented

Domain Events  ❌ Planned

Value Objects  ❌ Planned

Aggregates     ❌ Planned**

    *🧪 Quality Assurance*

Unit tests are implemented using xUnit to validate core application logic, specifically focusing on:

Business Logic Services

Validation Rules

Data Transformation

To run tests locally:

    *dotnet test*


The testing strategy prioritizes code reliability and critical paths over 100% vanity coverage.

                                       *📝 Final Thoughts*

*Every architectural choice was made to prioritize clarity and extensibility. This system is built to be easily understood by developers while providing the robustness required for enterprise evolution.*
