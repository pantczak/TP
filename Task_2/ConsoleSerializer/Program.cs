using ConsoleSerializer.DataModel;
using ConsoleSerializer.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace ConsoleSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            MySerializer serializer = new MySerializer();

            Console.WriteLine("Available operations: ");
            Console.WriteLine(
                "[1] Serialize graph \n[2] Deserialize graph\n" +
                "[3] Serialize list \n[4] Deserialize list\n" +
                "[5] Validate JSON (graph)\n[6] Serialize graph to JSON\n[7] Deserialize graph from JSON\n" +
                "[8] Validate JSON (List)\n[9] Serialize List to JSON\n[10] Deserialize List from JSON\n[11] Exit");
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

            List<Document> documents = new List<Document>
            {
                new Document(123, "342"),
                new Document(23, "sdsd")
            };

            DocumentBinder binder = new DocumentBinder(documents, new Alias[] {new Alias("A1"), new Alias("A2")});

            while (choice != 11)
            {
                choice = Console.Read() - '0';
                switch (choice)
                {
                    case 1:
                        using (FileStream fileStream = new FileStream("serializationGraph.txt", FileMode.Create))
                        {
                            serializer.Serialize(fileStream, class1);
                            Console.WriteLine("Object serialized");
                            Console.WriteLine("Path: " + Directory.GetCurrentDirectory());
                        }

                        break;
                    case 2:
                        using (FileStream fileStream = new FileStream("serializationGraph.txt", FileMode.Open))
                        {
                            serializer.Deserialize(fileStream);
                            Console.WriteLine("Object deserialized");
                        }

                        break;
                    case 3:
                        using (FileStream fileStream = new FileStream("serializationList.txt", FileMode.Create))
                        {
                            serializer.Serialize(fileStream, binder);
                            Console.WriteLine("Object serialized");
                            Console.WriteLine("Path: " + Directory.GetCurrentDirectory());
                        }

                        break;
                    case 4:
                        using (FileStream fileStream = new FileStream("serializationList.txt", FileMode.Open))
                        {
                            serializer.Deserialize(fileStream);
                            Console.WriteLine("Object deserialized");
                        }

                        break;
                    case 5:
                        using (FileStream fileStream = new FileStream("serializationGraph.json", FileMode.Open))
                        {
                            string json;
                            using (StreamReader reader = new StreamReader(fileStream))
                            {
                                json = reader.ReadToEnd();
                            }

                            JSchemaGenerator generator = new JSchemaGenerator();
                            Console.WriteLine(JsonSerializer.Validate(generator.Generate(typeof(Class1)), json)
                                ? "Valid"
                                : "InValid");
                        }

                        break;
                    case 6:
                        using (FileStream fileStream = new FileStream("serializationGraph.json", FileMode.Create))
                        {
                            JsonSerializer.Serialize(fileStream, class1);
                            Console.WriteLine("Object serialized");
                            Console.WriteLine("Path: " + Directory.GetCurrentDirectory());
                        }

                        break;
                    case 7:
                        using (FileStream fileStream = new FileStream("serializationGraph.json", FileMode.Open))
                        {
                            Class1 deserialize = JsonSerializer.Deserialize<Class1>(fileStream);
                            Console.WriteLine("Object deserialized");
                        }

                        break;
                    case 8:
                        using (FileStream fileStream = new FileStream("serializationList.json", FileMode.Open))
                        {
                            string json;
                            using (StreamReader reader = new StreamReader(fileStream))
                            {
                                json = reader.ReadToEnd();
                            }

                            JSchemaGenerator generator = new JSchemaGenerator();
                            Console.WriteLine(JsonSerializer.Validate(generator.Generate(typeof(DocumentBinder)), json)
                                ? "Valid"
                                : "InValid");
                        }

                        break;
                    case 9:
                        using (FileStream fileStream = new FileStream("serializationList.json", FileMode.Create))
                        {
                            JsonSerializer.Serialize(fileStream, binder);
                            Console.WriteLine("Object serialized");
                            Console.WriteLine("Path: " + Directory.GetCurrentDirectory());
                        }

                        break;
                    case 10:
                        using (FileStream fileStream = new FileStream("serializationList.json", FileMode.Open))
                        {
                            DocumentBinder deserialize = JsonSerializer.Deserialize<DocumentBinder>(fileStream);
                            Console.WriteLine("Object deserialized");
                        }

                        break;
                    case 11:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}