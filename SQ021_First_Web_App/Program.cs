using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQ021_First_Web_App.Data;
using SQ021_First_Web_App.Models.Entity;
using SQ021_First_Web_App.Services;
using SQ021_First_Web_App.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conStr = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<MyDbContext>(option => option.UseSqlServer(conStr));
builder.Services.AddIdentity<AppUser, IdentityRole>(
//    option =>
//{
//    option.Password.RequiredLength = 3;
//    option.Password.RequireNonAlphanumeric = false;
//}
).AddEntityFrameworkStores<MyDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDogRepositoryService, DogRepositoryService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Gallery}/{action=Index}/{id?}");

app.Run();
