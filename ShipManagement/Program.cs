using Microsoft.EntityFrameworkCore;

using ShipManagement.Repositories;
using ShipManagement.Repositories.Interfaces;
using ShipManagement.Services;
using ShipManagement.Services.Interfaces;

namespace ShipManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ShipManagementContext>(opt => opt.UseInMemoryDatabase("ShipManagementContext"));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            SetupDependencies(builder);
            SetupAutoMapper(builder.Services);

            var app = builder.Build();

            SeedDatabase(app);

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

        private static void SeedDatabase(WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var shipManagementContext = services.GetRequiredService<ShipManagementContext>();
                ShipContextSeed.SeedShips(shipManagementContext);
                ShipContextSeed.SeedPorts(shipManagementContext);
            }
        }

        private static void SetupAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private static void SetupDependencies(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ShipManagementContext>();
            builder.Services.AddTransient<IPortService, PortService>();
            builder.Services.AddTransient<IShipService, ShipService>();
            builder.Services.AddTransient<IShipRepository, ShipRepository>();
            builder.Services.AddTransient<IPortRepository, PortRepository>();
        }
    }
}