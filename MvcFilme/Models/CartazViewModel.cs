using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    public class CartazViewModel : IValidatableObject
    {
        public Cartaz Cartaz { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [Range(1, 99)]
        [DisplayName("Preço")]
        [DataType(DataType.Currency)]
        public decimal? Preco { get; set; }

        [IsDateBefore("FimExibicao", true, ErrorMessage = "A data de inicio da exibicao deve ser igual ou menor que data de fim da exibição")]
        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Inicio da Exibição"), Column(TypeName = "Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? InicioExibicao { get; set; }

        [IsDateAfter("InicioExibicao", true, ErrorMessage = "A data de fim da exibicao deve ser igual ou maior que data de inicio da exibição")]
        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Fim da Exibição"), Column(TypeName = "Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FimExibicao { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Filme")]
        public Guid? FilmePublicId { get; set; }

        [Required(ErrorMessage = "O campo {0} não pode ser vazio")]
        [DisplayName("Cinema")]
        public Guid? CinemaPublicId { get; set; }

        public List<SelectListItem> Filmes { get; private set; }

        public List<SelectListItem> Cinemas { get; private set; }

        /// <summary>
        /// Atribui as lista para as select list de forma dinâmicamente a partir do banco de dados
        /// </summary>
        /// <param name="context">Contexto da conexão com banco de dados</param>
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

        /// <summary>
        /// Validação para datas de inicio e fim da exibição
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((InicioExibicao != null && FimExibicao != null) && InicioExibicao > FimExibicao)
            {
                yield return new ValidationResult("A data de inicio da exibição de ser menor que a do fim da exibição.");
            }
        }
    }

    /// <summary>
    /// Atributo que válida se o campo <c>DateTime</c> decorado é maior ou talvez igual a outro campo passado
    /// </summary>
    public sealed class IsDateAfter : ValidationAttribute, IClientModelValidator
    {
        private readonly string _testedPropertyName;
        private readonly bool _allowEqualDates;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testedPropertyName">O string com o nome da propriedade a ser comparada</param>
        /// <param name="allowDateEquals">A igualdade também em válida?</param>
        public IsDateAfter(string testedPropertyName, bool allowDateEquals = false)
        {
            _testedPropertyName = testedPropertyName;
            _allowEqualDates = allowDateEquals;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(_testedPropertyName);
            if (propertyTestedInfo == null)
                return new ValidationResult(String.Format("unknown property {0}", this._testedPropertyName));

            var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);

            if (value == null || !(value is DateTime))
                return ValidationResult.Success;

            if (propertyTestedValue == null || !(propertyTestedValue is DateTime))
                return ValidationResult.Success;

            if ((_allowEqualDates && (DateTime)value >= (DateTime)propertyTestedValue) || (DateTime)value > (DateTime)propertyTestedValue)
                return ValidationResult.Success;

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        /// <summary>
        /// Seta atributos para a validação no lado do criente
        /// </summary>
        /// <param name="context"></param>
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val-isdateafter", errorMessage);
            MergeAttribute(context.Attributes, "data-val-isdateafter-allowequaldates", _allowEqualDates.ToString());
            MergeAttribute(context.Attributes, "data-val-isdateafter-propertytested", _testedPropertyName);
        }

        private bool MergeAttribute(
            IDictionary<string, string> attributes,
            string key,
            string value
            )
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);

            return true;
        }
    }

    /// <summary>
    /// Atributo que válida se o campo <c>DateTime</c> decorado é menor ou talvez igual a outro campo passado
    /// </summary>
    public sealed class IsDateBefore : ValidationAttribute, IClientModelValidator
    {
        private readonly string _testedPropertyName;
        private readonly bool _allowEqualDates;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="testedPropertyName">O string com o nome da propriedade a ser comparada</param>
        /// <param name="allowDateEquals">A igualdade também em válida?</param>
        public IsDateBefore(string testedPropertyName, bool allowDateEquals = false)
        {
            _testedPropertyName = testedPropertyName;
            _allowEqualDates = allowDateEquals;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(_testedPropertyName);
            if (propertyTestedInfo == null)
                return new ValidationResult(String.Format("unknown property {0}", this._testedPropertyName));

            var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);

            if (value == null || !(value is DateTime))
                return ValidationResult.Success;

            if (propertyTestedValue == null || !(propertyTestedValue is DateTime))
                return ValidationResult.Success;

            if ((_allowEqualDates && (DateTime)value <= (DateTime)propertyTestedValue) || (DateTime)value < (DateTime)propertyTestedValue)
                return ValidationResult.Success;

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        /// <summary>
        /// Seta atributos para a validação no lado do criente
        /// </summary>
        /// <param name="context"></param>
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val-isdatebefore", errorMessage);
            MergeAttribute(context.Attributes, "data-val-isdatebefore-allowequaldates", _allowEqualDates.ToString());
            MergeAttribute(context.Attributes, "data-val-isdatebefore-propertytested", _testedPropertyName);
        }

        private bool MergeAttribute(
            IDictionary<string, string> attributes,
            string key,
            string value
            )
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);

            return true;
        }
    }
}
