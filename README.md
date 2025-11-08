# inventory-api
Tech Stack

Backend: .NET 8 + Entity Framework Core + SQLite + JWT Authentication
DB: SQLite (auto-created by migrations)

1. Clone Repository
   git clone https://github.com/nazmul-hasan54/inventory-api.git


2. Apply Database Migration
   add-migration InitialCreate
   update-database

3. Run the project and api will available at:
   https://localhost:7186

4. Create a User (required before login)
   In Swagger → POST /api/auth/register
   
   {
    "username": "admin",
    "password": "admin123",
    "role": "Admin"
  }

5. Login and Get Token
   Swagger → POST /api/auth/login
   
   {
    "username": "admin",
    "password": "admin123"
  }
