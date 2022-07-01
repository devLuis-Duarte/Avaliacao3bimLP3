using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;


namespace LabManager.Repositories;

class ProductRepository
{
    private DatabaseConfig databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }
      public List<Product>GetAll()
    {
        var conection = new SqliteConnection("Data Source=database.db");
            conection.Open();
            
            var command = conection.CreateCommand();
            command.CommandText = "SELECT * FROM Products;";

            var reader = command.ExecuteReader();

            var products = new List<Product>();

            while(reader.Read())
            {   
                products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetBoolean(3)));
              
                
            }
            
            conection.Close();

            return products;
    }
     public Product Save(Product product)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("INSERT INTO Products VALUES(@Id, @Name, @Price, @Active)",
        product);
        connection.Close();
        return product;
}
public void Delete(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Products WHERE id = @Id;", new{Id = id});
    
        connection.Close();
    }
    public void Enable(int id){
         Product product = new Product();

         if(product.Active == false){
            product.Active = true;
         }
        
    }
    public void Disable(int id){
         Product product = new Product();

         if(product.Active == true){
            product.Active = false;
         }
    }
    public List<Product> GetAllWithPriceBetween(double initialPrice, double endPrice){
         var conection = new SqliteConnection("Data Source=database.db");
            conection.Open();
            
            var command = conection.CreateCommand();
            connection.Execute("SELECT* FROM Products WHERE (price > @InitialPrice) AND (price < @Endprice);", 
            new{InitialPrice = initialPrice, Endprice = endPrice});

            var reader = command.ExecuteReader();

            var products = new List<Product>();

            while(reader.Read())
            {   
                products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetBoolean(3)));
              
                
            }
            
            conection.Close();

            return products;
    }
    public List<Product> GetAllWithPriceHigherThan(double price){
        var conection = new SqliteConnection("Data Source=database.db");
            conection.Open();
            
            var command = conection.CreateCommand();
            connection.Execute("SELECT* FROM Products WHERE price > @Price;", 
            new{Price = price});    

            var reader = command.ExecuteReader();

            var products = new List<Product>();

            while(reader.Read())
            {   
                products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetBoolean(3)));
              
                
            }
            
            conection.Close();

            return products;
    }
    public List<Product> GetAllWithPriceLowerThan(double price){
      var conection = new SqliteConnection("Data Source=database.db");
            conection.Open();
            
            var command = conection.CreateCommand();
            connection.Execute("SELECT* FROM Products WHERE price < @Price;", 
            new{Price = price});    
            var reader = command.ExecuteReader();

            var products = new List<Product>();

            while(reader.Read())
            {   
                products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetBoolean(3)));
              
                
            }
            
            conection.Close();

            return products;   
    }
    public double GetAveragePrice(){
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var result = Convert.ToDouble(connection.ExecuteScalar("SELECT avg(price) FROM Products;"));

        return result;
    }

 

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var result = Convert.ToBoolean(connection.ExecuteScalar("SELECT count(id) FROM Products WHERE id = @Id;", new {Id = id}));

        return result;
    }
  
}