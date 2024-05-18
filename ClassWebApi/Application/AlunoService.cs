using ClassWebApi.Domain.Interfaces;
using ClassWebApi.Domain.Models;
using ClassWebApi.Infrastructure.Interfaces;
using ClassWebApi.Infrastructure.Repositories;
using ClassWebApi.Models;

namespace ClassWebApi.Application
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }
        public bool DeleteAluno(int alunoId)
        {
            return _alunoRepository.DeleteAluno(alunoId);
        }
        public bool UpdateAluno(Aluno alunoBD)
        {
            return _alunoRepository.UpdateAluno(alunoBD);
        }

        public int CreateAlunoAsync(Aluno aluno)
        {
            return _alunoRepository.CreateAluno(aluno);
        }
        public IEnumerable<Aluno> GetAlunos(int? turmaId = null)
        {
            return _alunoRepository.GetAlunos(turmaId);
        }
        public Aluno GetAluno(int alunoId)
        {
            return _alunoRepository.GetAluno(alunoId);
        }
    }
}
