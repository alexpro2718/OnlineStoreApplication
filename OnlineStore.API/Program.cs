using OnlineStore.API;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов ASP.NET Core
builder.Services.AddControllers();

// Генерация описания API
builder.Services.AddOpenApi();

var app = builder.Build();

// Собственный Middleware
app.UseMiddleware<RequestLoggingMiddleware>();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUi(options =>
    {
        options.Path = "/swagger";
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

// Маршруты контроллеров
app.MapControllers();

app.Run();
