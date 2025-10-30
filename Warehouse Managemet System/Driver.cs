using System;
using Microsoft.EntityFrameworkCore;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.RowModels;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.Seeders;

namespace Warehouse_Managemet_System
{
    public class Driver
    {
        private Context _context;
        private WebApplication _app;
        private IServiceScope _scope;
        private WebApplicationBuilder _builder;

        public Driver(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Context>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 30))
                ));

            _app = builder.Build();

            using (_scope = _app.Services.CreateScope())
            {
                _context = _scope.ServiceProvider.GetRequiredService<Context>();
                Parser parser = new Parser();
                Seeder seeder = new Seeder(_context, parser);
            }
                
            _app.UseHttpsRedirection();
            _app.Run();
        }
    }
}