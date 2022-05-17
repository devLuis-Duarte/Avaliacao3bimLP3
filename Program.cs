// See https://aka.ms/new-console-template for more information
using LabManager.Database;
using Microsoft.Data.Sqlite;

Console.WriteLine(args);

foreach (var arg in args)
{
    Console.WriteLine(arg);
}
    var databaseConfig = new DatabaseConfig();
    new DatabaseSetup(databaseConfig);

    //Routing -- Roteamento


    var modelName = args[0];
    var modelAction = args[1];

    if(modelName == "Computer"){
        if(modelAction == "List"){
            var conection = new SqliteConnection("Data Source=database.db");
            conection.Open();
            
            var command = conection.CreateCommand();
            command.CommandText = "SELECT * FROM Computers;";

            var reader = command.ExecuteReader();

            while(reader.Read())
            {   
                Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                
            }
            reader.Close();
            conection.Close();

            Console.WriteLine("List Computer");
        }
        if(modelAction == "New"){
            var conection = new SqliteConnection("Data Source=database.db");
            conection.Open();

            Console.WriteLine("New computer");
            int id = Convert.ToInt32(args[2]);
            string ram = args[3];
            string processor = args[4];

            var command = conection.CreateCommand();
            command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor)";
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$ram", ram);
            command.Parameters.AddWithValue("$processor",processor);
            command.ExecuteNonQuery();

            conection.Close();

            Console.WriteLine("{0}, {1}, {2}", id, ram, processor);
        }
    }