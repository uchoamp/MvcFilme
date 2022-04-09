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
                            Capa = "https://br.web.img3.acsta.net/pictures/17/01/09/12/22/442219.jpg",
                            Lancamento = DateTime.Parse("2017-3-16"),
                            Genero = "Fantasia/Romance",
                            Classificacao = "L",
                            Sinopse = "Em A Bela e a Fera, moradora de uma pequena aldeia francesa, Bela (Emma Watson) tem o pai capturado pela Fera (Dan Stevens) e decide entregar sua vida ao estranho ser em troca da liberdade dele. No castelo, ela conhece objetos mágicos e descobre que a Fera é, na verdade, um príncipe que precisa de amor para voltar à forma humana." 
                        },
                        new Filme
                        {
                            Titulo = "Django Livre",
                            Capa = "https://br.web.img2.acsta.net/medias/nmedia/18/90/07/53/20391069.jpg",
                            Lancamento = DateTime.Parse("2012-1-18"),
                            Genero = "Faroeste",
                            Classificacao = "16",
                            Sinopse = "Django (Jamie Foxx) é um escravo liberto cujo passado brutal com seus antigos proprietários leva-o ao encontro do caçador de recompensas alemão Dr. King Schultz (Christoph Waltz). Schultz está em busca dos irmãos assassinos Brittle, e somente Django pode levá-lo a eles. O pouco ortodoxo Schultz compra Django com a promessa de libertá-lo quando tiver capturado os irmãos Brittle, vivos ou mortos."
                        }
                        );

                    context.SaveChanges();
                }
            }
        }
    }
}
