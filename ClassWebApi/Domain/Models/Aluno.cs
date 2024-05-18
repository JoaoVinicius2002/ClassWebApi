using ClassWebApi.Domain.Models.DTOs;

namespace ClassWebApi.Domain.Models
{
    public class Aluno
    {
        public Aluno()
        {

        }
        public Aluno(AlunoDTO aluno)
        {
            Nome = aluno.Nome;
            Usuario = aluno.Usuario;
            Senha = aluno.Senha;
        }
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}
