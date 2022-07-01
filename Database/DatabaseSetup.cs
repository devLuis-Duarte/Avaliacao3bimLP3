using Microsoft.Data.Sqlite;

namespace LabManager.Database;

class DatabaseSetup
{
    private DatabaseConfig databaseConfig;

    public DatabaseSetup (DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
        CreateProductTable();
    }

    public void CreateProductTable()
    {
        
        var conection = new SqliteConnection("Data Source=database.db");
        conection.Open();

        var command = conection.CreateCommand();
        command.CommandText = @";
        CREATE TABLE IF NOT EXISTS Products(
            id int not null primary key,
            name varchar(100) not null,
            price double(3) not null,
            active bool not null
        );
        ";
        command.ExecuteNonQuery();

        conection.Close();
    }
}