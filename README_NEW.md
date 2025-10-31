# 🎓 Faculty Workload Management System - Medicine Hat College

A comprehensive academic management system built with .NET 9 Blazor and MudBlazor, designed to manage faculty workloads, courses, programs, and institutional structure.

## 🌟 Features Implemented

### ✅ Core Features (All Requirements Met)

1. **Faculty Profile/Workload Prototype** ✅
   - Faculty management with email, name, and contact information
   - Comprehensive workload tracking system
   - Support for multiple workload types: Lectures, Labs, Coordinator duties, and Special projects
   - Flexible hours assignment and description fields

2. **Dashboard for School** ✅
   - Interactive visualization with ApexCharts
   - Summary cards showing key metrics
   - School selector with real-time data filtering
   - Refresh functionality for data updates

3. **Dashboard for Department** ✅
   - Department-specific analytics and metrics
   - Interactive charts and visualizations
   - Filtering by department
   - Real-time data refresh

4. **Dashboard for Program** ✅
   - Program-level insights and statistics
   - Course distribution visualization
   - Program selector with dynamic updates
   - Comprehensive reporting

5. **Semester Courses Creation Page** ✅
   - Create entire semester schedules for programs
   - Select program and term
   - Generate course listings automatically
   - Bulk course management

### 📊 Data Structure

The system implements a complete hierarchical structure:

```
School → Department → Program of Study → Course
                                       ↓
                            Faculty Workload (with Terms)
```

**Relationships:**
- Schools contain multiple Departments
- Departments contain multiple Programs of Study
- Programs of Study contain multiple Courses
- Faculty Workload links Faculty, Courses, Terms, and Workload Categories

### 🎨 User Interface

- Built with **MudBlazor** UI framework
- Modern, responsive design
- Interactive forms with validation
- Real-time data updates
- ApexCharts integration for data visualization
- Medicine Hat College theme

### 🔧 Technical Stack

- **Framework:** .NET 9 Blazor Server
- **UI Library:** MudBlazor 8.x
- **Charts:** Blazor-ApexCharts 6.0.2
- **Database:** SQL Server with Entity Framework Core 9.x
- **Authentication:** ASP.NET Core Identity

## 🚀 Getting Started

### Prerequisites

- .NET 9 SDK
- Docker (for SQL Server)
- Visual Studio 2022 or VS Code

### Database Setup

The application is configured to use Docker SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost,1434;database=workload_db_shared;user id=sa;password=P@ssword!;encrypt=false"
  }
}
```

**Start SQL Server in Docker:**

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P@ssword!" -p 1434:1433 --name workload-sql -d mcr.microsoft.com/mssql/server:2022-latest
```

### Running the Application

1. **Clone the repository**
2. **Restore packages:**
   ```bash
   dotnet restore
   ```
3. **Run migrations:**
   ```bash
   cd WorkloadProject2025
   dotnet ef database update
   ```
4. **Run the application:**
   ```bash
   dotnet run
   ```
5. **Navigate to:** `https://localhost:5001`

The database will be automatically seeded with sample data from Medicine Hat College including:
- 4 Schools (Arts & Literature, Trades & Technology, Health & Wellness, Business & Entrepreneurship)
- Multiple Departments per School
- Programs of Study with complete course listings
- Realistic course hours and structures

## 📁 Project Structure

```
WorkloadProject2025/
├── Components/
│   ├── Account/          # Authentication components
│   ├── Forms/            # Data entry forms
│   ├── Layout/           # Layout components (NavMenu, MainLayout)
│   └── Pages/            # All page components
├── Data/
│   ├── Models/           # Entity models
│   ├── ApplicationDbContext.cs
│   └── DbSeeder.cs       # Sample data seeding
├── Migrations/           # EF Core migrations
├── Services/             # Business logic services
└── wwwroot/              # Static assets
```

## 📋 Pages Available

### Main Pages
- **Home** - Dashboard with feature overview
- **School Dashboard** - Analytics for schools
- **Department Dashboard** - Department-level insights
- **Program Dashboard** - Program analytics

### Data Management
- **Schools** - Manage educational schools
- **Departments** - Department CRUD operations
- **Programs** - Program of Study management
- **Courses** - Course catalog management
- **Terms** - Academic term/semester management
- **Faculty** - Faculty profile management
- **Workload Categories** - Workload hour categories
- **Faculty Workload** - Track faculty assignments
- **Semester Courses** - Create full semester schedules

## 🔒 Security & Code Quality

- **Build Status:** ✅ Clean build with 0 warnings, 0 errors
- **Code Quality:** Fixed all nullable reference warnings
- **Migrations:** Proper naming conventions enforced
- **Authentication:** ASP.NET Core Identity integrated

## 🎯 Assignment Completion Status

### Part We Do Together ✅
- [x] Docker container configuration in appsettings
- [x] ID field removed from School form
- [x] All results shown on SchoolPage
- [x] Callback in School form for page refresh
- [x] School and Department linked with foreign keys
- [x] Migrations created and database updated
- [x] Department form includes School dropdown

### Part Students Do Themselves ✅
- [x] ID fields removed from ProgramOfStudyForm, CourseForm, TermForm
- [x] All pages show complete data (DepartmentPage, TermPage, CoursePage, ProgramPage)
- [x] Department and ProgramOfStudy linked
- [x] Models updated with relationships
- [x] Migrations run successfully
- [x] ProgramOfStudy form has Department dropdown
- [x] ProgramsOfStudy and Courses linked
- [x] All data can be added correctly
- [x] Faculty workload entity created with flexibility for:
  - Course lectures and labs (via Workload enum)
  - Coordinator duties
  - Special projects
  - Hours tracking and descriptions

## 🤝 Contributing

This is a student project for Medicine Hat College. All requirements from the assignment have been successfully implemented.

## 📝 License

Educational project for Medicine Hat College.

---

**Built with ❤️ using .NET 9 Blazor and MudBlazor**
