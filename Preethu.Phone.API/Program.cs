
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Preethu.Phone.API.Database;
using Preethu.Phone.API.Repositories;
using Preethu.Phone.API.Controllers;

namespace Preethu.Phone.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ISmartPhoneRepository, SmartPhoneRepository>();
            builder.Services.AddScoped<ISpecificationRepository, SpecificationRepository>();
            builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition =
                    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SmartPhoneDbContext>(options =>
            {
                var ConnectionString = builder.Configuration.GetConnectionString("Default");
                options.UseSqlServer(ConnectionString);
            });
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<SmartPhoneDbContext>()
                .AddDefaultTokenProviders();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapIdentityApi<IdentityUser>();
            app.MapControllers();

            app.Run();
        }
    }
}
