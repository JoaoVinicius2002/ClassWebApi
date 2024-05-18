using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ClassWebApi.Domain.Models.DTOs
{
    public class TurmaDTO
    {
        [Required]
        [SwaggerSchema("Ano da turma")]
        public int Ano { get; set; }
        [Required]
        [StringLength(50)]
        [SwaggerSchema("Nome da turma")]
        public string TurmaNome { get; set; }
        [Required]
        [StringLength(50)]
        [SwaggerSchema("Materia da turma")]
        public string Materia { get; set; }

    }
}
