
using AppViajesWirsolut.Context;
using AppViajesWirsolut.Services;
using Microsoft.EntityFrameworkCore;

namespace AppViajesWirsolut
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDB"))
            );


            // Add services to the container.
            builder.Services.AddHttpClient<IClimaService, ClimaService>();
            builder.Services.AddControllers();




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Configuración de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // La URL de tu frontend React
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowLocalhost");  // Nombre de la política que creaste
            app.MapControllers();

            app.Run();
        }
    }
}
