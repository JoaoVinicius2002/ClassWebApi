using ClassWebApi.Domain.Models;
using ClassWebApi.Infrastructure.Interfaces;
using ClassWebApi.Infrastructure.Utils;
using ClassWebApi.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ClassWebApi.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IDbConnection _connection;
        private readonly string _connectionString;


        public AlunoRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public bool DeleteAluno(int alunoId)
        {
            if (alunoId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(alunoId), "Informe um ID de aluno válido.");
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var sqlDelete = @"DELETE FROM Aluno WHERE Id = @Id";
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", alunoId);

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
        public bool UpdateAluno(Aluno aluno)
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            using var transaction = _connection.BeginTransaction();

            try
            {
                var sqlUpdate = @"UPDATE Aluno SET Nome = @Nome, Usuario = @Usuario WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Nome", aluno.Nome);
                parameters.Add("@Usuario", aluno.Usuario);
                parameters.Add("@Id", aluno.AlunoId);

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

        public int CreateAluno(Aluno aluno)
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            using var transaction = _connection.BeginTransaction();

            try
            {
                string hashedSenha = HashHelper.CalculateSHA256Hash(aluno.Senha); 

                var sqlInsert = @"INSERT INTO Aluno (Nome, Usuario, Senha) VALUES (@Nome, @Usuario, @Senha)";
                var parameters = new DynamicParameters();
                parameters.Add("@Nome", aluno.Nome);
                parameters.Add("@Usuario", aluno.Usuario.Trim());
                parameters.Add("@Senha", hashedSenha);

                _connection.Execute(sqlInsert, parameters, transaction: transaction);
                var sqlGetId = @"SELECT TOP 1 id from aluno order by id desc";
                var alunoId = _connection.QuerySingle<int>(sqlGetId, transaction: transaction);

                transaction.Commit();

                return alunoId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 0;
            }
        }

        public IEnumerable<Aluno> GetAlunos(int? turmaId = null)
        {
            using (var connection = _connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                var sql = "";
                using (var command = connection.CreateCommand())
                {
                    if (turmaId.HasValue)
                    {
                        sql = @"
                            SELECT a.id, a.nome, a.usuario, a.senha
                            FROM Aluno a
                            WHERE a.id IN (
                                SELECT aluno_id
                                FROM aluno_turma
                                WHERE turma_id IS NULL OR turma_id = @turmaId
                            )";
                    }
                    else
                    {
                        sql = @"SELECT id, nome, usuario, senha  FROM Aluno";
                    }
                    command.CommandText = sql;
                    if (turmaId.HasValue)
                    {
                        command.Parameters.Add(new SqlParameter("@turmaId", SqlDbType.Int) { Value = turmaId.Value });
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        var alunos = new List<Aluno>();
                        while (reader.Read())
                        {

                            var aluno = new Aluno
                            {

                                AlunoId = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Usuario = reader["usuario"].ToString().Trim(),
                                Senha = reader["senha"].ToString().Trim(),
                            };
                            alunos.Add(aluno);
                        }
                        return alunos;
                    }
                }
            }
        }
        public Aluno GetAluno(int alunoId)
        {
            if (alunoId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(alunoId), "Informe um ID de aluno válido.");
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", alunoId);

            var query = @"SELECT id, nome, usuario, senha FROM Aluno WHERE id = @Id";

            using (var reader = _connection.ExecuteReader(query, parameters))
            {
                if (reader.Read())
                {
                    return new Aluno()
                    {
                        AlunoId = Convert.ToInt32(reader["id"]),
                        Nome = reader["nome"].ToString(),
                        Usuario = reader["usuario"].ToString().Trim(),
                        Senha = reader["senha"].ToString().Trim()
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
