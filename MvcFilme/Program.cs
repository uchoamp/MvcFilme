using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MvcFilme.Data;
using MvcFilme.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MvcFilme
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var app = CreateWebHostBuilder(args).Build();
            await SeedData.Initialize(app.Services);
            await InitializeSelectsList(app.Services);
            app.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static async Task InitializeSelectsList(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                using (var context = new MvcFilmeContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<MvcFilmeContext>>()))
                {
                    await CinemasViewModel.UpdateCidades(context);
                }
            }
        }
    }
}
