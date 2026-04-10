using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Net.Security;
using System.Security.Principal;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", () => "Привет от ИСП-233! Автор: <Селиванов Беляев>");

app.Use(async (context, next) =>
{
    Console.WriteLine($"[LOG] {context.Request.Method} {context.Request.Path}");
    await next(context);
    Console.WriteLine($"[LOG] Ответ отправлен: {context.Response.StatusCode}");
});

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Powered-By", "ASP.NET Core Lab27");
    await next(context);
});

app.MapGet("/", () => "Добро пожаловать на сервер!");

app.MapGet("/about", () => "Это мой первый ASP.NET Core сервер");

app.MapGet("/time", () => $"Время на сервере: {DateTime.Now}");

app.MapGet("/hello/{name}", (string name) => $"Привет, {name}!");

app.MapGet("/student", () => new
{
    Name = "Селиванов Беляев",
    Group = "ISP-233",
    Year = 3,
    IsActive = true
});

app.MapGet("/subjects", () => new[]
{
    "RPM",
    "RMP",
    "ISPRO",
    "SP",
});

app.MapGet("/product/{id}", (int id) => new Product(
    Id: id,
    Name: $"Товар #{id}",
    Price: id * 99.99m,
    InStock: id % 2 == 0
));

app.Run();

record Product(int Id, string Name, decimal Price, bool InStock);