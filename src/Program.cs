using Microsoft.EntityFrameworkCore;
using QuanLyDatPhong_UNeTI5_TI17A1HN.Repositories;
using QuanLyDatPhong_UNETI5_TI17A1HN.Models;


using QuanLyDatPhong_UNETI5_TI17A1HN.Repositories;

var builder = WebApplication.CreateBuilder(args);

var dbDir = Path.Combine(builder.Environment.ContentRootPath, "bin", "Debug", "net10.0");
AppDomain.CurrentDomain.SetData("DataDirectory", dbDir);

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext")
    ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.");

if (connectionString.Contains("|DataDirectory|"))
{
    connectionString = connectionString.Replace("|DataDirectory|", dbDir.TrimEnd('\\', '/'));
}

builder.Services.AddDbContext<QuanLyDatPhong_UNETI5_TI17A1HN.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPhongRepository, PhongRepository>();
builder.Services.AddScoped<IThongkeRepository, ThongKeRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<ILeTanRepository, LeTanRepository>();
builder.Services.AddScoped<ILeTanDatPhongRepository, LeTanRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
