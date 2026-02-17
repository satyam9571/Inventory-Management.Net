using Microsoft.AspNetCore.Authentication.Cookies;
using StoreManagement.BAL.Adapter;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DapperContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
        options.LogoutPath = "/Home/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

// Dependency Injection
builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<IAuthentication, AuthenticationAdapter>();
builder.Services.AddSingleton<StoreManagement.Common.DapperContext.DbContext>();
builder.Services.AddScoped<ICategoryRepository, CategoryAdapter>();
builder.Services.AddScoped<IBrandRepository, BrandAdapter>();
builder.Services.AddScoped<IProductRepository, ProductAdapter>();
builder.Services.AddScoped<IStockEntryRepository, StockEntryAdapter>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
