using ClassWebApi.Domain.Models.DTOs;

namespace ClassWebApi.Models
{
    public class Turma
    {
        public Turma()
        {

        }

        public Turma(TurmaDTO turma)
        {
            TurmaNome = turma.TurmaNome;
            Materia = turma.Materia;
            Ano = turma.Ano;
        }
        public int TurmaId { get; set; }
        public int Ano { get; set; }
        public string TurmaNome { get; set; }
        public string Materia { get; set; }
    }
}
