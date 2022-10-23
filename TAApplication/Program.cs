/**
  Author:    Nasser Mughrabi
  Partner:   None   
  Date:      13-October-2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Nasser Mughrabi - This work may not be copied for use in Academic Coursework.
  I, Nasser Mughrabi, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source. Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This class is the first commands of the whole project/program
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TAApplication.Areas.Data;
using TAApplication.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<TAUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// add policy for u0000000
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("oneStudent", policyBuilder =>
        policyBuilder.RequireAssertion(context =>
        context.User.IsInRole("Administrator")
        || context.User.IsInRole("Professor")
        || context.User.Identity.Name == "u0000000"
        )
    );
});


builder.Services.AddAuthentication()
.AddGoogle(options =>
{
    IConfigurationSection googleAuthNSection =
    builder.Configuration.GetSection("Authentication:Google");

    options.ClientId = googleAuthNSection["ClientId"];
    options.ClientSecret = googleAuthNSection["ClientSecret"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    // database won't connect correctly without this 
    var services = scope.ServiceProvider;
    var context =
       services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    var DB = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var um = scope.ServiceProvider.GetRequiredService<UserManager<TAUser>>();
    var rm = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var us = scope.ServiceProvider.GetRequiredService<IUserStore<TAUser>>();

    await DB.InitializeUsers(um, rm, us);
    await DB.InitializeApplications(um);
    await DB.InitializeCourses(um);
    
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
