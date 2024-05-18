using ClassFrontEnd.Models;

namespace ClassFrontEnd.Controllers
{
    internal class TurmaDetailsViewModel
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public IEnumerable<AlunoViewModel> Alunos { get; set; }
    }
}