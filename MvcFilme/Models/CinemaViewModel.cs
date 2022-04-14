using Microsoft.AspNetCore.Mvc.Rendering;
using MvcFilme.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcFilme.Utils;

namespace MvcFilme.Models
{
    public class CinemaViewModel
    {
        [DisplayName("Cinema")]
        public Cinema Cinema { get; set; }
        public List<Cartaz> Cartazes { get; set; }

        [DisplayName("Apenas em Cartaz")]
        public bool ApenasEmCartaz { get; set; } = true;

        [DisplayName("Busca por Titulo")]
        public string FilmeTitulo { get; set; }

        public List<SelectListItem> FilmeGeneros { get; private set; }

        public List<SelectListItem> FilmeClassificacoes { get; private set; }
        public List<SelectListItem> FilmeAnosLancamento { get; private set; }

        [DisplayName("Gênero")]
        public GenerosFilme? FilmeGenero { get; set; }

        [DisplayName("Classificação")]
        public Classificacoes? FilmeClassificacao { get; set; }

        [DisplayName("Ano de Lançamento")]
        public int FilmeAnoLancamento { get; set; } 

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
            var baseQuery = context.Cartaz.Where(ca => ca.CinemaId == Cinema.Id)
                .Where(c => !ApenasEmCartaz || c.FimExibicao >= hoje);

            FilmeGeneros = await baseQuery
                .Select(c => new SelectListItem
                {
                    Text = c.Filme.Genero.GetEnumDisplayName(),
                    Value = ((int)c.Filme.Genero).ToString()
                })
                .Distinct().OrderBy(si => si.Value).ToListAsync();

            FilmeClassificacoes = await baseQuery
                .Select(c => new SelectListItem
                {
                    Text = c.Filme.Classificacao.GetEnumDisplayName(),
                    Value = ((int)c.Filme.Classificacao).ToString()
                })
                .Distinct().OrderBy(si => si.Value).ToListAsync();

            FilmeAnosLancamento = await baseQuery
                .Select(c => c.Filme.Lancamento.Year)
                .Distinct()
                .Select(c => new SelectListItem { 
                    Text = c.ToString(),
                    Value = c.ToString()
                }).ToListAsync();
        }
    }
}
