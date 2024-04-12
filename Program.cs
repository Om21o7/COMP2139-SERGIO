using COMP2139_Labs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using COMP2139_Labs.Areas.ProjectManagement.Models;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


/*
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI()
    .AddDefaultTokenProviders();
*/
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



/*    
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
*/


// This ensures that whenever an IEmailSender is injected an instance of EmailSender is provided
//builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using var scop = app.Services.CreateScope();
var loogerFactory=scop.ServiceProvider.GetRequiredService<ILoggerFactory>();


try
{
    // get services needed  for role seeding
    //scope.serviceProvider -used to access instances of registered services
    var context = scop.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scop.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scop.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //seed roles 
    await ContextSeed.SeedRolesAsync(userManager, roleManager); 
    //seed superAdmin
    await ContextSeed.SuperSeedRoleAsync(userManager, roleManager);


}
catch (Exception e)
{
    var logger = loogerFactory.CreateLogger<Program>();
    logger.LogError(e, "An error occured when attempting to seed the roles for the system.");

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Project}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();