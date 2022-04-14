using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFilme.Data;
using MvcFilme.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilme.Models
{
    public class FilmesViewModel
    {
        public PaginatedList<Filme> Filmes { get; set; }

        // FILTROS
        [DisplayName("Gênero")]
        public GenerosFilme? Genero { get; set; }

        [DisplayName("Classificação")]
        public Classificacoes? Classificacao { get; set; }

        [DisplayName("Ano de Lançamento")]
        public int AnoLancamento { get; set; }

        [DisplayName("Busca por Titulo")]
        public string BuscaTitulo { get; set; } 
        public List<SelectListItem> AnosLancamento { get; private set; }

        // ORDENAÇÃO  
        public bool Ordem { get; set; } = true;

        private string _ordenaPor = "insercao";
        public string OrdenaPor { get => _ordenaPor; set {
                if (TiposOrdem.ContainsKey(value))
                    _ordenaPor = value;
                else
                    _ordenaPor = TiposOrdem.Keys.First();
            } } 

        // PAGINAÇÃO
        private int _paginaAtual { get; set; } = 1;
        
        public int PaginaAtual
        {
            get => _paginaAtual; 
            set {
                _paginaAtual = value < 1 ? 1 : value;
            }
        }

        private int _quantidadeDeItemPorPagina { get; set; } = 9;
        public int QuantidadeDeItemPorPagina
        {
            get => _quantidadeDeItemPorPagina; 
            set {
                _quantidadeDeItemPorPagina = value < 1 ? 9 : value;
            }
        }

        public IDictionary<string, string> TiposOrdem = new Dictionary<string, string> {
            {"insercao", "Inserção" },
            {"titulo", "Titulo" },
            {"lancamento", "Lançamento" },
        };

        
        public static List<SelectListItem> Generos { get; } = Enum.GetValues(typeof(GenerosFilme))
            .Cast<GenerosFilme>().Select(g => new SelectListItem
            {
                Text = g.GetEnumDisplayName(),
                Value = ((int)g).ToString()
            })
            .ToList();



        /// <summary>
        /// Cria uma lista de selects a partir de um enum
        /// </summary>
        public static List<SelectListItem> Classificacoes { get; } = Enum.GetValues(typeof(Classificacoes))
            .Cast<Classificacoes>().Select(c => new SelectListItem
            {
                Text = c.GetEnumDisplayName(),
                Value = ((int)c).ToString()
            })
            .ToList();

       
        /// <summary>
        /// O estado da view a atual e forma de dicionário para ser usado no anchor tag help como forma de manter
        /// o estado principalemnte como forma de ajudar na paginação 
        /// </summary>
        /// <param name="page">O número da página</param>
        /// <returns>Dicionários com os parâmetros com estado da view atual</returns>
        public IDictionary<string, string> GetRouteParams(int page = 0)
        {
            var @params = new Dictionary<string, string>
        {
            { "Genero",  Genero == null ? "" : ((int)Genero).ToString()},
            { "BuscaTitulo",  BuscaTitulo == null ? "" : BuscaTitulo},
            { "AnoLancamento",  AnoLancamento.ToString()},
            { "Classificacao", Classificacao == null ? "" : ((int)Classificacao).ToString()},
            { "Ordem", Ordem.ToString().ToLower() },
            { "OrdenaPor",  OrdenaPor},
            { "QuantidadeDeItemPorPagina", QuantidadeDeItemPorPagina.ToString()},
            { "PaginaAtual", page == 0 ? PaginaAtual.ToString() : page.ToString()}
        };
            return @params;
        }

        /// <summary>
        /// Atribui as lista para as select list de forma dinâmicamente a partir do banco de dados
        /// </summary>
        /// <param name="context">Contexto da conexão com banco de dados</param>
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
