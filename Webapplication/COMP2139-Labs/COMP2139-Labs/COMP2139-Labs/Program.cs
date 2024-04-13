using Microsoft.EntityFrameworkCore;
using COMP2139_Labs.Models;
using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using COMP2139_Labs.Services;
using COMP2139_Labs.Areas.ProjectManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36)))); // Adjust the version as necessary



builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().
    AddDefaultUI()
    .AddDefaultTokenProviders();



builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();





//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

// this is that whenever an IEmailSender is injected an instance of EmailSender is provided
builder.Services.AddSingleton<IEmailSender, EmailSender>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithRedirects("/Home/NotFound?statusCode={0}");
}

//LAB 10
using var scope = app.Services.CreateScope();
var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

try
{
    //get service needed for role seeding
    //scope.serviceprovider - used to access instances of registered services

    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    //seed roles
    await ContextSeed.SeedRolesAsync(userManager, roleManager);

    //seed superAdmin
    await ContextSeed.SuperSeedRoleAsync(userManager, roleManager);

}
catch (Exception e)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(e, "An Error occurred when attempting to seed the roles for the System");
}
//END OF LAB 10



app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

// Add the code to log database connection information
using (scope)
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var connectionString = dbContext.Database.GetConnectionString();
    app.Logger.LogInformation($"Connected to database: {connectionString}");
}

// Correct area routing configuration
app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
