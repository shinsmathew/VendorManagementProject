# Vendor Management Project with JWT Authentication

This is a **.NET Core 8 REST API** for managing vendors, their contact persons, and bank account details, featuring user authentication with **JWT (JSON Web Token)**.

---

## 📌 Features

- ✅ **JWT Authentication** (Register & Login).
- ✅ **Vendor CRUD operations** (`Create`, `Read`, `Update`, `Delete`).
- ✅ **Exception Handling** using middleware.
- ✅ **Entity Framework Core** with **SQL Server**.
- ✅ **Redis caching** for performance optimization.
- ✅ **Repository and Service layer** for clean architecture.

---

##   Getting Started

### **1️⃣ Prerequisites**

Ensure you have the following installed:

- [.NET SDK 8.0]
- [Visual Studio 2022]
- [SQL Server Management Studio (SSMS)] - 2019
- [Redis] - (for caching)
- [Postman] or Swagger (for testing the API)

---

### **2️⃣ Setup and Installation**

#### **Clone the Repository**

1. Clone the repository to your local machine:

 git clone https://github.com/shinsmathew/VendorManagementProject.git

#### **Database Setup**

1. Run the provided SQL script to create the necessary databases and tables. (The SQL script is attached to the project.)

2. Update the `appsettings.json` file with your SQL Server connection string:


     "ConnectionStrings": {
    "DBSC": "Data Source=YOUR_SERVER_NAME;Initial Catalog=VendorManagementProject;Integrated Security=True;Trust Server Certificate=True"
}

3. Install Radis - download Redis for windows 11 git

- In Own computer - Advanced system Settings -> Environment variables -> Path -> Edit -> New -> Add new path for Redis (C:\Program Files\Redis) -> Ok

Start the Redis server:

4. Open Command Prompt and run:

 redis-server
 redis-cli

-> KEYS *     - to get all keys
-> KEYS Vendor_*  - filter keys for vendor
-> HGETALL /GET / LRANGE / SMEMBERS <key>  : Hash, String, List, Set    ( Eg:  GET Vendor_9) -[ disply/read the vendor_9 ]
-> DEL <key>
-> TYPE <key> (know the datatype of Key)


5. The application will start, and you can access the Swagger UI at:

   https://localhost:5001/swagger


📄 API Endpoints ********

Authentication-------

Register: POST /api/auth/register

Login: POST /api/auth/login

Vendors ----------

Get All Vendors: GET /api/vendors 

Get Vendor by ID: GET /api/vendors/{id} 

Create Vendor: POST /api/vendors (Admin Only)

Update Vendor: PUT /api/vendors/{id} (Admin Only)

Delete Vendor: DELETE /api/vendors/{id} (Admin Only)





