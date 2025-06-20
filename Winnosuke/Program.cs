// Program.cs - Hoàn ch?nh c?u hình h? th?ng MyGarage
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;
using Winnosuke.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. K?t n?i Database
var connectionString = builder.Configuration.GetConnectionString("MyGarageFinalConnection")
    ?? "Server=localhost;Database=MyGarageFinal;Trusted_Connection=True;"; // fallback
builder.Services.AddDbContext<MyGarageFinalContext>(options =>
    options.UseSqlServer(connectionString));

// 2. ??ng ký DI Services

builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ITechnicalReportService, TechnicalReportService>();
builder.Services.AddScoped<IGarageService, GarageService>();
builder.Services.AddScoped<IRepairStatusService, RepairStatusService>();
builder.Services.AddScoped<IAdminActivityService, AdminActivityService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// 3. ??ng ký HttpContext và Session
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 4. Xác th?c ng??i dùng (Cookie + Google OAuth)
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/AccountLogin/Login";
    options.LogoutPath = "/AccountLogin/Logout";
    options.AccessDeniedPath = "/AccountLogin/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "your-client-id";
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "your-client-secret";
    options.CallbackPath = "/signin-google";
});

// 5. MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 6. Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// 7. Route M?c ??nh
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
