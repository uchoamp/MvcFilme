using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFilme.Models
{
    public enum GenerosFilme: ushort
    {
        [Display(Name = "Ação")]
        ACAO,
        [Display(Name = "Aventura")]
        AVENTURA, 
        [Display(Name = "Comédia")]
        COMEDIA, 
        [Display(Name = "Comédia de ação")]
        COMEDIA_DE_ACAO, 
        [Display(Name = "Comédia romântica")]
        COMEDIA_ROMANTICA, 
        [Display(Name = "Dança")]
        DANCA, 
        [Display(Name = "Documentário")]
        DOCUMENTARIO, 
        [Display(Name = "Drama")]
        DRAMA, 
        [Display(Name = "Espionagem")]
        ESPIONAGEM, 
        [Display(Name = "Faroeste")]
        FAROESTE, 
        [Display(Name = "Fantasia")]
        FANTASIA, 
        [Display(Name = "Ficção científica")]
        FICCAO_CIENTIFICA, 
        [Display(Name = "Guerra")]
        GUERRA, 
        [Display(Name = "Mistério")]
        MISTERIO, 
        [Display(Name = "Musical")]
        MUSICAL, 
        [Display(Name = "Policial")]
        POLICIAL, 
        [Display(Name = "Romance")]
        ROMANCE, 
        [Display(Name = "Terror")]
        TERROR, 
        [Display(Name = "Thriller")]
        THRILLER, 
    };

    public enum Classificacoes: byte 
    {
        [Display(Name = "Livre")]
        CL,
        [Display(Name = "10")]
        C10,
        [Display(Name = "12")]
        C12,
        [Display(Name = "14")]
        C14,
        [Display(Name = "16")]
        C16,
        [Display(Name = "18")]
        C18
    }
    public class Filme: BaseModelPersistence
    {

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [StringLength(60, MinimumLength = 3)]
        public string Titulo { get; set; }

        [Column(TypeName = "text")]
        public string Sinopse { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [StringLength(255), DataType(DataType.ImageUrl)]
        public string Capa { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Lançamento"),Column(TypeName = "Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Lancamento { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Gênero")]
        public GenerosFilme Genero { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Classificação")]
        public Classificacoes Classificacao { get; set; }

        public virtual List<Cartaz> Cartazes { get; set; } 
    }
}
