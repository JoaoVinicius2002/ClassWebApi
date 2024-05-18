using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ClassFrontEnd.Models
{
    public class AlunoViewModel
    {
        public AlunoViewModel()
        {

        }
        public int AlunoId { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Usuário é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Usuário deve ter no máximo {1} caracteres.")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        [RegularExpression(@"^(?=.*[!@#$%^&*]).{8,}$", ErrorMessage = "A senha deve conter pelo menos 8 caracteres, incluindo pelo menos um caractere especial.")]
        public string Senha { get; set; }

    }
}
