using System;
using ConsoleSerializer.DataModel;

namespace ConsoleSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Available operations: ");
            Console.WriteLine(
                "[1] Serialize graph \n[2] Deserialize graph \n[3] Serialize graph to JSON\n[4] Deserialize graph from JSON\n[5] Exit");
            int choice = 0;

            Class1 class1 = new Class1("Text", DateTime.Now, 5.5);
            Class2 class2 = new Class2("Text2", DateTime.Now);
            Class3 class3 = new Class3("Text3", DateTime.Now);

            class1.Class2 = class2;
            class1.Class3 = class3;

            class3.Class1 = class1;
            class3.Class2 = class2;

            class2.Class1 = class1;
            class2.Class3 = class3;


            while (choice != 5)
            {
                choice = Console.Read() - '0';
                switch (choice)
                {
                    case 1:
                        break;
                    case 2:
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