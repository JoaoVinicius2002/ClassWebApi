using ClassWebApi.Application;
using ClassWebApi.Domain.Models;
using ClassWebApi.Models;

namespace ClassWebApi.Domain.Interfaces
{
    public interface IAlunoService
    {
        int CreateAlunoAsync(Aluno aluno);
        IEnumerable<Aluno> GetAlunos(int? turmaId = null);
        bool UpdateAluno(Aluno alunoBD);
        bool DeleteAluno(int alunoId);
        Aluno GetAluno(int id);
    }
}
