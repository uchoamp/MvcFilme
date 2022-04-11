using Microsoft.AspNetCore.Mvc.Rendering;
using MvcFilme.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MvcFilme.Models
{
    public class CinemasViewModel
    {
        public static List<SelectListItem> Cidades { get; protected set; } = new List<SelectListItem>();
        public static List<SelectListItem> Estados { get; } = Enum.GetValues(typeof(UnidadesFederativas))
            .Cast<UnidadesFederativas>().Select(u => new SelectListItem { 
                Text = u.GetEnumDisplayName(),
                Value = ((int)u).ToString()})
            .ToList();


        [DisplayName("Cidade")]
        public string Cidade { get; set; }
        [DisplayName("Estado")]
        public string Estado { get; set; }

        public List<Cinema> Cinemas { get; set; }

        public static async Task UpdateCidades(MvcFilmeContext context)
        {
            Cidades = await context.Cinema.OrderBy(c => c.Cidade).Select(c => new SelectListItem
            {
                Text = c.Cidade,
                Value = c.Cidade
            }).Distinct().ToListAsync();
        }
    }
}
