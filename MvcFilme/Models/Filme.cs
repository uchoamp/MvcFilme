using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFilme.Models
{
    public enum Classificacoes: byte 
    {
        [Display(Name = "Livre")]
        CL,
        [Display(Name = "12")]
        C12,
        [Display(Name = "14")]
        C14,
        [Display(Name = "16")]
        C16,
        [Display(Name = "18")]
        C18
    }
    public class Filme
    {
        [Key]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PublicId { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [StringLength(60, MinimumLength = 3)]
        public string Titulo { get; set; }

        [Column(TypeName = "text")]
        public string Sinopse { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [StringLength(255), DataType(DataType.ImageUrl)]
        public string Capa { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Data de Lançamento"),Column(TypeName = "Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Lancamento { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [StringLength(30, ErrorMessage = "O campo {0} deve possível nom máximo {1} caracteres")]
        [DisplayName("Gênero")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Classificação")]
        public Classificacoes Classificacao { get; set; }

        public List<Cartaz> Cartazes { get; set; } 
    }
}
