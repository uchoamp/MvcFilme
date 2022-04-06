using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFilme.Models
{
    public class Filme
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3), Required]
        public string Titulo { get; set; }

        [DisplayName("Data de Lançamento"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Lancamento { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z'\/\s]*$"), Required]
        [StringLength(30)]
        [DisplayName("Gênero")]
        public string Genero { get; set; }

        [Range(1,100)]
        [DisplayName("Preço")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Decimal(2,2)")]
        public decimal Preco { get; set; }
        
        [StringLength(5)]
        [Required]
        [DisplayName("Classificação")]
        public string Classificacao { get; set; }
    }
}
