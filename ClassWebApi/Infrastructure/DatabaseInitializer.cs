namespace ClassWebApi.Infrastructure
{
    using Microsoft.Extensions.Configuration;
    using System.Data.SqlClient;
    using System.IO;

    public class DatabaseInitializer
    {
        private readonly IConfiguration _configuration;

        public DatabaseInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void InitializeDatabase()
        {
            var masterConnectionString = _configuration.GetConnectionString("MasterConnection");
            var connectionString = _configuration.GetConnectionString("SqlConnection");

            using (var masterConnection = new SqlConnection(masterConnectionString))
            {
                masterConnection.Open();

                var createDbScript = @"
                    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ClassProject') 
                    BEGIN
                      CREATE DATABASE ClassProject;
                    END;
                    ";

                using (var command = new SqlCommand(createDbScript, masterConnection))
                {
                    command.ExecuteNonQuery();
                }
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "create_database.sql");
                if (!File.Exists(scriptPath))
                {
                    throw new FileNotFoundException($"The script file '{scriptPath}' was not found.");
                }

                var scriptText = File.ReadAllText(scriptPath);

                var commands = scriptText.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var commandText in commands)
                {
                    if (!string.IsNullOrWhiteSpace(commandText))
                    {
                        using (var command = new SqlCommand(commandText, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
