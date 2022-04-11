using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFilme.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilme.Models
{
    public class FilmesViewModel
    {
        public List<Filme> Filmes { get; set; }

        [DisplayName("Gênero")]
        public string Genero { get; set; }

        [DisplayName("Classificação")]
        public Classificacoes? Classificacao { get; set; }

        [DisplayName("Ano de Lançamento")]
        public int AnoLancamento { get; set; }

        [DisplayName("Busca por Titulo")]
        public string BuscaTitulo { get; set; }
        public List<SelectListItem> AnosLancamento { get; private set; }


        public static List<SelectListItem> Generos { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value="Ação", Text="Ação"},
            new SelectListItem { Value="Aventura", Text="Aventura"},
            new SelectListItem { Value="Comédia", Text="Comédia"},
            new SelectListItem { Value="Comédia de ação", Text="Comédia de ação"},
            new SelectListItem { Value="Comédia dramática", Text="Comédia dramática"},
            new SelectListItem { Value="Comédia romântica", Text="Comédia romântica"},
            new SelectListItem { Value="Dança", Text="Dança"},
            new SelectListItem { Value="Documentário", Text="Documentário"},
            new SelectListItem { Value="Drama", Text="Drama"},
            new SelectListItem { Value="Espionagem", Text="Espionagem"},
            new SelectListItem { Value="Faroeste", Text="Faroeste"},
            new SelectListItem { Value="Fantasia", Text="Fantasia"},
            new SelectListItem { Value="Ficção científica", Text="Ficção científica"},
            new SelectListItem { Value="Guerra", Text="Guerra"},
            new SelectListItem { Value="Mistério", Text="Mistério"},
            new SelectListItem { Value="Musical", Text="Musical"},
            new SelectListItem { Value="Policial", Text="Policial"},
            new SelectListItem { Value="Romance", Text="Romance"},
            new SelectListItem { Value="Terror", Text="Terror"},
            new SelectListItem { Value="Thriller", Text="Thriller"}
        };
        public static List<SelectListItem> Classificacoes { get; } = Enum.GetValues(typeof(Classificacoes))
            .Cast<Classificacoes>().Select(c => new SelectListItem
            {
                Text = c.GetEnumDisplayName(),
                Value = ((int)c).ToString()
            })
            .ToList();

        public async Task SetSelectListItems(MvcFilmeContext context)
        {
            AnosLancamento = await context.Filme.Select(f => f.Lancamento.Year).Distinct()
                .Select(c => new SelectListItem { 
                    Text = c.ToString(),
                    Value = c.ToString()
                }).ToListAsync();
        }

    }
}
