# SahayaSetu - NGO Management Platform

A platform connecting donors with verified NGOs for transparent donations, fundraising, and volunteer coordination.

## Features

**For Donors:**
- Browse verified NGOs and active fundraisers
- Make secure donations via Razorpay
- Contribute resources and volunteer for causes
- Track donation history

**For NGOs:**
- Register with DARPAN ID verification
- Create fundraising campaigns
- Post resource and volunteer requests
- Receive email notifications for donations

**For Admins:**
- Approve/reject NGO registrations
- Monitor platform activities
- Manage users and NGOs

## Tech Stack

**Frontend:** React 19, Vite, Bootstrap 5  
**Backend:** Spring Boot 3.5, Java 17, Spring Security + JWT  
**Database:** MySQL 8.0+  
**Payment:** Razorpay integration  
**Email:** Spring Mail with HTML templates

## Prerequisites

- Node.js 18+
- MySQL 8.0+
- Java 17+
- Maven 3.8+

## Installation

### 1. Database Setup

```sql
CREATE DATABASE sahayaSetuDemo;
```

### 2. Backend Setup

```bash
cd backend-java/sahayaSetu

# Copy and configure environment file
cp .env.example .env

# Edit .env with:
# - Database credentials (DB_URL, DB_USERNAME, DB_PASSWORD)
# - JWT secret (JWT_SECRET)
# - Email config (MAIL_USERNAME, MAIL_PASSWORD)
# - Razorpay keys (RAZORPAY_KEY_ID, RAZORPAY_KEY_SECRET)

# Build and run
mvn clean install
mvn spring-boot:run
```

Backend runs on: `http://localhost:8080`

### 3. Frontend Setup

```bash
cd frontend

# Install dependencies
npm install

# Configure environment
cp .env.example .env.local
# Set VITE_API_URL=http://localhost:8080/api

# Start development server
npm run dev
```

Frontend runs on: `http://localhost:5173`

## Project Structure

```
sahayasetu_final/
├── frontend/                 # React application
│   ├── src/
│   │   ├── components/
│   │   ├── pages/
│   │   └── services/
│   └── .env.example
│
└── backend-java/
    └── sahayaSetu/           # Spring Boot application
        ├── src/main/java/com/sahayaSetu/
        │   ├── config/       # Security, JWT, CORS
        │   ├── controllers/  # REST API endpoints
        │   ├── services/     # Business logic (Interface + Impl)
        │   ├── repositories/ # Data access layer
        │   ├── entities/     # JPA entities
        │   └── dtos/         # Data transfer objects
        ├── src/main/resources/
        │   ├── application.properties
        │   └── templates/    # Email templates
        ├── pom.xml
        └── .env.example
```

## API Documentation

Swagger UI available at: `http://localhost:8080/swagger-ui.html`

### Key Endpoints

**Authentication:**
- `POST /api/auth/register` - Register as donor
- `POST /api/auth/register-ngo` - Register NGO
- `POST /api/auth/login` - User login
- `POST /api/auth/login-ngo` - NGO login

**Donor Operations:**
- `GET /api/donor/fundraisers` - Browse active fundraisers
- `POST /api/donor/requests/{id}/donate` - Make donation
- `POST /api/donor/requests/{id}/volunteer` - Apply as volunteer
- `GET /api/donor/contributions` - View contribution history

**NGO Operations:**
- `POST /api/ngo/requests/fundraiser` - Create fundraiser
- `POST /api/ngo/requests/resource` - Create resource request
- `POST /api/ngo/requests/volunteer` - Create volunteer request
- `GET /api/ngo/{id}/donations` - View received donations

**Admin Operations:**
- `GET /api/admin/ngos` - List all NGOs
- `PUT /api/admin/ngos/{id}/approve` - Approve NGO
- `PUT /api/admin/ngos/{id}/disapprove` - Reject NGO

## Architecture Highlights

**Backend Design:**
- Service layer with interface-implementation pattern
- Constructor-based dependency injection
- JWT authentication with role-based access control
- DTO pattern for clean API contracts
- Async email notifications

**Security:**
- BCrypt password encryption
- JWT token authentication
- Role-based authorization (DONOR, NGO, ADMIN)
- Input validation

## Environment Variables

**Backend (.env):**
```properties
DB_URL=jdbc:mysql://localhost:3306/sahayaSetuDemo
DB_USERNAME=root
DB_PASSWORD=your_password
JWT_SECRET=your_secret_key
MAIL_USERNAME=your_email@gmail.com
MAIL_PASSWORD=your_app_password
RAZORPAY_KEY_ID=your_key
RAZORPAY_KEY_SECRET=your_secret
```

**Frontend (.env.local):**
```properties
VITE_API_URL=http://localhost:8080/api
```

## License

CDAC Final Project