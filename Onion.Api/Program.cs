using Ecommerce.Reposatory.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onion.Api.Extensions;
using Onion.Api.Helper;
using Onion.Api.Middlewares;
using Onion.Repository.Data;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddEndpointsApiExplorer();

        // Add Swagger
        builder.Services.AddSwaggerGen();

        // Add ProductPictureUrlResolver
        builder.Services.AddScoped<ProductPictureUrlResolver>();

        //Add Connection String
        builder.Services.AddDbContext<OnionContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        // Add Extension Methods
        builder.Services.AddApplicationServices();

        var app = builder.Build();

        // Update database without Console & Seed the database
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var dbContext = services.GetRequiredService<OnionContext>();
            // Apply migrations
            await dbContext.Database.MigrateAsync();

            // Seed the database
            await OnionContextSeed.SeedAsync(dbContext);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred while migrating the database OR seeding it");
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMiddleware<ExceptionMiddleWare>();
            app.UseSwaggerMiddlewares();
        }

        // Add static files
        app.UseStaticFiles();

        //use Status Code
        app.UseStatusCodePagesWithRedirects("/errors/{0}");

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}