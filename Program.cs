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

    var computerRepository = new ComputerRepository(databaseConfig);

    //Routing -- Roteamento


    var modelName = args[0];
    var modelAction = args[1];

    if(modelName == "Computer")
    {
        if(modelAction == "List")
        {
           Console.WriteLine("Computer List");
           foreach(var computer in computerRepository.GetAll())
           {
               Console.WriteLine("{0},{1},{2}", computer.Id, computer.Ram, computer.Processor);
           }
        }
        if(modelAction == "New"){
           

            Console.WriteLine("New computer");
            int id = Convert.ToInt32(args[2]);
            string ram = args[3];
            string processor = args[4];

            var computer = new Computer(id, ram, processor);
            computerRepository.Save(computer);


        }
        if(modelAction == "Delete")
    {
        Console.WriteLine("Computer Delete");
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            computerRepository.Delete(id);
        }
        else {
            Console.WriteLine($"Computador com id {id} não existe");
        }
    }
        
    if(modelAction == "Update")
    {
        Console.WriteLine("Computer Update");
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            var ram = args[3];
            var processor = args[4];
            var computer = new Computer(id, ram, processor);

            computerRepository.Update(computer);
        }
        else {
            Console.WriteLine($"Computador com id {id} não existe");
        }
    }

    if(modelAction == "Show")
    {
        Console.WriteLine("Computer Show");
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            var computer = computerRepository.GetById(id);
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        }
        else {
            Console.WriteLine($"O computador com id {id} não existe");
        }
    }
}
    