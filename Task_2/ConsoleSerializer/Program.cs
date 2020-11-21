using System;
using BookShop.Model;

namespace ConsoleSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Available operations: ");
            Console.WriteLine("[1] Serialize \n[2] Deserialize\n[3] Serialize to JSON\n[4] Deserialize from JSON\n[5] Exit");
            int choice = 0;

            //INIT Data
            DataRepository repository = new DataRepository(new NullDataFiller());


            while(choice != 5)
            {
                choice = Console.Read() - '0';
                switch (choice)
                {
                    case 1:
                        //TODO add Serialization
                        break;
                    case 2:
                        //TODO add Deserialization
                        break;
                    case 3:
                        //TODO add JSON serialization
                        break;
                    case 4:
                        //TODO add JSON deserialization
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
