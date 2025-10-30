### 📦 Prerequisites  ###
Requirement	    Version
.NET SDK	    8.0+
MySQL Server	8.0+
Git	            Latest

### 🚀 Run the Project via CLI (.NET 8) ###
This repository contains:
✅ Web Management API (ASP.NET Core Web API)
✅ Warehouse Management System (Console Application)
✅ Warehouse Management Test (Unit Tests)

### Project Structure ###
Warehouse-Managment-System
│-- Warehouse Management System.sln
│-- Web Management API/        <-- Web API (.NET 8)
│-- Warehouse Managemet System/ <-- Console App
└-- Warehouse Managment Test/   <-- Tests

### How to run ###
1️⃣ Clone the repository
git clone <repo-url>
cd Warehouse-Managment-System

2️⃣ Restore dependencies
dotnet restore

3️⃣ Build solution
dotnet build

▶️ Run Applications
✅ Run Console App
cd "Warehouse Managemet System"
dotnet run
cd ..

✅ Run Web API
cd "Web Management API"
dotnet run
cd ..

🧪 Run Unit Tests
cd "Warehouse Managment Test"
dotnet test
cd ..

[![.NET](https://github.com/ewrefads/Warehouse-Managment-System/actions/workflows/dotnet.yml/badge.svg?branch=development)](https://github.com/ewrefads/Warehouse-Managment-System/actions/workflows/dotnet.yml)
