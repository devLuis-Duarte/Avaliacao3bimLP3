// See https://aka.ms/new-console-template for more information
using LabManager.Database;
using LabManager.Models;
using LabManager.Repositories;
using Microsoft.Data.Sqlite;

Console.WriteLine(args);

foreach (var arg in args)
{
    Console.WriteLine(arg);
}
    var databaseConfig = new DatabaseConfig();
    new DatabaseSetup(databaseConfig);

    var productRepository = new ProductRepository(databaseConfig);

    //Routing -- Roteamento


    var modelName = args[0];
    var modelAction = args[1];

    if(modelName == "Product")
    {
        if(modelAction == "List")
        {
           Console.WriteLine("Product List");
           foreach(var product in  productRepository.GetAll())
           {
               Console.WriteLine("{0},{1},{2},{3}", product.Id, product.Name, product.Price, product.Active);
           }
        }
        if(modelAction == "New"){
           
            int id = Convert.ToInt32(args[2]);
            string name = args[3];
            double price = Convert.ToDouble(args[4]);
            bool active = Convert.ToBoolean(args[5]);

             if(productRepository.ExistsById(id))
            {
            Console.WriteLine($"Produto com {id} já existe");
            }

            var product = new Product(id, name, price, active);
            Console.WriteLine($"Produto {name} cadastrado com sucesso");
            productRepository.Save(product);


        }
        if(modelAction == "Delete")
    {
        Console.WriteLine("Product Delete");
        var id = Convert.ToInt32(args[2]);

        if(productRepository.ExistsById(id))
        {
            productRepository.Delete(id);
            Console.WriteLine($"Produto com {id} removido com sucesso");
        }
        else {
            Console.WriteLine($"Produto com {id} não encontrado");
        }
    }
    if(modelAction == "Enable")
    {
        var id = Convert.ToInt32(args[2]);

         if(productRepository.ExistsById(id))
        {
            productRepository.Enable(id);
            Console.WriteLine($"Produto id {id} habilitado com sucesso");
        }
        else {
            Console.WriteLine($"Producto id {id} não encontrado");
        }
    }

     if(modelAction == "Disable")
    {
        Console.WriteLine("Product Disable");
        var id = Convert.ToInt32(args[2]);

         if(productRepository.ExistsById(id))
        {
            productRepository.Disable(id);
            Console.WriteLine($"Produto id {id} desabilitado com sucesso");
        }
        else {
            Console.WriteLine($"Produto id {id} não encontrado");
        }
    }
    if(modelAction ==  "Product PriceBetween"){
            var id = Convert.ToInt32(args[2]);

            if(productRepository.ExistsById)
            {
               productRepository.GetAllWithPriceBetween(id);
            }
            else {
                Console.WriteLine("Nenhum produto encontrado dentro do intervalo de preço"); 
            }
    }
    if(modelAction == "Product PriceHigherThan"){
            var id = Convert.ToInt32(args[2]);

            if(productRepository.ExistsById)
            {
               productRepository.GetAllWithPriceHigherThan(id);
            }
            else {
                Console.WriteLine("Nenhum produto encontrado com preço maior que"); 
            }
    }
     if(modelAction == "Product PriceLowerThan"){
            var id = Convert.ToInt32(args[2]);

            if(productRepository.ExistsById)
            {
               productRepository.GetAllWithPriceLowerThan(id);
            }
            else {
                Console.WriteLine("Nenhum produto encontrado com preço menor que"); 
            }
    }
    if(modelAction == "Product AveragePrice"){
            var id = Convert.ToInt32(args[2]);

            if(productRepository.ExistsById)
            {
               productRepository.GetAveragePrice();
            }
            else {
                Console.WriteLine("Nenhum produto cadastrado"); 
            }
    }
    

        
    
}
    