using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFilme.Models
{
    public enum UnidadesFederativas: byte
    {
        [Display(Name = "Acre")]
        AC,
        [Display(Name = "Alagoas")]
        AL,
        [Display(Name = "Amapá")]
        AP,
        [Display(Name = "Amazonas")]
        AM,
        [Display(Name = "Bahia")]
        BA,
        [Display(Name = "Ceará")]
        CE,
        [Display(Name = "Distrito Federal")]
        DF,
        [Display(Name = "Espirito Santo")]
        ES,
        [Display(Name = "Goiás")]
        GO,
        [Display(Name = "Maranhão")]
        MA,
        [Display(Name = "Mato Grosso")]
        MT,
        [Display(Name = "Mato Grosso do Sul")]
        MS,
        [Display(Name = "Minas Gerais")]
        MG,
        [Display(Name = "Pará")]
        PA,
        [Display(Name = "Paraiba")]
        PB,
        [Display(Name = "Paraná")]
        PR,
        [Display(Name = "Pernambuco")]
        PE,
        [Display(Name = "Piauí")]
        PI,
        [Display(Name = "Rio de Janeiro")]
        RJ,
        [Display(Name = "Rio Grande do Norte")]
        RN,
        [Display(Name = "Rio Grande do Sul")]
        RS,
        [Display(Name = "Rondônia")]
        RO,
        [Display(Name = "Roraima")]
        RR,
        [Display(Name = "Santa Catarina")]
        SC,
        [Display(Name = "São Paulo")]
        SP,
        [Display(Name = "Sergipe")]
        SE,
        [Display(Name = "Tocantis")]
        TO
    }

    public class Cinema
    {

        [Key]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PublicId { get; set; }

        [DisplayName("Nome")]
        [StringLength(255)]
        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        public string Nome { get; set; }

        [DisplayName("Telefone")]
        [StringLength(15), Required(ErrorMessage = "O campo {0} não pode ser vazio"), DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\([1-9]{2}\) \d{4}-\d{4}$", ErrorMessage = "O telefone deve está no formato (xx) xxxx-xxxx")]
        public string Telefone { get; set; }

        [StringLength(100, MinimumLength = 4)]
        [Required]
        public string Cidade { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        public UnidadesFederativas UnidadeFederativa { get; set; }

        [DisplayName("Quantidade de Filmes em Cartaz")]
        [NotMapped]
        public int QuantidadeFilmesEmCartaz { get; set; } = 0;

        [DisplayName("Quantidade de Cartazes")]
        [NotMapped]
        public int QuantidadeCartazes { get; set; } = 0;

        public List<Cartaz> Cartazes { get; set; }
    }
}
