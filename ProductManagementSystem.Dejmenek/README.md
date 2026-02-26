# ProductManagementSystem

## Table of Contents
- [General Information](#general-information)
- [Features](#features)
- [Used Technologies](#used-technologies)
- [Database Structure](#database-structure)
- [Local Setup](#local-setup)
- [Usage Examples](#usage-examples)
- [Learned Things & Study Materials](#learned-things--study-materials)
- [Notes](#notes)

## General Information

ProductManagementSystem is a back-of-store application designed for managing computer-related products. The system provides comprehensive product management capabilities for businesses dealing with computer hardware, specifically laptops and desktop computers. It features a role-based access control system that enables efficient staff management and secure product operations.

## Features

### Product Management
- **Add Products**: Create new laptop and desktop computer entries with detailed specifications including processor, RAM, storage, operating system, and graphics card
- **Update Products**: Modify existing product information and specifications
- **Delete Products**: Remove individual products or perform bulk deletions
- **View Products**: Browse and search through the product catalog with sorting and pagination capabilities

### Admin Area
- **Role-Based Access**: Admin area accessible only to users with the 'Admin' role
- **Staff Management**: Admin staff can perform complete CRUD operations on staff accounts
  - Add new staff members
  - Update staff information
  - Delete staff accounts
  - View all staff members

### Authentication & Security
- **Staff Registration**: Self-registration system for staff members with email confirmation requirement
- **Email Confirmation**: Account confirmation via email to ensure valid staff accounts
- **Password Reset**: Secure password reset functionality
- **Password Recovery**: Password recovery system for staff members who forget their credentials
- **Email Service**: Integrated email service for account confirmation and password-related functions

## Used Technologies

- **Frontend**: 
  - Razor Pages
  - HTML
  - CSS
  - JavaScript
  - Bootstrap

- **Backend**:
  - .NET 9.0
  - ASP.NET Core Identity (Authentication & Authorization)
  - Entity Framework Core (ORM)

- **Database**:
  - SQL Server

- **Logging**:
  - Serilog (with SQL Server sink)

- **Additional Libraries**:
  - FluentValidation (Input validation)

## Database Structure

The application uses a hierarchical product model with Table-Per-Hierarchy (TPH) inheritance strategy:

- **Products Table**: Base table storing all products with a discriminator column
  - Product (base)
  - Computer (abstract, extends Product)
    - Laptop (extends Computer)
    - Desktop (extends Computer)

- **Logs Table**: Table storing data related to application's activity
- **Identity Tables**: ASP.NET Core Identity tables for user management and authentication

<img width="820" height="746" alt="image" src="https://github.com/user-attachments/assets/65aec3f7-d5eb-4e35-bc2e-9800a5255951" />

## Local Setup

This section provides step-by-step instructions to set up and run the ProductManagementSystem on your local machine.

### Prerequisites

Before getting started, ensure you have the following installed:

- **.NET 9.0 SDK** or later - [Download](https://dotnet.microsoft.com/download/dotnet/9.0)
- **SQL Server** or **SQL Server LocalDB** (included with Visual Studio)
  - For LocalDB, you can install it via Visual Studio Installer or as a standalone component
- **A code editor** such as Visual Studio 2022, Visual Studio Code, or JetBrains Rider
- **Git** for cloning the repository

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Dejmenek/ProductManagementSystem.git
   cd ProductManagementSystem
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

### Database Configuration

The application uses SQL Server LocalDB by default. The connection string is configured in `src/ProductManagementSystem.Web/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Database=PMS;Integrated Security=True;Trusted_Connection=True"
}
```

**Database Initialization:**

The database and tables will be created automatically when you first run the application thanks to Entity Framework Core migrations.

If you need to use a different SQL Server instance, update the `DefaultConnection` string in `appsettings.json` accordingly.

### Email Configuration

The application requires email configuration for user registration, email confirmation, and password recovery features. Email settings are configured in `src/ProductManagementSystem.Web/appsettings.json` under the `EmailConfig` section.

#### Default Configuration

```json
"EmailConfig": {
  "From": "",
  "SmtpServer": "smtp.gmail.com",
  "Port": 465,
  "Username": "",
  "Password": ""
}
```

#### Setting Up Email for Local Development

To enable email functionality, you need to configure the following fields:

1. **From**: The email address that will appear as the sender
   - Example: `"noreply@yourdomain.com"` or your Gmail address

2. **SmtpServer**: The SMTP server address
   - For Gmail: `"smtp.gmail.com"` (default)
   - For Outlook/Hotmail: `"smtp-mail.outlook.com"`
   - For other providers, check their SMTP server documentation

3. **Port**: The SMTP port number
   - For Gmail with SSL: `465` (default)
   - For Gmail with TLS: `587`
   - For Outlook: `587`

4. **Username**: Your email account username
   - For Gmail: Your full Gmail address (e.g., `"yourname@gmail.com"`)
   - For Outlook: Your full Outlook/Hotmail address

5. **Password**: Your email account password or app-specific password
   - **Important**: For Gmail, you must use an **App Password**, not your regular Gmail password
   - For Outlook, you may also need an app-specific password if you have 2FA enabled

#### Example Configuration for Gmail

```json
"EmailConfig": {
  "From": "yourname@gmail.com",
  "SmtpServer": "smtp.gmail.com",
  "Port": 465,
  "Username": "yourname@gmail.com",
  "Password": "your-app-specific-password"
}
```

### External Login Providers

The application supports sign-in via **Google**, **Twitter (X)**, **GitHub**, **Facebook**, and **Microsoft**. OAuth credentials must never be stored in `appsettings.json`. Use the [.NET Secret Manager](https://learn.microsoft.com/aspnet/core/security/app-secrets) to store them securely during local development.

#### 1. Initialize User Secrets

Run the following command once from the repository root:

```bash
dotnet user-secrets init --project src/ProductManagementSystem.Web
```

#### 2. Obtain OAuth Credentials (Google, Twitter(X), GitHub, Facebook, Microsoft)

#### 3. Store Credentials Using User Secrets

**Google**
```bash
dotnet user-secrets set "Authentication:Google:ClientId" "your-google-client-id" --project src/ProductManagementSystem.Web
dotnet user-secrets set "Authentication:Google:ClientSecret" "your-google-client-secret" --project src/ProductManagementSystem.Web
```

**Twitter (X)**
```bash
dotnet user-secrets set "Authentication:Twitter:ConsumerAPIKey" "your-twitter-api-key" --project src/ProductManagementSystem.Web
dotnet user-secrets set "Authentication:Twitter:ConsumerSecret" "your-twitter-consumer-secret" --project src/ProductManagementSystem.Web
```

**GitHub**
```bash
dotnet user-secrets set "Authentication:GitHub:ClientId" "your-github-client-id" --project src/ProductManagementSystem.Web
dotnet user-secrets set "Authentication:GitHub:ClientSecret" "your-github-client-secret" --project src/ProductManagementSystem.Web
```

**Facebook**
```bash
dotnet user-secrets set "Authentication:Facebook:AppId" "your-facebook-app-id" --project src/ProductManagementSystem.Web
dotnet user-secrets set "Authentication:Facebook:AppSecret" "your-facebook-app-secret" --project src/ProductManagementSystem.Web
```

**Microsoft**
```bash
dotnet user-secrets set "Authentication:Microsoft:ClientId" "your-microsoft-client-id" --project src/ProductManagementSystem.Web
dotnet user-secrets set "Authentication:Microsoft:ClientSecret" "your-microsoft-client-secret" --project src/ProductManagementSystem.Web
```

#### 4. Verify Stored Secrets

```bash
dotnet user-secrets list --project src/ProductManagementSystem.Web
```

### Running the Application

1. **Navigate to the Web project directory**
   ```bash
   cd src/ProductManagementSystem.Web
   ```

2. **Run the application**
   ```bash
   dotnet run
   ```

3. **Access the application**
   - Open your browser and navigate to the URL shown in the console (typically `https://localhost:5001` or `http://localhost:5000`)
   - The database will be created automatically on first run
   - You can now register a staff account (requires email confirmation)

## Usage Examples

### Product Management Interface
The main product management interface allows users to view, add, edit, and delete products with comprehensive filtering and sorting options.

<img width="1863" height="893" alt="image" src="https://github.com/user-attachments/assets/2ea563fb-c5aa-48db-854b-ad4f56156a2a" />

### Admin Area
The admin area provides staff management capabilities, accessible only to users with administrative privileges.

<img width="1864" height="896" alt="image" src="https://github.com/user-attachments/assets/8afd0593-2999-470e-bf58-a90d7036cf8c" />

### Staff Registration and Password Recovery
Staff members can register their accounts with email confirmation and utilize password recovery features when needed.

<img width="1866" height="896" alt="image" src="https://github.com/user-attachments/assets/ff1240f6-1f10-46e7-b87e-aba56fdfa3aa" />

<img width="1868" height="894" alt="image" src="https://github.com/user-attachments/assets/13d9976e-9742-4697-a08c-25af01f13894" />

## Learned Things & Study Materials

### Caching
Implemented server-side caching using `MemoryCache`. Learned about various cache types and strategies such as cache-aside, write-through, and read-through. Explored cache eviction policies and common problems like cache stampede, hot keys and cache consistency for system design.

### Authorization
Practiced implementing authorization using ASP.NET Core Identity.

### Clean Architecture
Practiced the principles and structure of clean architecture in this project.

### Study Materials
- [Caching in a system design interview](https://youtu.be/1NngTUYPdpI?si=Epb7zjwx3KRlxWlz)
- [Caching system design concept for beginners](https://www.geeksforgeeks.org/system-design/caching-system-design-concept-for-beginners)

## Notes

- Clean architecture was implemented in this project for practice purposes only. It is clearly an overkill for a simple project like this, but served as a valuable learning experience.

---

*For more information or to report issues, please visit the [GitHub repository](https://github.com/Dejmenek/ProductManagementSystem).*
