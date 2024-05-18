
-- Seleciona o banco de dados
USE ClassProject;

-- Cria a tabela 'aluno'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aluno]') AND type in (N'U'))
BEGIN
  CREATE TABLE aluno (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nome NVARCHAR(100) NOT NULL,
    usuario NVARCHAR(50) NOT NULL,
    senha NVARCHAR(64) NOT NULL
  )
END;

-- Cria a tabela 'turma'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[turma]') AND type in (N'U'))
BEGIN
    CREATE TABLE turma (
        id INT IDENTITY(1,1) PRIMARY KEY,
        curso_nome NVARCHAR(100) NOT NULL,
        turma NVARCHAR(100) NOT NULL,
        ano INT NOT NULL
    )
END

-- Cria a tabela 'aluno_turma'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aluno_turma]') AND type in (N'U'))
BEGIN
    CREATE TABLE aluno_turma (
        aluno_id INT NOT NULL,
        turma_id INT NOT NULL,
    )
END

