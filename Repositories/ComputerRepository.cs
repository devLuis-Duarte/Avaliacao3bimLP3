using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }
    public List<Computer>GetAll()
    {
        var conection = new SqliteConnection("Data Source=database.db");
            conection.Open();
            
            var command = conection.CreateCommand();
            command.CommandText = "SELECT * FROM Computers;";

            var reader = command.ExecuteReader();

            var computers = new List<Computer>();

            while(reader.Read())
            {   
                computers.Add(new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
              
                
            }
            
            conection.Close();

            return computers;
    }
    public void Save(Computer computer)
    {
        var conection = new SqliteConnection("databaseConfig.ConnectionString");
            conection.Open();

            Console.WriteLine("New computer");
  

            var command = conection.CreateCommand();
            command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor)";
            command.Parameters.AddWithValue("$id", computer.Id);
            command.Parameters.AddWithValue("$ram", computer.Ram);
            command.Parameters.AddWithValue("$processor",computer.Processor);
            command.ExecuteNonQuery();

            conection.Close();
    }
  
}