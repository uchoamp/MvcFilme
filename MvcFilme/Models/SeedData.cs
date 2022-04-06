using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcFilme.Data;
using System;
using System.Threading.Tasks;

namespace MvcFilme.Models
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                using (var context = new MvcFilmeContext(
                    scope.ServiceProvider.GetRequiredService<DbContextOptions<MvcFilmeContext>>()))
                {
                    if ( await context.Filme.AnyAsync())
                        return;
                    context.Filme.AddRange(
                        new Filme
                        {
                            Titulo = "A Bela e a Fera",
                            Lancamento = DateTime.Parse("2017-3-16"),
                            Genero = "Fantasia/Romance",
                            Preco = 7.99M,
                            Classificacao = "L"
                        },
                        new Filme
                        {
                            Titulo = "Django Livre",
                            Lancamento = DateTime.Parse("2012-1-18"),
                            Genero = "Faroeste",
                            Preco = 12.99M,
                            Classificacao = "16"
                        }
                        );

                    context.SaveChanges();
                }
            }
        }
    }
}
