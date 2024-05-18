namespace ClassFrontEnd.Models
{
    public class AlunoTurmaViewModel
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public List<AlunoViewModel> Alunos { get; set; }
        public List<TurmaViewModel> Turmas { get; set; }

    }
}
