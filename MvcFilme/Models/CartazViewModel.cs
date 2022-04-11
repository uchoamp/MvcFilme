using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFilme.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilme.Models
{
    public class CartazViewModel: IValidatableObject
    {
        public Cartaz Cartaz { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [Range(1, 99)]
        [DisplayName("Preço")]
        [DataType(DataType.Currency)]
        public decimal? Preco { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}"),DisplayName("Inicio da Exibição")]
        public DateTime? InicioExibicao { get; set; }
        
        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}"), DisplayName("Fim da Exibição")]
        public DateTime? FimExibicao { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Filme")]
        public Guid? FilmePublicId { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Cinema")]
        public Guid? CinemaPublicId { get; set; }

        public List<SelectListItem> Filmes { get; private set; }  

        public List<SelectListItem> Cinemas { get; private set; }

        public async Task SetSelectListItems(MvcFilmeContext context)
        {
            Filmes = await context.Filme.Select(f => new SelectListItem
            {
                Text = f.Titulo + " - " + f.Lancamento.Year.ToString(),
                Value = f.PublicId.ToString()
            }).ToListAsync();

            Cinemas = await context.Cinema.Select(c => new SelectListItem
            {
                Text = c.Nome + " - " + c.Cidade,
                Value = c.PublicId.ToString()
            }).ToListAsync();
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((InicioExibicao != null && FimExibicao != null) && InicioExibicao > FimExibicao)
            {
                yield return new ValidationResult("A data de inicio da exibição de ser menor que a do fim da exibição.");
            }
        }
    }
}
