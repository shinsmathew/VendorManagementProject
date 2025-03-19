# Vendor Management Project with JWT Authentication

Hello Shins Here......................... Nice to meet you!

This is a **.NET Core 8 REST API** for managing vendors, their contact persons, and bank account details, featuring user authentication with **JWT (JSON Web Token)**.

---

## Features

-  **JWT Authentication** (Register & Login).
-  **Vendor CRUD operations** (`Create`, `Read`, `Update`, `Delete`).
-  **Exception Handling** using middleware.
-  **Entity Framework Core** with **SQL Server**.
-  **Redis caching** for performance optimization.
-  **Repository and Service layer** for clean architecture.


### ** 1 Prerequisites**
--------------------------------------

Ensure you have the following installed:

- [.NET SDK 8.0]
- [Visual Studio 2022]
- [SQL Server Management Studio (SSMS)] - 2019
- [Redis] - (for caching)
- [Postman] or Swagger (for testing the API)

---

### **2 Setup and Installation**
----------------------------------


#### **Clone the Repository**

1. Clone the repository to your local machine:

 git clone https://github.com/shinsmathew/VendorManagementProject.git

#### **Database Setup**

- Run the provided SQL script to create the necessary databases and tables. (The SQL script is attached to the project.)

- Update the `appsettings.json` file with your SQL Server connection string:
 "
     "ConnectionStrings": {
    "DBSC": "Data Source=YOUR_SERVER_NAME;Initial Catalog=VendorManagementProject;Integrated Security=True;Trust Server Certificate=True"
    }

- Install Radis - download Redis for windows 11 git

- In Own computer - Advanced system Settings -> Environment variables -> Path -> Edit -> New -> Add new path for Redis (C:\Program Files\Redis) -> Ok

### ** Start the Redis server**

- Open Command Prompt and run:

 redis-server
 redis-cli

-> KEYS *     - to get all keys
-> KEYS Vendor_*  - filter keys for vendor
-> HGETALL /GET / LRANGE / SMEMBERS <key>  : Hash, String, List, Set    ( Eg:  GET Vendor_9) -[ disply/read the vendor_9 ]
-> DEL <key>
-> TYPE <key> (know the datatype of Key)



### **3 API Endpoints**
----------------------------------

- The application will start, and you can access the Swagger UI at:

   https://localhost:5001/swagger

Authentication-------

Register: POST /api/auth/register

Login: POST /api/auth/login

Vendors ----------

Get All Vendors: GET /api/vendors 

Get Vendor by ID: GET /api/vendors/{id} 

Create Vendor: POST /api/vendors (Admin Only)

Update Vendor: PUT /api/vendors/{id} (Admin Only)

Delete Vendor: DELETE /api/vendors/{id} (Admin Only)

------------------------------------------------

#Packages

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.Extensions.Caching.StackExchangeRedis
StackExchange.Redis
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package BCrypt.Net-Next

#Tables

select * from BankAccounts
select * from ContactPersons
select * from Vendors
select * from VendorUsers





