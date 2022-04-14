using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using MvcFilme.Data;
using MvcFilme.Utils;

namespace MvcFilme.Models
{

    public class FilmeViewModel
    {

        [DisplayName("Filme")]
        public Filme Filme { get; set; }
        public List<Cartaz> Cartazes { get; set; }

        [DisplayName("Apenas em Cartaz")]
        public bool ApenasEmCartaz { get; set; } = true;

        public List<SelectListItem> CinemaNomes { get; private set; }

        public List<SelectListItem> CinemaCidades { get; private set; }

        public List<SelectListItem> CinemaEstados { get; private set; }

        [DisplayName("Cinema")]
        public string CinemaNome { get; set; }

        [DisplayName("Cidade")]
        public string CinemaCidade { get; set; }

        [DisplayName("Estado")]
        public UnidadesFederativas? CinemaEstado { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}"), DisplayName("Inicio da Exibição")]
        public DateTime? InicioExibicao { get; set; }

        [DataType(DataType.Date), DisplayName("Fim da Exibição")]
        public DateTime? FimExibicao { get; set; }

        /// <summary>
        /// Atribui as lista para as select list de forma dinâmicamente a partir do banco de dados
        /// </summary>
        /// <param name="context">Contexto da conexão com banco de dados</param>
        public async Task SetSelectListItems(MvcFilmeContext context)
        {
            var hoje = DateTime.Now;
            var baseQuery = context.Cartaz.Where(ca => ca.FilmeId == Filme.Id)
                .Where(c => !ApenasEmCartaz || c.FimExibicao >= hoje);

            CinemaNomes = await baseQuery.Select(c => new SelectListItem { Text = c.Cinema.Nome, Value = c.Cinema.Nome }).Distinct().OrderBy(si => si.Value).ToListAsync();
            CinemaCidades = await baseQuery.Select(c => new SelectListItem { Text = c.Cinema.Cidade, Value = c.Cinema.Cidade }).Distinct().OrderBy(si => si.Value).ToListAsync();
            CinemaEstados = await baseQuery.Select(c => new SelectListItem { Text = ((int)c.Cinema.UnidadeFederativa).ToString(), Value = c.Cinema.UnidadeFederativa.GetEnumDisplayName() }).Distinct().OrderBy(si => si.Value).ToListAsync();
        }
    }
}
