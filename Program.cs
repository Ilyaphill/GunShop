using GunShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GunShop.Models;

var builder = WebApplication.CreateBuilder(args);

// Настройка контекста базы данных
builder.Services.AddDbContext<GunShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GunShopContext") ?? throw new InvalidOperationException("Connection string 'GunShopContext' not found.")));

// Добавление сервисов в контейнер
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Создание или обновление базы данных при запуске
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GunShopContext>();

    // Применение миграций для создания или обновления базы данных
    context.Database.Migrate();

    // Инициализация начальных данных
    SeedData.Initialize(services);
}

// Конфигурация HTTP запросов
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
