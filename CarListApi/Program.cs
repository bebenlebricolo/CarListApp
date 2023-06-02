
using CarListApi.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CarListApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = "v1",
                        Title = " ",
                        Description = "Test api for Car List application client project"
                    });
                });
            builder.Services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", a => a.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });

            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dbPath = Path.Combine("C:\\CarListApi\\", "carlist.db");
            var conn = new SqliteConnection($"Data Source={dbPath}");
            builder.Services.AddDbContext<CarListDbContext>(o => o.UseSqlite(conn));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // Swagger enable for everyone 
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}