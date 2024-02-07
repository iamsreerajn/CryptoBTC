using CryptoBTC.Business;
using CryptoBTC.Data;
using CryptoBTC.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// allowed origins
var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("myAppCors", policy =>
    {
        policy.WithOrigins(allowedOrigin)
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});
//Serilogging - logs to an external file
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
            .WriteTo.File("Log/CryptoBTCLog.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//dbcontext configuration
builder.Services.AddDbContext<CryptoContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("CryptoConnection"));
});
//dependancy injection configuration
builder.Services.AddScoped<ICoinProvider, CoinProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseCors("myAppCors");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Crypto}/{action=Index}/{id?}");

app.Run();
