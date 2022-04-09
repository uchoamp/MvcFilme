using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFilme.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PublicId { get; set; }

        [StringLength(60, MinimumLength = 3), Required]
        public string Titulo { get; set; }

        [Column(TypeName = "text")]
        public string Sinopse { get; set; }

        [StringLength(255), DataType(DataType.ImageUrl), Required]
        public string Capa { get; set; }

        [DisplayName("Data de Lançamento"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Lancamento { get; set; }

        [Required, StringLength(30)]
        [DisplayName("Gênero")]
        public string Genero { get; set; }

        [StringLength(5)]
        [Required]
        [DisplayName("Classificação")]
        public string Classificacao { get; set; }

        public List<Cartaz> Cartazes { get; set; } 
    }
}
