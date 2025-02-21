using Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Zaliczenie.Middleware;
using Zaliczenie.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Register UniversityContext with DI using a SQLite database in LocalApplicationData.
builder.Services.AddDbContext<UniversityContext>(options =>
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    var db = System.IO.Path.Join(path, "university.db");
    options.UseSqlite($"Data Source={db}");
});

// Configure Identity to use UniversityContext.
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UniversityContext>();

// Register application services.
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IInstructorService, InstructorService>();
builder.Services.AddTransient<IEnrollmentService, EnrollmentService>();

// Add MVC controllers and Razor Pages.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add memory cache and session support.
builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Map Razor Pages and default controller routes.
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Add custom middleware for logging last visit.
app.UseMiddleware<LastVisitMiddleware>();

// Add custom middleware to log request details.
app.Use(async (context, next) =>
{
    var requestContent = new StringBuilder();
    requestContent.AppendLine("=== Request Info ===");
    requestContent.AppendLine($"method = {context.Request.Method.ToUpper()}");
    requestContent.AppendLine($"path = {context.Request.Path}");
    requestContent.AppendLine("-- headers");
    foreach (var (headerKey, headerValue) in context.Request.Headers)
    {
        requestContent.AppendLine($"header = {headerKey} value = {headerValue}");
    }
    requestContent.AppendLine("-- body");
    context.Request.EnableBuffering();
    var requestReader = new StreamReader(context.Request.Body);
    var content = await requestReader.ReadToEndAsync();
    requestContent.AppendLine($"body = {content}");
    Console.Write(requestContent.ToString());
    context.Request.Body.Position = 0;
    await next();
});

app.Run();
