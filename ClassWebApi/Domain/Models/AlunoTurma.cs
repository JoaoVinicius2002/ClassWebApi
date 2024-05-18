using ClassWebApi.Domain.Models.DTOs;

namespace ClassWebApi.Models
{
    public class AlunoTurma
    {
        public AlunoTurma()
        {

        }
        public AlunoTurma(int alunoId, int turmaId)
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
        }
        public AlunoTurma(AlunoTurmaDTO alunoTurma)
        {
            AlunoId = alunoTurma.Aluno.AlunoId;
            TurmaId = alunoTurma.Turma.TurmaId;
        }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
    }
}
