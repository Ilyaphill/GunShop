using GunShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GunShop.Models;

var builder = WebApplication.CreateBuilder(args);

// ��������� ��������� ���� ������
builder.Services.AddDbContext<GunShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GunShopContext") ?? throw new InvalidOperationException("Connection string 'GunShopContext' not found.")));

// ���������� �������� � ���������
builder.Services.AddControllersWithViews();

var app = builder.Build();

// �������� ��� ���������� ���� ������ ��� �������
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GunShopContext>();

    // ���������� �������� ��� �������� ��� ���������� ���� ������
    context.Database.Migrate();

    // ������������� ��������� ������
    SeedData.Initialize(services);
}

// ������������ HTTP ��������
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Katalog}/{action=Index}/{id?}");

app.Run();
