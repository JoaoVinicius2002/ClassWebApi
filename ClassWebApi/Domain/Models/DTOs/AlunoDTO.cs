using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ClassWebApi.Domain.Models.DTOs
{
    public class AlunoDTO
    {
        [Required]
        [StringLength(50)]
        [SwaggerSchema("Usuário de login do aluno")]
        public string Nome { get; set; }
        [Required]
        [StringLength(30)]
        [SwaggerSchema("Usuário de login do aluno")]
        public string Usuario { get; set; }
        [Required]
        [StringLength(64)]
        [SwaggerSchema("Usuário de login do aluno")]
        public string Senha { get; set; }

    }
}
