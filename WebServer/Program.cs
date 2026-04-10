using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    var key = context.Request.Query["key"];

    if (key != "secret")
    {
      
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
        return; 
    }

   
    await next(context);
});

app.Use(async (context, next) =>
{
    Console.WriteLine($"[LOG]{context.Request.Method} {context.Request.Path}");
    await next();
    Console.WriteLine($"[LOG] Ответ отправлен:{context.Response.StatusCode}");
});

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Powered-By", "ASP.Net Core Lab 27");
    await next();
});


app.MapGet("/", () => "Добро пожаловать на сервер");
app.MapGet("/about", () => "Это мой ASP.NET core сервер");
app.MapGet("/time", () => $"Время на сервере: {DateTime.Now}");
app.MapGet("/hello/{name}", (string name) => $"Привет, {name}!");

app.MapGet("/student", () => new
{
    Name = "Микаилов Ахмед ",
    Group = "ИСП-231",
    Year = 3,
    IsActive = true
});

app.MapGet("/subjects", () => new[]
{
    "РПМ",
    "РМП",
    "ИСРПО",
    "СП",
});

app.MapGet("/product/{id}", (int id) => new Product(
    id: id,
    Name: $"Товар  #{id}",
    Price: id * 99.99m,
    InStock: id % 2 == 0
));

app.Run();

record Product(int id, string Name, decimal Price, bool InStock);