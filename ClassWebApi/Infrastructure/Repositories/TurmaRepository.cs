using ClassWebApi.Infrastructure.Interfaces;
using ClassWebApi.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ClassWebApi.Infrastructure.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly IDbConnection _connection;
        private readonly string _connectionString;


        public TurmaRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public bool DeleteTurma(int turmaId)
        {
            if (turmaId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(turmaId), "Informe um ID de aluno válido.");
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var sqlDelete = @"DELETE FROM turma WHERE Id = @Id";
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", turmaId);

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
        public bool UpdateTurma(Turma turma)
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            using var transaction = _connection.BeginTransaction();

            try
            {
                var sqlUpdate = @"UPDATE turma SET turma = @turma, curso_nome = @materia, ano = @ano WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@turma", turma.TurmaNome);
                parameters.Add("@materia", turma.Materia);
                parameters.Add("@ano", turma.Ano);
                parameters.Add("@Id", turma.TurmaId);

                var rowsAffected = _connection.Execute(sqlUpdate, parameters, transaction: transaction);

                transaction.Commit();

                return rowsAffected > 0;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public int CreateTurma(Turma turma)
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            using var transaction = _connection.BeginTransaction();

            try
            {
                var sqlInsert = @"INSERT INTO turma (curso_nome, turma, ano) VALUES (@curso_nome, @turma, @ano)";
                var parameters = new DynamicParameters();
                parameters.Add("@curso_nome", turma.Materia);
                parameters.Add("@turma", turma.TurmaNome.Trim());
                parameters.Add("@ano", turma.Ano);

                _connection.Execute(sqlInsert, parameters, transaction: transaction);
                var sqlGetId = @"SELECT TOP 1 id from turma order by id desc";
                var turmaId = _connection.QuerySingle<int>(sqlGetId, transaction: transaction);

                transaction.Commit();

                return turmaId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 0;
            }
        }

        public IEnumerable<Turma> GetTurmas(int? alunoId = null)
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
                    if (alunoId.HasValue)
                    {
                        sql = @"
                            SELECT t.id, t.curso_nome, t.turma, t.ano
                            FROM turma t
                            WHERE t.id IN (
                                SELECT turma_id
                                FROM aluno_turma
                                WHERE aluno_id IS NULL OR aluno_id = @alunoId
                            )";
                    }
                    else
                    {
                        sql = @"SELECT id, curso_nome, turma, ano  FROM turma";
                    }
                    command.CommandText = sql;
                    if (alunoId.HasValue)
                    {
                        command.Parameters.Add(new SqlParameter("@alunoId", SqlDbType.Int) { Value = alunoId.Value });
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        var alunos = new List<Turma>();
                        while (reader.Read())
                        {

                            var aluno = new Turma
                            {

                                TurmaId = Convert.ToInt32(reader["id"]),
                                TurmaNome = reader["turma"].ToString(),
                                Materia = reader["curso_nome"].ToString().Trim(),
                                Ano = Convert.ToUInt16(reader["ano"]),
                            };
                            alunos.Add(aluno);
                        }
                        return alunos;
                    }
                }
            }
        }
        public Turma GetTurma(int turmaId)
        {
            if (turmaId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(turmaId), "Informe um ID de turma válido.");
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", turmaId);
            var query = @"SELECT id, curso_nome, turma, ano  FROM turma WHERE id = @Id";

            using (var reader = _connection.ExecuteReader(query, parameters))
            {
                if (reader.Read())
                {
                    return new Turma
                    {

                        TurmaId = Convert.ToInt32(reader["id"]),
                        TurmaNome = reader["turma"].ToString(),
                        Materia = reader["curso_nome"].ToString().Trim(),
                        Ano = Convert.ToUInt16(reader["ano"]),
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
