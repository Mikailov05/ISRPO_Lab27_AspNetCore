using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Добро пожаловать на сервер");
app.MapGet("/about", () => "Это мой ASP.NET core сервер");
app.MapGet("/time", () => "Время на сервере: {DateTime.Now}");
app.MapGet("/hello/{name}", (string name) => $"Привет,{name}!");

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
