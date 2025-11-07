using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using System.Globalization;
using System.Text;
using WorkloadProject2025.Components;
using WorkloadProject2025.Components.Account;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
 .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
// Services
builder.Services.AddScoped<SchoolService>();
builder.Services.AddScoped<IWorkloadCategoriesService, WorkloadCategoriesService>();
builder.Services.AddScoped<IWorkloadCalculationService, WorkloadCalculationService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<FacultyService>();
builder.Services.AddScoped<ProgramsOfStudyService>();
builder.Services.AddScoped<TermService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<FacultyWorkLoadService>();
builder.Services.AddScoped<WorkloadSummaryService>();
builder.Services.AddScoped<IWorkloadCopyService, WorkloadCopyService>();

builder.Services.AddAuthentication(options =>
{
 options.DefaultScheme = IdentityConstants.ApplicationScheme;
 options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
 options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
 .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<ApplicationDbContext>()
 .AddSignInManager()
 .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Seed the database and roles
using (var scope = app.Services.CreateScope())
{
 var services = scope.ServiceProvider;
 var context = services.GetRequiredService<ApplicationDbContext>();
 try
 {
 context.Database.Migrate();
 DbSeeder.SeedData(context);
 await RoleSeeder.EnsureRolesAsync(services);
 }
 catch (Exception ex)
 {
 var logger = services.GetRequiredService<ILogger<Program>>();
 logger.LogError(ex, "An error occurred while seeding the database.");
 }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
 app.UseMigrationsEndPoint();
}
else
{
 app.UseExceptionHandler("/Error", createScopeForErrors: true);
 // The default HSTS value is30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
 app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
 .AddInteractiveServerRenderMode();

// Minimal CSV report for instructors
app.MapGet("/api/reports/instructors", async (HttpContext http, ApplicationDbContext db) =>
{
 int? termId = null, schoolId = null, departmentId = null, programId = null, courseId = null;
 if (int.TryParse(http.Request.Query["termId"], out var termIdVal) && termIdVal >0) termId = termIdVal;
 if (int.TryParse(http.Request.Query["schoolId"], out var schoolIdVal) && schoolIdVal >0) schoolId = schoolIdVal;
 if (int.TryParse(http.Request.Query["departmentId"], out var departmentIdVal) && departmentIdVal >0) departmentId = departmentIdVal;
 if (int.TryParse(http.Request.Query["programId"], out var programIdVal) && programIdVal >0) programId = programIdVal;
 if (int.TryParse(http.Request.Query["courseId"], out var courseIdVal) && courseIdVal >0) courseId = courseIdVal;
 var yearsBack = (int.TryParse(http.Request.Query["yearsBack"], out var yb) ? yb :0);

 var q = db.FacultyWorkLoads
 .Include(w => w.Faculty)
 .Include(w => w.Course)
 .Include(w => w.ProgramOfStudy)!
 .ThenInclude(p => p!.Department)!
 .ThenInclude(d => d!.School)
 .Include(w => w.Term)
 .AsQueryable();

 if (termId.HasValue && yearsBack ==0) q = q.Where(w => w.TermId == termId.Value);
 if (yearsBack >0)
 {
 var cutoff = DateTime.Today.AddYears(-yearsBack);
 q = q.Where(w => w.Term != null && w.Term.StartDate >= cutoff);
 }
 if (schoolId.HasValue) q = q.Where(w => w.ProgramOfStudy != null && w.ProgramOfStudy.Department != null && w.ProgramOfStudy.Department.SchoolId == schoolId.Value);
 if (departmentId.HasValue) q = q.Where(w => w.ProgramOfStudy != null && w.ProgramOfStudy.DepartmentId == departmentId.Value);
 if (programId.HasValue) q = q.Where(w => w.ProgramOfStudyId == programId.Value);
 if (courseId.HasValue) q = q.Where(w => w.CourseId == courseId.Value);

 var items = await q.ToListAsync();

 static decimal Eff(FacultyWorkLoad i) => i.HoursAssigned * (i.PercentShare.HasValue ? (i.PercentShare.Value /100m) :1m);

 var rows = items
 .GroupBy(w => new { w.FacultyEmail, Name = (w.Faculty != null ? ($"{w.Faculty.FirstName} {w.Faculty.LastName}") : w.FacultyEmail) })
 .Select(g => new
 {
 Email = g.Key.FacultyEmail,
 Name = g.Key.Name,
 TotalHours = g.Sum(Eff),
 Assignments = g.Count(),
 HRPending = g.Count(i => !i.HRProcessed),
 Missing = g.Count(i => i.ProgramOfStudyId == null || i.CourseId == null)
 })
 .OrderByDescending(x => x.TotalHours)
 .ToList();

 var sb = new StringBuilder();
 void Csv(string s) => sb.Append('"').Append(s.Replace("\"", "\"\"")).Append('"');
 // header
 Csv("Email"); sb.Append(','); Csv("Name"); sb.Append(','); Csv("TotalHours"); sb.Append(','); Csv("Assignments"); sb.Append(','); Csv("HRPending"); sb.Append(','); Csv("MissingData"); sb.AppendLine();
 foreach (var r in rows)
 {
 Csv(r.Email); sb.Append(',');
 Csv(r.Name); sb.Append(',');
 sb.Append(r.TotalHours.ToString("0.##", CultureInfo.InvariantCulture)); sb.Append(',');
 sb.Append(r.Assignments); sb.Append(',');
 sb.Append(r.HRPending); sb.Append(',');
 sb.Append(r.Missing);
 sb.AppendLine();
 }

 var bytes = Encoding.UTF8.GetBytes(sb.ToString());
 http.Response.Headers.ContentDisposition = $"attachment; filename=InstructorReport_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv";
 return Results.File(bytes, "text/csv; charset=utf-8");
}).WithName("GetInstructorReport");

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();

public static class RoleSeeder
{
 public static async Task EnsureRolesAsync(IServiceProvider services)
 {
 var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
 string[] roles = ["Instructor", "Chair", "Administrator", "HR"];
 foreach (var role in roles)
 {
 if (!await roleManager.RoleExistsAsync(role))
 {
 await roleManager.CreateAsync(new IdentityRole(role));
 }
 }
 }
}
