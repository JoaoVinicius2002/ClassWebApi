using ClassWebApi.Models;

namespace ClassWebApi.Infrastructure.Interfaces
{
    public interface ITurmaRepository
    {
        bool UpdateTurma(Turma turma);
        int CreateTurma(Turma turma);
        IEnumerable<Turma> GetTurmas(int? turmaId = null);
        Turma GetTurma(int turmaId);
        bool DeleteTurma(int turmaId);
    }
}
