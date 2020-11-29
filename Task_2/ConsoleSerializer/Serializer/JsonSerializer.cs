using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleSerializer.Serializer
{
    public class JsonSerializer
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        public static void Serialize(Object obj, string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    string json = JsonConvert.SerializeObject(obj, Formatting.Indented, JsonSerializer.JsonSettings);
                    streamWriter.WriteLine(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
                throw;
            }
        }

        public static T Deserialize<T>(string filePath)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    String jsonString = streamReader.ReadToEnd();
                    jsonString = jsonString.Remove(jsonString.Length - 2);
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
                throw;
            }
        }
    }
}