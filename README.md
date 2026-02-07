InventarioApp 📦
A modern web-based inventory management system built with clean architecture principles and enterprise-grade patterns.
![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![Blazor](https://img.shields.io/badge/Blazor-Server-blue)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-blue)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0-green)
🎯 Business Problem Solved
Small businesses struggle with inventory tracking, leading to stockouts, overstocking, and lack of visibility into product movements. InventarioApp provides real-time inventory management with comprehensive audit trails.
Key Features
- **Product Management**: Full CRUD with categories, pricing, and stock levels
- **Stock Movement Tracking**: Three types - IN (receipts), OUT (dispatches), ADJUST (corrections)
- **Real-time Audit Log**: Every change tracked with timestamps and details
- **Search & Filter**: Instant product search with category filtering
- **Responsive UI**: Terminal/hacker theme with MudBlazor components
  🏗️ Architecture
  Clean Architecture Implementation
  ┌─────────────────────────────────────┐
  │           Presentation Layer        │
  │  (Blazor Components & UI Logic)    │
  ├─────────────────────────────────────┤
  │            Service Layer           │
  │   (Business Logic & Validation)    │
  ├─────────────────────────────────────┤
  │           Data Access Layer        │
  │     (EF Core & Repository)        │
  ├─────────────────────────────────────┤
  │           Database Layer           │
  │        (PostgreSQL)               │
  └─────────────────────────────────────┘
### Project Structure
InventarioApp/
├── Components/
│   └── Pages/                 # Blazor UI Components
│       ├── Home.razor         # Dashboard with charts
│       ├── Products.razor     # Product CRUD
│       ├── Categories.razor   # Category CRUD
│       ├── Stock.razor        # Stock movements
│       └── ChangeLog.razor    # Audit log viewer
├── Services/                  # Business Logic Layer
│   ├── StockService.cs        # Stock movement operations
│   ├── ProductService.cs      # Product operations
│   └── CategoriesService.cs   # Category operations
├── Models/                    # Domain Entities
│   ├── Product.cs
│   ├── Category.cs
│   ├── StockMovement.cs
│   └── ChangeLog.cs
├── Data/                      # Data Access Layer
│   └── ApplicationDbContext.cs
├── Migrations/                # Database Schema
└── Program.cs                 # Application Entry Point
## 🚀 Tech Stack
### Backend
- **.NET 8.0** - Latest .NET with performance optimizations
- **Blazor Server** - Real-time UI with SignalR
- **Entity Framework Core 8.0** - Modern ORM with PostgreSQL provider
- **PostgreSQL 16** - Production-grade relational database
### Frontend
- **MudBlazor 8.15.0** - Material Design component library
- **Custom Terminal Theme** - Unique hacker/terminal aesthetic
### Development Tools
- **Entity Framework Core Migrations** - Schema versioning
- **Dependency Injection** - Loose coupling and testability
- **User Secrets** - Secure credential management
## 📋 Technical Highlights
### Service Layer Pattern
Each business domain has dedicated service classes:
```csharp
public class ProductService
{
    private readonly ApplicationDbContext _db;
    
    public async Task<string?> CreateOrUpdateProductAsync(Product product)
    {
        // Business logic validation
        // Database operations
        // Audit logging
    }
}
Audit Trail System
Comprehensive change tracking with ChangeLog entity:
public class ChangeLog
{
    public Entity { get; set; }      // What was changed?
    public EntityId { get; set; }    // Which record?
    public Action { get; set; }      // Create/Update/Delete
    public Details { get; set; }     // Human-readable description
    public Date { get; set; }        // When? (UTC timezone)
}
PostgreSQL Integration
- UTC Timestamps: Proper timezone handling
- Cascade Deletes: Referential integrity
- Indexes: Optimized queries
🔧 Installation & Setup
Prerequisites
- .NET 8.0 SDK
- PostgreSQL 16+ (or Docker)
- Git
Local Development Setup
1. Clone and setup
      git clone https://github.com/yourusername/InventarioApp.git
   cd InventarioApp
   
2. Database setup
      # Install migrations
   dotnet ef database update
   
3. Configure credentials
      # Set up User Secrets for development
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:InventarioDB" "Host=localhost;Port=5432;Database=InventarioDB;Username=postgres;Password=your_password;"
   
4. Run the application
      dotnet run
      Navigate to https://localhost:7180
Docker Alternative
# Start PostgreSQL with Docker
docker run -d --name postgres-inventario \
  -e POSTGRES_PASSWORD=Inventario123! \
  -e POSTGRES_DB=InventarioDB \
  -p 5432:5432 postgres:16
# Then run the app
dotnet run
🌐 Production Deployment
Railway (Recommended)
1. Push to GitHub
2. Connect Railway to your repository
3. Railway automatically detects .NET + PostgreSQL
4. Set production database connection in Railway dashboard
5. Deploy: https://tu-app.railway.app
Alternative Platforms
- Render.com - Similar to Railway
- Azure App Service - Microsoft platform
- AWS Elastic Beanstalk - Enterprise cloud
📊 Database Schema
Core Entities
-- Categories
CREATE TABLE "Categories" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255),
    "Description" TEXT
);
-- Products with Category FK
CREATE TABLE "Products" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "Price" DECIMAL(18,2) NOT NULL,
    "Stock" INT NOT NULL,
    "CategoryId" INT REFERENCES "Categories"("Id") ON DELETE CASCADE
);
-- Stock Movements for audit
CREATE TABLE "StockMovements" (
    "Id" SERIAL PRIMARY KEY,
    "ProductId" INT REFERENCES "Products"("Id") ON DELETE CASCADE,
    "Type" INT NOT NULL, -- 0=IN, 1=OUT, 2=ADJUST
    "Quantity" INT NOT NULL,
    "Notes" TEXT,
    "Date" TIMESTAMPTZ NOT NULL
);
-- Comprehensive Audit Log
CREATE TABLE "ChangeLogs" (
    "Id" SERIAL PRIMARY KEY,
    "Entity" VARCHAR(255) NOT NULL,
    "EntityId" INT NOT NULL,
    "Action" VARCHAR(255) NOT NULL,
    "Details" TEXT NOT NULL,
    "Date" TIMESTAMPTZ NOT NULL
);
🔄 Workflow
Stock Movement Process
1. User selects product → Real-time stock display
2. Choose movement type → Business rules applied:
   - IN: Stock increases
   - OUT: Validates sufficient stock
   - ADJUST: Sets absolute stock level
3. Transaction processed → Atomic database operations
4. Audit logged → Complete change history
Business Rules
- Cannot OUT more than current stock
- Category deletion cascades to products
- All changes are audited with UTC timestamps
- Input validation with friendly error messages
🎨 UI/UX Features
Terminal/Hacker Theme
- Fira Code monospace font
- Green/cyan color scheme mimicking old terminals
- CRT scanline effects for retro feel
- Responsive design for mobile/desktop
User Experience
- Instant search - no page refreshes
- Confirmation dialogs for destructive actions
- Color-coded stock movements - green=IN, red=OUT, yellow=ADJUST
- Real-time updates - immediate UI feedback
🔮 Future Enhancements
Technical Roadmap
- [ ] Blazor WebAssembly migration for better scalability
- [ ] Redis caching for frequently accessed data
- [ ] Background job processing with Hangfire for bulk operations
- [ ] API REST layer for mobile app integration
- [ ] Unit & Integration Tests with xUnit
- [ ] CI/CD pipeline with GitHub Actions
Feature Roadmap
- [ ] Multi-warehouse support
- [ ] Purchase order management
- [ ] Sales order integration
- [ ] Reporting & analytics dashboard
- [ ] User authentication & permissions
- [ ] Data export (Excel/PDF reports)
🤝 Contributing
Contributions are welcome! Please follow these steps:
1. Fork the repository
2. Create a feature branch: git checkout -b feature/amazing-feature
3. Commit changes: git commit -m 'Add amazing feature'
4. Push to branch: git push origin feature/amazing-feature
5. Open a Pull Request
Development Guidelines
- Follow C# naming conventions
- Add unit tests for new features
- Update documentation
- Keep PRs focused and descriptive
📞 Contact
- Portfolio: tu-portfolio.com (https://tu-portfolio.com)
- LinkedIn: linkedin.com/in/tu-perfil (https://linkedin.com/in/tu-perfil)
- Email: tu-email@ejemplo.com
---
📈 What I Learned
This project demonstrates proficiency in:
- Clean Architecture with proper separation of concerns
- Enterprise Patterns: Service Layer, Dependency Injection, Repository Pattern
- Modern .NET 8 features and best practices
- Database Design with EF Core Migrations and relationships
- Frontend Development with Blazor and component libraries
- DevOps: User Secrets, Git workflow, deployment pipelines
- Problem Solving: Real-time inventory management challenges
🚀 Built with passion for clean code and user experience# InventarioAppC-.NET
