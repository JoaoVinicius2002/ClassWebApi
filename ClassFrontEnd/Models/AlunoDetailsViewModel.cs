using ClassFrontEnd.Models;

namespace ClassFrontEnd.Controllers
{
    internal class AlunoDetailsViewModel
    {
        public int AlunoId { get; set; }
        public object Nome { get; set; }
        public IEnumerable<TurmaViewModel> Turmas { get; set; }
    }
}