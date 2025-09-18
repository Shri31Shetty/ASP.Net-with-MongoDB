# I. Student Management API

ASP.NET Core 6 Web API with MongoDB for CRUD operations on student data. Includes Swagger for interactive API documentation.

---

## II. Features
- Retrieve all students or a single student by ID
- Create a new student
- Update existing student details
- Delete a student
- MongoDB backend
- Swagger UI for testing and docs

---

## III. Tech Stack
- ASP.NET Core 6 Web API
- MongoDB (Atlas or local)
- Swagger / Swashbuckle

---

## IV. Project Structure
- StudentManagement.sln — Solution file
- StudentManagement
  - Controllers — API endpoints
  - Services — MongoDB data access
  - Models — Student and settings
  - appsettings.json — Configuration
  - Program.cs — App bootstrap

---

## V. Getting Started

### A. Prerequisites
- Visual Studio 2022 or later
- .NET 6 SDK
- MongoDB
  - Atlas cluster with allowlisted IP, or
  - Local MongoDB at mongodb://localhost:27017

### B. Configuration
Update appsettings.json:

{
  "StudentDatabase": {
    "ConnectionString": "YOUR_MONGODB_CONNECTION_STRING",
    "DatabaseName": "StudentDB",
    "CollectionName": "Students"
  }
}

- Atlas example: `mongodb+srv://<username>:<password>@<cluster>.mongodb.net`
- Local example: `mongodb://localhost:27017`

Tip: Use appsettings.Development.json or User Secrets for sensitive values.

### C. Run (Visual Studio)
- Open `StudentManagement.sln`
- Set the Web API project as Startup Project
- Press F5 to run
- Swagger UI: `http://localhost:<port>/swagger`

---

## VI. API Endpoints
- GET `/api/students` — Get all students
- GET `/api/students/{id}` — Get a student by ID
- POST `/api/students` — Create a new student
- PUT `/api/students/{id}` — Update an existing student
- DELETE `/api/students/{id}` — Delete a student
