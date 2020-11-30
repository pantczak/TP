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
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.Auto,
        };

        public static void Serialize(Object obj, string filePath)
        {
            try
            {
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    //File.Delete(filePath);
                    string json = JsonConvert.SerializeObject(obj, Formatting.Indented, JsonSettings);
                    fileStream.Write(Encoding.UTF8.GetBytes(json), 0, Encoding.UTF8.GetByteCount(json));
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
                using (Stream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    byte[] bytes = new byte[fileStream.Length];
                    fileStream.Read(bytes,0,bytes.Length);
                    return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes), JsonSettings);
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