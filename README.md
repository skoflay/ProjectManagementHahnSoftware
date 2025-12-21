
📌 Project Management System 

📘 Project Overview

This project is a high-performance, containerized Task Management application built to demonstrate enterprise-grade software development practices.

The goal of this project goes beyond implementing basic CRUD features. It focuses on:

applying Clean Architecture to enforce a clear separation of concerns,

respecting SOLID principles to ensure maintainability and testability,

and adopting a modern DevOps workflow through containerization and automated environment setup.

The application is designed as a realistic backend–frontend system, where scalability, code organization, and long-term evolution are considered from the beginning rather than treated as afterthoughts.

This project reflects how a professional production-ready system would be structured, while still remaining pragmatic and suitable for a development and testing environment.


🛠️ Tools Used
Backend

.NET (ASP.NET Core Web API)

Entity Framework Core

JWT Authentication

xUnit (unit testing)

Docker

Frontend

React

TypeScript

React Router

Axios

Docker

Database

MySQL 8.0

🚀 How to Run the Project

The project is fully Dockerized, so backend, frontend, and database can be started together.

🔹 Prerequisites

You only need:

Docker

Docker Compose

(Optional for local dev: .NET SDK & Node.js)

▶️ How to Run Backend
Using Docker (recommended)

From the root folder:

docker compose up --build


Backend API will be available at:

http://localhost:7033

Running Backend Locally (optional)
cd BackendProjectManagement
dotnet restore
dotnet run

▶️ How to Run Frontend
Using Docker

Frontend starts automatically with Docker Compose:

http://localhost:3000

Running Frontend Locally (optional)
cd frontend
npm install
npm start

🗄️ Database Setup

MySQL runs inside a Docker container

Database and schema are created automatically

Default configuration (development only):

Database: projecttasksdb
Username: root
Password: souhil


⚠️ Important note
I’m aware that committing database credentials is not secure.
This was done intentionally for development and testing purposes only, not for production.

🧪 Testing

I added unit tests using xUnit to validate core application logic.

Tests are focused on:

Services

Business logic

Validation rules

Infrastructure and controllers are not over-tested to avoid unnecessary complexity

To run tests locally:

dotnet test


The goal here was code reliability, not 100% coverage.

🧱 Architecture & Design Choices

This project follows Clean Architecture principles, inspired by Domain-Driven Design (DDD).

I didn’t aim for a pure or theoretical DDD implementation.
Instead, I focused on clear separation of concerns, readability, and maintainability.

🗂️ Backend Structure
BackendProjectManagement/
├── Domain/
│   └── Entities/
│
├── Application/
│   ├── DTOs/
│   ├── Interfaces/
│   └── Services/
│
├── Infrastructure/
│   ├── Persistence/
│   │   ├── Data/
│   │   └── Repositories/
│   └── Services/
│
├── API/
│   └── Controllers/

🔹 Domain Layer

Contains the core business entities

No dependency on frameworks or infrastructure

Represents the business rules only

I initially planned to add Domain Events, but avoided heavy refactoring to keep the project stable.

🔹 Application Layer

Contains business logic

Defines interfaces for repositories and services

Uses DTOs to isolate domain models from external layers

🔹 Infrastructure Layer

Implements repository interfaces

Handles database access using EF Core

Manages JWT authentication and persistence concerns

🔹 API Layer

Exposes REST endpoints

Handles HTTP requests and responses

Delegates all logic to the Application layer

🔄 Dependency Flow

All dependencies flow inward, following Clean Architecture rules:

API → Application → Domain
Infrastructure → Application → Domain

🧠 SOLID Principles

Throughout the project, I tried to respect SOLID principles as much as possible:

S — Single Responsibility
Each class has a clear responsibility (controllers, services, repositories).

O — Open/Closed
Business logic is extensible through interfaces without modifying existing code.

L — Liskov Substitution
Interfaces and implementations are interchangeable without breaking behavior.

I — Interface Segregation
Interfaces are small and focused, avoiding “fat” contracts.

D — Dependency Inversion
High-level modules depend on abstractions, not concrete implementations.

This made the code easier to test and maintain.

📌 DDD Status (Honest Summary)
Concept	Status
Entities	✅ Implemented
Repositories	✅ Implemented
Services	✅ Implemented
Domain Events	❌ Not implemented
Value Objects	❌ Not implemented
Aggregates	❌ Not implemented

This is a DDD-inspired Clean Architecture, adapted to a realistic development context.

📝 Final Thoughts

Architecture choices were made with clarity and learning in mind

The project is easy to extend and easy to reason about

Security aspects were relaxed intentionally for development simplicity
