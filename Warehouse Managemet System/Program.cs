/*using Warehouse_Managemet_System.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 30))
    ));

var app = builder.Build();*/



// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
