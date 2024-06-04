using App.DAL.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App.Tests.IntegrationTests;

public class CustomWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // builder.ConfigureServices(services =>
        // {
        //     var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
        //
        //     if (descriptor != null)
        //     {
        //         services.Remove(descriptor);
        //     }
        //
        //     // Using an in-memory database for testing
        //     services.AddDbContext<AppDbContext>(options =>
        //     {
        //         options.UseInMemoryDatabase("InMemoryTestDb");
        //     });
        // });
        //
        // // Ensure the factory client doesn't use a running server
        // builder.ConfigureTestServices(services =>
        // {
        //     services.AddHttpClient<CustomWebAppFactory<TStartup>>().ConfigureHttpClient(client =>
        //     {
        //         client.BaseAddress = new Uri("http://localhost/");
        //     });
        // });
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebAppFactory<TStartup>>>();

            db.Database.EnsureCreated();
            
            
        });
    }
}
