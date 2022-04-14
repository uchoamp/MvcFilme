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
                    if (!await context.Filme.AnyAsync())
                    {

                        context.Filme.AddRange(
                            new Filme
                            {
                                Titulo = "A Bela e a Fera",
                                Capa = "https://br.web.img3.acsta.net/pictures/17/01/09/12/22/442219.jpg",
                                Lancamento = DateTime.Parse("2017-3-16"),
                                Genero = GenerosFilme.ROMANCE,
                                Classificacao = Classificacoes.CL,
                                Sinopse = "Em A Bela e a Fera, moradora de uma pequena aldeia francesa, Bela (Emma Watson) tem o pai capturado pela Fera (Dan Stevens) e decide entregar sua vida ao estranho ser em troca da liberdade dele. No castelo, ela conhece objetos mágicos e descobre que a Fera é, na verdade, um príncipe que precisa de amor para voltar à forma humana."
                            },
                            new Filme
                            {
                                Titulo = "Django Livre",
                                Capa = "https://br.web.img2.acsta.net/medias/nmedia/18/90/07/53/20391069.jpg",
                                Lancamento = DateTime.Parse("2012-1-18"),
                                Genero = GenerosFilme.FAROESTE,
                                Classificacao = Classificacoes.C16,
                                Sinopse = "Django (Jamie Foxx) é um escravo liberto cujo passado brutal com seus antigos proprietários leva-o ao encontro do caçador de recompensas alemão Dr. King Schultz (Christoph Waltz). Schultz está em busca dos irmãos assassinos Brittle, e somente Django pode levá-lo a eles. O pouco ortodoxo Schultz compra Django com a promessa de libertá-lo quando tiver capturado os irmãos Brittle, vivos ou mortos."
                            },
                            new Filme
                            {
                                Titulo = "Interstelar",
                                Capa = "https://br.web.img3.acsta.net/c_310_420/pictures/14/10/31/20/39/476171.jpg",
                                Lancamento = DateTime.Parse("2014-11-18"),
                                Genero = GenerosFilme.FICCAO_CIENTIFICA,
                                Classificacao = Classificacoes.C10,
                                Sinopse = "Após ver a Terra consumindo boa parte de suas reservas naturais, um grupo de astronautas recebe a missão de verificar possíveis planetas para receberem a população mundial, possibilitando a continuação da espécie. Cooper (Matthew McConaughey) é chamado para liderar o grupo e aceita a missão sabendo que pode nunca mais ver os filhos. Ao lado de Brand (Anne Hathaway), Jenkins (Marlon Sanders) e Doyle (Wes Bentley), ele seguirá em busca de uma nova casa. Com o passar dos anos, sua filha Murph (Mackenzie Foy e Jessica Chastain) investirá numa própria jornada para também tentar salvar a população do planeta."
                            },
                            new Filme
                            {
                                Titulo = "Mad Max: Estrada da Fúria",
                                Capa = "https://br.web.img3.acsta.net/c_310_420/pictures/15/03/26/21/14/132057.jpg",
                                Lancamento = DateTime.Parse("2014-3-7"),
                                Genero = GenerosFilme.ACAO,
                                Classificacao = Classificacoes.C16,
                                Sinopse = "Após ser capturado por Immortan Joe, um guerreiro das estradas chamado Max (Tom Hardy) se vê no meio de uma guerra mortal, iniciada pela Imperatriz Furiosa (Charlize Theron) na tentativa se salvar um grupo de garotas. Também tentanto fugir, Max aceita ajudar Furiosa em sua luta contra Joe e se vê dividido entre mais uma vez seguir sozinho seu caminho ou ficar com o grupo."
                            },
                            new Filme
                            {
                                Titulo = "O Poderoso Chefão",
                                Capa = "https://br.web.img3.acsta.net/c_310_420/medias/nmedia/18/90/93/20/20120876.jpg",
                                Lancamento = DateTime.Parse("1972-5-14"),
                                Genero = GenerosFilme.DRAMA,
                                Classificacao = Classificacoes.C16,
                                Sinopse = "Don Vito Corleone (Marlon Brando) é o chefe de uma \"família\" de Nova York que está feliz, pois Connie (Talia Shire), sua filha, se casou com Carlo (Gianni Russo). Porém, durante a festa, Bonasera (Salvatore Corsitto) é visto no escritório de Don Corleone pedindo \"justiça\", vingança na verdade contra membros de uma quadrilha, que espancaram barbaramente sua filha por ela ter se recusado a fazer sexo para preservar a honra."
                            },
                            new Filme
                            {
                                Titulo = "Batam: O Cavaleiro das Trevas",
                                Capa = "https://br.web.img3.acsta.net/c_310_420/medias/nmedia/18/86/98/32/19870786.jpg",
                                Lancamento = DateTime.Parse("2008-7-18"),
                                Genero = GenerosFilme.ACAO,
                                Classificacao = Classificacoes.C14,
                                Sinopse = "Após dois anos desde o surgimento do Batman (Christian Bale), os criminosos de Gotham City têm muito o que temer. Com a ajuda do tenente James Gordon (Gary Oldman) e do promotor público Harvey Dent (Aaron Eckhart), Batman luta contra o crime organizado. Acuados com o combate, os chefes do crime aceitam a proposta feita pelo Coringa (Heath Ledger) e o contratam para combater o Homem-Morcego.",
                            },
                            new Filme
                            {
                                Titulo = "Sonic 2",
                                Capa = "https://br.web.img3.acsta.net/c_310_420/pictures/21/12/08/15/46/3923761.jpg",
                                Lancamento = DateTime.Parse("2022-4-7"),
                                Genero = GenerosFilme.FICCAO_CIENTIFICA,
                                Classificacao = Classificacoes.C10,
                                Sinopse = "Sonic 2 - O Filme é uma sequência dos acontecimentos do primeiro live-action de Sonic - O Filme, baseado no videogame de sucesso. Após conseguir se estabelecer em Green Hills, Sonic está pronto para mais liberdade e quer provar que tem o necessário para ser um herói de verdade. Seu teste virá quando Tom e Maddie concordam em deixá-lo em casa enquanto saem de férias para ir ao casamento da irmã de Rachel, no Havaí. Mas para a infelicidade do ouriço, a data acaba coincidindo com o retorno do Dr. Robotnik, dessa vez com um novo parceiro, Knuckles, depois que o doutor do mal vai embora para o planeta cogumelo. O terrível Robotnik está à procura de uma esmeralda com o poder de destruir civilizações. Sonic se une a um novo companheiro, Tails, e juntos eles embarcam em uma jornada para encontrar a esmeralda antes que ela caia nas mãos erradas."
                            },
                            new Filme
                            {
                                Titulo = "Guardiões da Galáxia",
                                Capa = "https://br.web.img2.acsta.net/c_310_420/pictures/14/06/03/21/11/122582.jpg",
                                Lancamento = DateTime.Parse("2014-7-31"),
                                Genero = GenerosFilme.FICCAO_CIENTIFICA,
                                Classificacao = Classificacoes.C12,
                                Sinopse = "Peter Quill (Chris Pratt) foi abduzido da Terra quando ainda era criança. Adulto, fez carreira como saqueador e ganhou o nome de Senhor das Estrelas. Quando rouba uma esfera, na qual o poderoso vilão Ronan, da raça kree, está interessado, passa a ser procurado por vários caçadores de recompensas. Para escapar do perigo, Quill une forças com quatro personagens fora do sistema: Groot, uma árvore humanóide (Vin Diesel), a sombria e perigosa Gamora (Zoe Saldana), o guaxinim rápido no gatilho Rocket Racoon (Bradley Cooper) e o vingativo Drax, o Destruidor (Dave Bautista). Mas o Senhor das Estrelas descobre que a esfera roubada possui um poder capaz de mudar os rumos do universo, e logo o grupo deverá proteger o objeto para salvar o futuro da galáxia."
                            },
                            new Filme
                            {
                                Titulo = "Eu Sou a Lenda",
                                Capa = "https://br.web.img3.acsta.net/c_310_420/medias/nmedia/18/91/08/87/20128900.jpg",
                                Lancamento = DateTime.Parse("2007-1-18"),
                                Genero = GenerosFilme.FICCAO_CIENTIFICA,
                                Classificacao = Classificacoes.C16,
                                Sinopse = "Um terrível vírus incurável, criado pelo homem, dizimou a população de Nova York. Robert Neville (Will Smith) é um cientista brilhante que, sem saber como, tornou-se imune ao vírus. Há 3 anos ele percorre a cidade enviando mensagens de rádio, na esperança de encontrar algum sobrevivente. Robert é sempre acompanhado por vítimas mutantes do vírus, que aguardam o momento certo para atacá-lo. Paralelamente ele realiza testes com seu próprio sangue, buscando encontrar um meio de reverter os efeitos do vírus"
                            },
                            new Filme
                            {
                                Titulo = "Bad Boys Para Sempre",
                                Capa = "https://br.web.img3.acsta.net/c_310_420/pictures/20/01/28/22/11/0608065.jpg",
                                Lancamento = DateTime.Parse("2020-1-30"),
                                Genero = GenerosFilme.POLICIAL,
                                Classificacao = Classificacoes.C16,
                                Sinopse = "Terceiro episódio das histórias dos policiais Burnett (Martin Lawrence) e Lowrey (Will Smith), que devem encontrar e prender os mais perigosos traficantes de drogas da cidade."
                            },
                            new Filme
                            {
                                Titulo = "Homem Aranha: Sem Volta Para Casa",
                                Capa = "https://br.web.img3.acsta.net/c_310_420/pictures/21/11/08/16/02/3963914.png",
                                Lancamento = DateTime.Parse("2021-12-16"),
                                Genero = GenerosFilme.AVENTURA,
                                Classificacao = Classificacoes.C12,
                                Sinopse = "Em Homem-Aranha: Sem Volta para Casa, Peter Parker (Tom Holland) precisará lidar com as consequências da sua identidade como o herói mais querido do mundo após ter sido revelada pela reportagem do Clarim Diário, com uma gravação feita por Mysterio (Jake Gyllenhaal) no filme anterior. Incapaz de separar sua vida normal das aventuras de ser um super-herói, além de ter sua reputação arruinada por acharem que foi ele quem matou Mysterio e pondo em risco seus entes mais queridos, Parker pede ao Doutor Estranho (Benedict Cumberbatch) para que todos esqueçam sua verdadeira identidade. Entretanto, o feitiço não sai como planejado e a situação torna-se ainda mais perigosa quando vilões de outras versões de Homem-Aranha de outro universos acabam indo para seu mundo. Agora, Peter não só deter vilões de suas outras versões e fazer com que eles voltem para seu universo original, mas também aprender que, com grandes poderes vem grandes responsabilidades como herói."
                            },
                            new Filme
                            {
                                Titulo = "Velozes e Furiosos 9",
                                Capa = "https://br.web.img3.acsta.net/c_310_420/pictures/21/04/14/19/06/3385237.jpg",
                                Lancamento = DateTime.Parse("2021-6-24"),
                                Genero = GenerosFilme.ACAO,
                                Classificacao = Classificacoes.C14,
                                Sinopse = "Em Velozes & Furiosos 9, dois anos após o confronto com a ciber-terrorista Cipher, Dominic Toretto (Vin Diesel) - agora aposentado - e Letty (Michelle Rodriguez) vivem uma vida pacata ao lado de seu filho Brian. Mas a vida dos dois é logo interrompida quando Roman Pearce, Tej Parker e Ramsey chegam com notícias de que, pouco depois de prender Cipher (Charlize Theron), o avião de Mr. Nobody foi atacado por agentes e sequestraram Cipher, precisando da ajuda dele para investigar mais afundo. Acompanhando eles em uma missão, o grupo logo acha nos escombros parte de um dispositivo chamado Projeto Aries. A calmaria é dissipada quando o irmão desaparecido de Dom retorna e rouba o dispositivo deles com um grupo altamente treinado. Jakob (John Cena), um assassino habilidoso e excelente motorista, está trabalhando ao lado de Cipher. Para enfrentá-los, Toretto vai precisar reunir sua equipe novamente, inclusive Han (Sung Kang), que todos acreditavam estar morto."
                            },
                            new Filme
                            {
                                Titulo = "O Esquadrão Suicida",
                                Capa = "https://br.web.img2.acsta.net/c_310_420/pictures/21/06/22/22/19/1495362.jpg",
                                Lancamento = DateTime.Parse("2021-8-5"),
                                Genero = GenerosFilme.AVENTURA,
                                Classificacao = Classificacoes.C16,
                                Sinopse = "Liderados por Sanguinário (Idris Elba), Pacificador (John Cena), Coronel Rick Flag (Joel Kinnaman), e pela psicopata favorita de todos, Arlequina (Margot Robbie), o Esquadrão Suicida está disposto a fazer qualquer coisa para escapar da prisão. Armados até os dentes e rastreados pela equipe de Amanda Waller (Viola Davis), eles são jogados (literalmente) na remota ilha Corto Maltese, repleta de militantes adversários e forças de guerrilha. O grupo de supervilões busca destruição, mas basta um movimento errado para que acabem mortos."
                            }
                            );

                        await context.SaveChangesAsync();
                    }
                    if (!await context.Cinema.AnyAsync())
                    {

                        context.Cinema.AddRange(
                            new Cinema
                            {
                                Nome = "Cinemark",
                                Cidade = "Foz Iguaçu",
                                UnidadeFederativa = UnidadesFederativas.PR,
                                Telefone = "(74) 3614-8860"
                            },
                            new Cinema
                            {
                                Nome = "Cinemas Teresina",
                                Cidade = "Teresina",
                                UnidadeFederativa = UnidadesFederativas.PI,
                                Telefone = "(86) 3233-9278"
                            }
                            );

                        await context.SaveChangesAsync();
                    }
                    if (!await context.Cartaz.AnyAsync())
                    {
                        var filmes = await context.Filme.ToListAsync();
                        var cinemas = await context.Cinema.ToListAsync();
                        foreach (var f in filmes)
                            foreach (var c in cinemas)
                                context.Add(new Cartaz
                                {
                                    CinemaId = c.Id,
                                    FilmeId = f.Id,
                                    InicioExibicao = DateTime.Now,
                                    Preco = 7.98M,
                                    FimExibicao = DateTime.Now.AddDays(4)
                                });
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
