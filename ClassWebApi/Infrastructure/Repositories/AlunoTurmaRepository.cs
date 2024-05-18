using ClassWebApi.Infrastructure.Interfaces;
using ClassWebApi.Models;
using Dapper;
using System.Data;

namespace ClassWebApi.Infrastructure.Repositories
{
    public class AlunoTurmaRepository : IAlunoTurmaRepository
    {
        private readonly IDbConnection _connection;
        private readonly string _connectionString;


        public AlunoTurmaRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public bool DeleteAlunoTurma(int alunoId, int turmaId)
        {
            if (alunoId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(alunoId), "Informe um ID de alunoTurma válido.");
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var sqlDelete = @"DELETE FROM aluno_turma WHERE aluno_id = @aluno_id and turma_id = @turma_id";
                    var parameters = new DynamicParameters();
                    parameters.Add("@aluno_id", alunoId);
                    parameters.Add("@turma_id", turmaId);


                    var rowsAffected = _connection.Execute(sqlDelete, parameters, transaction: transaction);

                    transaction.Commit();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public bool UpdateAlunoTurma(AlunoTurma alunoTurma)
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            using var transaction = _connection.BeginTransaction();

            try
            {
                var sqlUpdate = @"UPDATE aluno_turma SET Nome = @Nome, Usuario = @Usuario, Senha = @Senha WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Nome", alunoTurma.TurmaId);

                var rowsAffected = _connection.Execute(sqlUpdate, parameters, transaction: transaction);

                transaction.Commit();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
        }

        public void CreateAlunoTurma(AlunoTurma alunoTurma)
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            using var transaction = _connection.BeginTransaction();

            try
            {
                var sqlInsert = @"INSERT INTO aluno_turma (aluno_id, turma_id) VALUES (@aluno_id, @turma_id)";
                var parameters = new DynamicParameters();
                parameters.Add("@aluno_id", alunoTurma.AlunoId);
                parameters.Add("@turma_id", alunoTurma.TurmaId);


                _connection.Execute(sqlInsert, parameters, transaction: transaction);

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public IEnumerable<AlunoTurma> GetAlunoTurmas()
        {
            using (var connection = _connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT aluno_id, turma_id  FROM aluno_turma";

                    using (var reader = command.ExecuteReader())
                    {
                        var alunoTurmas = new List<AlunoTurma>();
                        while (reader.Read())
                        {

                            var alunoTurma = new AlunoTurma
                            {

                                AlunoId = Convert.ToInt32(reader["aluno_id"]),
                                TurmaId = Convert.ToInt32(reader["turma_id"]),
                            };
                            alunoTurmas.Add(alunoTurma);
                        }
                        return alunoTurmas;
                    }
                }
            }
        }
        public AlunoTurma GetAlunoTurma(int alunoId, int turmaId)
        {
            if (alunoId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(alunoId), "Informe um ID de alunoTurma válido.");
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"SELECT aluno_id, turma_id  FROM aluno_turma";
                var parameters = new DynamicParameters();
                parameters.Add("@aluno_id", alunoId);
                parameters.Add("@turma_id", turmaId);


                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new AlunoTurma
                        {

                            AlunoId = Convert.ToInt32(reader["aluno_id"]),
                            TurmaId = Convert.ToInt32(reader["turma_id"]),
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
