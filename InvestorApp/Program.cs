
using Investor.Application.Services;
using Investor.Domain.Model;
using Investor.Infrastructure.Repository;
using InvestorApp.Utilities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace InvestorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(builder.Configuration)
                        .Enrich.FromLogContext()
                        .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            builder.Services.AddDependencies();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin() // Replace with your frontend URL
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
            var app = builder.Build();

            //Seed Data
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // Call the seed method
                DataSeeder.SeedData(context);
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAnyOrigin");
            app.MapControllers();

            app.Run();
        }
    }

    public static class Extension
    {

        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
               optionsBuilder => optionsBuilder.UseInMemoryDatabase("Investor.db"));

            services.AddScoped<IInvestorRepository, InvestorRepository>();
            services.AddScoped<IInvestorService, InvestorService>();
            services.AddScoped<ICommitmentService,CommitmentService>();
        }

    }
}
