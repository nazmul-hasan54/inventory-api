1. Clone Repository

   ```bash
   git clone https://github.com/nazmul-hasan54/inventory-api.git
   ```

2. Apply Database Migration

   ```bash
   add-migration InitialCreate
   update-database
   ```

3. Run Project

   Project URL: `https://localhost:7186`

4. Create User (in Swagger)
   POST `/api/auth/register`
   ```json
   {
     "username": "admin",
     "password": "admin123",
     "role": "Admin"
   }
   ```
5. Login and Get Token
   POST `/api/auth/login`
   ```json
   {
     "username": "admin",
     "password": "admin123"
   }
   ```
