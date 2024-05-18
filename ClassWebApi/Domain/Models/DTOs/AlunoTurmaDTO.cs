using ClassWebApi.Models;

namespace ClassWebApi.Domain.Models.DTOs
{
    public class AlunoTurmaDTO
    {
        public Aluno Aluno { get; set; }    
        public Turma Turma { get; set; }
    }
}
