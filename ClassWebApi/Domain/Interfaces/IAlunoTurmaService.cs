using ClassWebApi.Models;

namespace ClassWebApi.Domain.Interfaces
{
    public interface IAlunoTurmaService
    {
        void CreateAlunoTurmaAsync(AlunoTurma alunoTurma);
        IEnumerable<AlunoTurma> GetAlunoTurmas();
        bool UpdateAlunoTurma(AlunoTurma alunoTurmaBD);
        bool DeleteAlunoTurma(int alunoId, int turmaId);
        AlunoTurma GetAlunoTurma(int alunoId, int turmaId);
    }
}
