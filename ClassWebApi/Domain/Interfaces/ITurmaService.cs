using ClassWebApi.Models;

namespace ClassWebApi.Domain.Interfaces
{
    public interface ITurmaService
    {
        int CreateTurmaAsync(Turma turma);
        IEnumerable<Turma> GetTurmas(int? turmaId = null);
        bool UpdateTurma(Turma turmaBD);
        bool DeleteTurma(int turmaId);
        Turma GetTurma(int id);
    }
}
