using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;


namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }
      /*public List<Computer>GetAll()
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
    }*/
   public IEnumerable<Computer> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var computers = connection.Query<Computer>("SELECT * FROM Computers");
        return computers;
    }

    /*public Computer Save(Computer computer)
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

            return computer;
    }*/
     public Computer Save(Computer computer)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)",
        computer);
        connection.Close();
        return computer;
}
public void Delete(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Computers WHERE id = @Id;", new{Id = id});
    
        connection.Close();
    }

    public Computer Update(Computer computer)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute(@"
            UPDATE Computers 
            SET 
                ram = @Ram,
                processor = @Processor
            WHERE id = @Id;
            ", computer);

        connection.Close();

        return computer;
    }

    public Computer GetById(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var computer = connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE id = @Id;", new{Id = id});
        
        connection.Close();
        return computer;
    }

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var result = Convert.ToBoolean(connection.ExecuteScalar("SELECT count(id) FROM Computers WHERE id = @Id;", new {Id = id}));

        return result;
    }
  
}