/*
 * ECommerce WebApplication which provides in easy shopping of products 
   and it is a user-friendly application
*/

using E_Commerce_WebApplication;
using E_Commerce_WebApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.UnitOfWork;
using E_Commerce_WebApplication.FactoryPattern;
using E_Commerce_WebApplication.Utilities;
using E_Commerce_WebApplication.Repositories.Interfaces;
using E_Commerce_WebApplication.Repositories.Providers;
using Microsoft.AspNetCore.Identity;
using E_Commerce_WebApplication.Models;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Connection String 
builder.Services.AddDbContext<ECommerceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDbContext")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});


// Adding services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICheckoutRepository, CheckoutRepository>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped <IUserIdUtility,UserIdUtility>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


// Enable EF Core logging
builder.Services.AddDbContext<ECommerceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDbContext"))
           .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // If needed, customize JSON serialization options
});


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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
