
using Generalizacija.Data;
using Generalizacija.Entity;
using Microsoft.EntityFrameworkCore;

namespace Generalizacija
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<GeneralizacijaContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("KalendarDB")));

            builder.Services.AddScoped<IIndSastanakRepository, IndSastanakRepository>();
            builder.Services.AddScoped<ISastanakRepository, SastanakRepository>();
            builder.Services.AddScoped<ITimskiSastanakRepository, TimskiSastanakRepository>();

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var app = builder.Build();

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
    }
}
