using FitnessClub.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// В Docker строка подключения берётся из переменной окружения
// ConnectionStrings__DefaultConnection (задана в docker-compose.yml)
// При локальном запуске — из appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new() { Title = "FitnessClub API", Version = "v1" }));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Swagger доступен всегда — и локально и в Docker
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Авто-миграция при старте — нужна в Docker т.к. БД создаётся с нуля
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    Console.WriteLine("Миграция выполнена успешно.");
}
catch (Exception ex)
{
    Console.WriteLine($"Миграция пропущена: {ex.Message}");
}

app.Run();
