
using Microsoft.EntityFrameworkCore;
using VendorManagementProject.DataBase;
using VendorManagementProject.Middleware;
using VendorManagementProject.Services.Class;
using VendorManagementProject.Services.Interface;
using VendorManagementProject.Services.Interfaces;

namespace VendorManagementProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Register DbContext with SQL Server
            builder.Services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBSC")));

            // Register Redis Cache
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration["Redis"];
            });

            
            builder.Services.AddScoped<IVendorRepository, VendorRepository>();
            builder.Services.AddScoped<IVendorService, VendorService>();
            builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();

           
            builder.Services.AddControllers();

            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            app.Run();



        }
    }
}


