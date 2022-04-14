using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFilme.Models
{
    /// <summary>
    /// Modelo base, esses são campos básicos para todas a entitdades
    /// </summary>
    public class BaseModelPersistence
    {
        [Key] // O Id inteiro continua sendo a primary key, não sei se é a melhor abordagem mas como indexei o PublicId deve dá no mesmo
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Não funciona
        public Guid PublicId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Inserted { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; }

    }
}
