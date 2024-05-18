using ClassWebApi.Domain.Models;
using ClassWebApi.Models;

namespace ClassWebApi.Infrastructure.Interfaces
{
    public interface IAlunoRepository
    {
        bool UpdateAluno(Aluno aluno);
        int CreateAluno(Aluno aluno);
        IEnumerable<Aluno> GetAlunos(int? turmaId = null);
        Aluno GetAluno(int alunoId);
        bool DeleteAluno(int alunoId);

    }
}
