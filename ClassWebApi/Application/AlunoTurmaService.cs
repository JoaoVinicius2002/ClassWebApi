using ClassWebApi.Domain.Interfaces;
using ClassWebApi.Infrastructure.Interfaces;
using ClassWebApi.Models;

namespace ClassWebApi.Application
{
    public class AlunoTurmaService : IAlunoTurmaService
    {
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;

        public AlunoTurmaService(IAlunoTurmaRepository alunoTurmaRepository)
        {
            _alunoTurmaRepository = alunoTurmaRepository;
        }
        public bool DeleteAlunoTurma(int alunoId, int turmaId)
        {
            return _alunoTurmaRepository.DeleteAlunoTurma(alunoId, turmaId);
        }
        public bool UpdateAlunoTurma(AlunoTurma alunoTurmaBD)
        {
            return _alunoTurmaRepository.UpdateAlunoTurma(alunoTurmaBD);
        }

        public void CreateAlunoTurmaAsync(AlunoTurma alunoTurma)
        {
             _alunoTurmaRepository.CreateAlunoTurma(alunoTurma);
        }
        public IEnumerable<AlunoTurma> GetAlunoTurmas()
        {
            return _alunoTurmaRepository.GetAlunoTurmas();
        }
        public AlunoTurma GetAlunoTurma(int alunoId, int turmaId)
        {
            return _alunoTurmaRepository.GetAlunoTurma(alunoId, turmaId);
        }
    }
}
