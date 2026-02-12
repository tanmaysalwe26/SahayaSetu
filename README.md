# SahayaSetu - NGO Management and Donation Platform

> A comprehensive platform connecting donors with verified NGOs for transparent donation management, fundraising campaigns, and volunteer coordination.

 Overview

SahayaSetu bridges the gap between donors and NGOs, enabling:
- Secure monetary and resource donations
- Fundraiser campaign management
- Volunteer coordination
- Admin oversight and NGO verification
- Razorpay payment integration

 Features

**Donors:** Registration, donations, resource contributions, volunteering, donation tracking

**NGOs:** DARPAN ID verification, fundraiser campaigns, resource requests, volunteer management, email notifications

**Admins:** NGO approval/rejection, activity monitoring, dashboard analytics, user management

 Tech Stack

**Frontend:** React 19.2.0, Vite 7.2.4, React Router, Axios, Bootstrap 5.3.8

**Backend (Java):** Spring Boot 3.5.10, Java 17, Spring Security + JWT, Razorpay SDK, Spring Mail

**Backend (.NET):** ASP.NET Core 8.0, EF Core 8.0.22, JWT Authentication

**Database:** MySQL 8.0+

 Project Structure

```
sahayasetu_final/
├── frontend/              # React + Vite application
├── backend-java/          # Spring Boot backend (Primary)
└── backend-dotnet/        # ASP.NET Core backend (Alternative)
```

 Prerequisites

- Node.js 18+, npm 9+
- MySQL 8.0+
- Java 17+ & Maven 3.8+ (for Java backend)
- .NET SDK 8.0+ (for .NET backend)

 Installation

**1. Database Setup**
```sql
CREATE DATABASE sahayaSetuDemo;
```

**2. Frontend**
```bash
cd frontend
npm install
cp .env.example .env.local  # Configure environment variables
```

**3. Backend (Java)**
```bash
cd backend-java/sahayaSetu
cp .env.example .env  # Configure environment variables
mvn clean install
```

**4. Backend (.NET - Alternative)**
```bash
cd backend-dotnet/SahayaSetu
dotnet restore
dotnet ef database update
```

 Configuration

Configure environment variables in:
- `frontend/.env.local` - API URL, app settings
- `backend-java/sahayaSetu/.env` - Database, JWT, Mail, Razorpay
- `backend-dotnet/SahayaSetu/appsettings.json` - Database, JWT

Refer to `.env.example` files for required variables.

 Running the Application

**Backend (Java)**
```bash
cd backend-java/sahayaSetu
mvn spring-boot:run
```

**Backend (.NET)**
```bash
cd backend-dotnet/SahayaSetu
dotnet run
```

**Frontend**
```bash
cd frontend
npm run dev
```

**Access:**
- Frontend: http://localhost:5173
- Backend API: http://localhost:8080/api
- Swagger: http://localhost:8080/swagger-ui.html


