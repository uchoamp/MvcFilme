using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFilme.Models
{
    public enum UnidadesFederativas 
    {
        [DisplayName("Acre")]
        AC,
        [DisplayName("Alagoas")]
        AL,
        [DisplayName("Amapá")]
        AP,
        [DisplayName("Amazonas")]
        AM,
        [DisplayName("Bahia")]
        BA,
        [DisplayName("Ceará")]
        CE,
        [DisplayName("Distrito Federal")]
        DF,
        [DisplayName("Espirito Santo")]
        ES,
        [DisplayName("Goiás")]
        GO,
        [DisplayName("Maranhão")]
        MA,
        [DisplayName("Mato Grosso")]
        MT,
        [DisplayName("Mato Grosso do Sul")]
        MS,
        [DisplayName("Minas Gerais")]
        MG,
        [DisplayName("Pará")]
        PA,
        [DisplayName("Paraiba")]
        PB,
        [DisplayName("Paraná")]
        PR,
        [DisplayName("Pernambuco")]
        PE,
        [DisplayName("Piauí")]
        PI,
        [DisplayName("Rio de Janeiro")]
        RJ,
        [DisplayName("Rio Grande do Norte")]
        RN,
        [DisplayName("Rio Grande do Sul")]
        RS,
        [DisplayName("Rondônia")]
        RO,
        [DisplayName("Roraima")]
        RR,
        [DisplayName("Santa Catarina")]
        SC,
        [DisplayName("São Paulo")]
        SP,
        [DisplayName("Sergipe")]
        SE,
        [DisplayName("Tocantis")]
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
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public string Nome { get; set; }

        [StringLength(100, MinimumLength = 4)]
        [Required]
        public string Cidade { get; set; }

        [DisplayName("UF")]
        [Required]
        public UnidadesFederativas UnidadeFederativa { get; set; }

        public List<Cartaz> Cartazes { get; set; } 
    }
}
