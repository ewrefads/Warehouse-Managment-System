
using Microsoft.EntityFrameworkCore;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.DataFaking;
using Warehouse_Managemet_System.Driver;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.RowModels;
using Warehouse_Managemet_System.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Driver driver = new Driver(builder);

builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 30))
    ));
builder.Services.AddSingleton<Driver>(driver);
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    DataFileManager manager = new DataFileManager();
    DataFileGenerator fileG = new DataFileGenerator();
    DataGenerator gen = new DataGenerator();
    if (!Directory.Exists("DataFiles"))
    {
        Directory.CreateDirectory("DataFiles");
    }
    manager.CreateDataFiles(gen, fileG, "DataFiles");
    Context context = scope.ServiceProvider.GetRequiredService<Context>();
    Parser parser = new Warehouse_Managemet_System.Parsers.Parser();
    Seeder seeder = new Seeder(context, parser);
    seeder.PopulateTable<Product>("products.csv");
    seeder.PopulateTable<OrderItem>("orderItems.csv");
    seeder.PopulateTable<Order>("order.csv");
    seeder.PopulateTable<Transaction>("transaction.csv");
    seeder.PopulateTable<InventoryItem>("inventoryItem.csv");
    seeder.PopulateTable<Warehouse>("warehouse.csv");
    driver.SetUpDatabase(context);
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}


