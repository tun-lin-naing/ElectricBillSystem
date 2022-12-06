using ElectricBillWebApp.Data;
using ElectricBillWebApp.Interface;
using ElectricBillWebApp.Repository;
using ElectricBillWebApp.Utils;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.Configure<FormOptions>(x =>
{
    x.MultipartBodyLengthLimit = int.MaxValue; // Limit on form body size
});

builder.Services.AddDbContext<EBSDBContext>(option =>
    option.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"),
        sqlServerOptions => sqlServerOptions.CommandTimeout(60)
    )
);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
//builder.Services.AddScoped<IRFIDTaggingRepository, RFIDTaggingRepository>();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

LoggerOptions loggerOptions = new();
builder.Configuration.GetSection("Logging").GetSection("CustomLogger").GetSection("Options").Bind(loggerOptions);
builder.Services.AddSingleton(loggerOptions);
builder.Services.AddSingleton<Logger>();

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
