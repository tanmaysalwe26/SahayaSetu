# SahayaSetu

Connecting donors with verified NGOs for transparent donations and volunteer coordination.

## What's Inside

**Donors can:**
- Browse verified NGOs and fundraisers
- Donate securely via Razorpay
- Volunteer for causes
- Track their contributions

**NGOs can:**
- Register with DARPAN ID
- Create fundraising campaigns
- Post resource/volunteer needs
- Get email notifications for donations

**Admins can:**
- Approve/reject NGO registrations
- Monitor platform activity
- Manage users

## Tech Stack

- **Frontend:** React 19 + Vite + Bootstrap 5
- **Backend:** Spring Boot 3.5 (Java 17) OR ASP.NET Core 8.0 (C# 12)
- **Auth:** JWT with role-based access
- **Database:** MySQL 8.0+
- **Payments:** Razorpay
- **Email:** SMTP notifications

## Getting Started

You'll need:
- Node.js 18+
- MySQL 8.0+
- Java 17+ & Maven (for Java backend) OR .NET 8.0 SDK (for .NET backend)

## Setup

### Database

```sql
CREATE DATABASE sahayaSetuDemo;
```

### Backend (pick one)

**Java:**
```bash
cd backend-java/sahayaSetu
cp .env.example .env
# Edit .env with your DB, JWT, email, and Razorpay credentials
mvn clean install
mvn spring-boot:run
```

**.NET:**
```bash
cd backend-dotnet/SahayaSetu
# Edit appsettings.json with your configs
dotnet restore
dotnet run
```

Backend will run on `localhost:8080` (Java) or `localhost:5000` (.NET)

### Frontend

```bash
cd frontend
npm install
cp .env.example .env.local
# Set VITE_API_URL=http://localhost:8080/api
npm run dev
```

Frontend runs on `localhost:5173`

## Project Structure

```
sahayasetu_final/
├── frontend/
│   ├── src/
│   │   ├── components/
│   │   ├── pages/
│   │   └── services/
│   └── .env.example
├── backend-java/sahayaSetu/
│   ├── src/main/java/com/sahayaSetu/
│   │   ├── config/
│   │   ├── controllers/
│   │   ├── services/
│   │   ├── repositories/
│   │   ├── entities/
│   │   └── dtos/
│   └── .env.example
└── backend-dotnet/SahayaSetu/
    ├── Controllers/
    ├── Services/
    ├── Repositories/
    ├── Models/
    ├── DTOs/
    └── Middleware/
```

## API Endpoints

Swagger docs: `http://localhost:8080/swagger-ui.html`

**Auth:**
- `POST /api/auth/register` - Donor signup
- `POST /api/auth/register-ngo` - NGO signup
- `POST /api/auth/login` - Donor login
- `POST /api/auth/login-ngo` - NGO login

**Donors:**
- `GET /api/donor/fundraisers` - List fundraisers
- `POST /api/donor/requests/{id}/donate` - Donate
- `POST /api/donor/requests/{id}/volunteer` - Volunteer
- `GET /api/donor/contributions` - Contribution history

**NGOs:**
- `POST /api/ngo/requests/fundraiser` - New fundraiser
- `POST /api/ngo/requests/resource` - Request resources
- `POST /api/ngo/requests/volunteer` - Request volunteers
- `GET /api/ngo/{id}/donations` - View donations

**Admin:**
- `GET /api/admin/ngos` - All NGOs
- `PUT /api/admin/ngos/{id}/approve` - Approve NGO
- `PUT /api/admin/ngos/{id}/disapprove` - Reject NGO

## How It Works

- REST API with service/repository layers
- JWT auth with roles (DONOR, NGO, ADMIN)
- BCrypt password hashing
- Async email notifications
- DTOs for clean API contracts

## Config

**Java (.env):**
```
DB_URL=jdbc:mysql://localhost:3306/sahayaSetuDemo
DB_USERNAME=root
DB_PASSWORD=your_password
JWT_SECRET=your_secret
MAIL_USERNAME=your_email@gmail.com
MAIL_PASSWORD=your_app_password
RAZORPAY_KEY_ID=your_key
RAZORPAY_KEY_SECRET=your_secret
```

**.NET (appsettings.json):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=sahayaSetuDemo;User=root;Password=your_password;"
  },
  "JwtSettings": {
    "Secret": "your_secret",
    "Issuer": "SahayaSetu",
    "Audience": "SahayaSetuUsers"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "Username": "your_email@gmail.com",
    "Password": "your_app_password"
  },
  "Razorpay": {
    "KeyId": "your_key",
    "KeySecret": "your_secret"
  }
}
```

**Frontend (.env.local):**
```
VITE_API_URL=http://localhost:8080/api
```

---

CDAC Final Project