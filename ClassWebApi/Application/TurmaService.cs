using ClassWebApi.Domain.Interfaces;
using ClassWebApi.Infrastructure.Interfaces;
using ClassWebApi.Models;

namespace ClassWebApi.Application
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }
        public bool DeleteTurma(int turmaId)
        {
            return _turmaRepository.DeleteTurma(turmaId);
        }
        public bool UpdateTurma(Turma turmaBD)
        {
            return _turmaRepository.UpdateTurma(turmaBD);
        }

        public int CreateTurmaAsync(Turma turma)
        {
            return _turmaRepository.CreateTurma(turma);
        }
        public IEnumerable<Turma> GetTurmas(int? turmaId = null)
        {
            return _turmaRepository.GetTurmas(turmaId);
        }
        public Turma GetTurma(int turmaId)
        {
            return _turmaRepository.GetTurma(turmaId);
        }
    }
}
