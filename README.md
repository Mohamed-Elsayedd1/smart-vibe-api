# SmartVibe API 🛒

A robust and scalable **RESTful API** for a modern e-commerce platform, built with **ASP.NET Core (.NET 10)** and **PostgreSQL**.

---

## ✨ Features

- 🔐 **JWT Authentication** — Secure login, registration, and role-based authorization
- 🛍️ **Product Management** — Full CRUD with categories, ratings, reviews, and image support
- 📦 **Order Management** — Complete order lifecycle tracking
- 🖼️ **Image Uploads** — Cloud-based image storage via **Cloudinary**
- 🔑 **Password Reset** — OTP-based forgot/reset password flow
- 🐳 **Docker Ready** — Dockerfile included for easy containerization
- 🚀 **Auto Migrations** — Database migrations run automatically on startup

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core (.NET 10) |
| Database | PostgreSQL |
| ORM | Entity Framework Core 9 |
| Auth | JWT Bearer Tokens |
| Image Storage | Cloudinary |
| Password Hashing | BCrypt.Net |
| Containerization | Docker |

---

## 📁 Project Structure

```
SmartVibe/
├── Controllers/
│   ├── AuthController.cs        # Register, Login, Profile, Password Reset
│   ├── ProductsController.cs    # Product CRUD
│   ├── OrdersController.cs      # Order management
│   └── OtherControllers.cs      # Categories, Reviews, etc.
├── Models/
│   └── Models.cs                # AppUser, Product, Order, Category, Review
├── DTOs/
│   └── DTOs.cs                  # Request/Response data transfer objects
├── Data/
│   └── AppDbContext.cs          # EF Core DB context
├── Helpers/
│   └── JwtHelper.cs             # JWT token generation
├── Migrations/                  # EF Core database migrations
├── Dockerfile
└── Program.cs                   # App configuration & middleware
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/)
- [Cloudinary Account](https://cloudinary.com/)

### 1. Clone the repository

```bash
git clone https://github.com/t9amw0rk-sys/smartvibe-api.git
cd smartvibe-api
```

### 2. Configure Environment Variables

Create an `appsettings.Development.json` file or set the following environment variables:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=smartvibe;Username=postgres;Password=yourpassword"
  },
  "Jwt": {
    "Secret": "your-super-secret-key-min-32-chars",
    "Issuer": "SmartVibe",
    "Audience": "SmartVibeUsers"
  },
  "Frontend": {
    "Url": "http://localhost:5173"
  }
}
```

Or use environment variables directly:

```bash
export ConnectionStrings__DefaultConnection="Host=localhost;Database=smartvibe;..."
export JWT_SECRET="your-super-secret-key"
export FRONTEND_URL="http://localhost:5173"
```

### 3. Run the API

```bash
dotnet run
```

The API will be available at `http://localhost:5000`. Migrations run automatically on startup.

---

## 🐳 Docker

```bash
docker build -t smartvibe-api .
docker run -p 5000:5000 \
  -e ConnectionStrings__DefaultConnection="your-connection-string" \
  -e JWT_SECRET="your-secret" \
  smartvibe-api
```

---

## 📡 API Endpoints

### Auth
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/auth/register` | Register new user | ❌ |
| POST | `/api/auth/login` | Login and get token | ❌ |
| GET | `/api/auth/me` | Get current user | ✅ |
| PUT | `/api/auth/profile` | Update profile | ✅ |
| POST | `/api/auth/forgot-password` | Request reset code | ❌ |
| POST | `/api/auth/reset-password` | Reset password with code | ❌ |

### Products
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/products` | Get all products | ❌ |
| GET | `/api/products/{id}` | Get product by ID | ❌ |
| POST | `/api/products` | Create product | 🔐 Admin |
| PUT | `/api/products/{id}` | Update product | 🔐 Admin |
| DELETE | `/api/products/{id}` | Delete product | 🔐 Admin |

### Orders
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/orders` | Get user orders | ✅ |
| POST | `/api/orders` | Create order | ✅ |
| GET | `/api/orders/{id}` | Get order details | ✅ |

### Categories
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/categories` | Get all categories | ❌ |
| POST | `/api/categories` | Create category | 🔐 Admin |
| PUT | `/api/categories/{id}` | Update category | 🔐 Admin |
| DELETE | `/api/categories/{id}` | Delete category | 🔐 Admin |

---

## 🔒 Authentication

This API uses **JWT Bearer Tokens**. After login or registration, include the token in the `Authorization` header:

```
Authorization: Bearer <your-token>
```

---

## 🤝 Contributing

Pull requests are welcome! For major changes, please open an issue first.

---

## 📄 License

This project is licensed under the MIT License.
