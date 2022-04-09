using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcFilme.Models
{
    public class FilmeViewModel
    {
        public List<Filme> filmes;
        public string genero { get; set; }
        public string classificacao { get; set; }

        public static List<SelectListItem> generos { get; } = new List<SelectListItem>
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

        public static List<SelectListItem> Classificacoes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value="L", Text="Livre"},
            new SelectListItem { Value="10", Text="10" },
            new SelectListItem { Value="12", Text="12" },
            new SelectListItem { Value="14", Text="14" },
            new SelectListItem { Value="16", Text="16" },
            new SelectListItem { Value="18", Text="18" }
        };
    }
}
