using System.ComponentModel.DataAnnotations;

namespace ClassFrontEnd.Models
{
    public class TurmaViewModel
    {
        public TurmaViewModel()
        {

        }
        public int TurmaId { get; set; }
        [Required(ErrorMessage = "O campo Ano é obrigatório.")]
        [Range(1, 5, ErrorMessage = "O campo Ano deve estar entre 1 e 5. (Pois a duração máxima de um curso é de 5 anos)")]
        public int Ano { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Nome deve ter no máximo {1} caracteres.")]
        public string TurmaNome { get; set; }
        [Required(ErrorMessage = "O campo Materia é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Materia deve ter no máximo {1} caracteres.")]
        public string Materia { get; set; }
    }
}
