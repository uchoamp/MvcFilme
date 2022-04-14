using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFilme.Models
{
    public class Cartaz: BaseModelPersistence
    {
        [Range(1,99)]
        [DisplayName("Preço")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Decimal(4,2)")]
        public decimal? Preco { get; set; }

        [Column(TypeName = "Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false),DisplayName("Inicio da Exibição")]
        public DateTime? InicioExibicao { get; set; }
        
        [Column(TypeName = "Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false), DisplayName("Fim da Exibição")]
        public DateTime? FimExibicao { get; set; }

        [Required]
        [DisplayName("Filme")]
        public int FilmeId { get; set; }

        [Required]
        [DisplayName("Cinema")]
        public int CinemaId { get; set; }

        public virtual Filme Filme { get; set; }
        public virtual Cinema Cinema { get; set; }

    }

}
