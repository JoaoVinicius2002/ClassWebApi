using ClassWebApi.Models;

namespace ClassWebApi.Infrastructure.Interfaces
{
    public interface IAlunoTurmaRepository
    {
        bool UpdateAlunoTurma(AlunoTurma alunoTurma);
        void CreateAlunoTurma(AlunoTurma alunoTurma);
        IEnumerable<AlunoTurma> GetAlunoTurmas();
        AlunoTurma GetAlunoTurma(int alunoId, int turmaId);
        bool DeleteAlunoTurma(int alunoId, int turmaId);
    }
}
