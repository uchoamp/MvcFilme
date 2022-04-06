using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcFilme.Models
{
    public class FilmeGeneroViewModel
    {
        public List<Filme> filmes;
        public SelectList generos;
        public string genero { get; set; }
        public string teste { get; set; }
    }
}
