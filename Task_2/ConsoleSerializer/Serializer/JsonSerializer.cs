using System;
using System.IO;
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
            File.Delete(filePath);
            using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                string serialized =
                    JsonConvert.SerializeObject(obj, Formatting.Indented, JsonSerializer.JsonSettings);
                // stream.Write(Encoding.UTF8.GetBytes(serialized));
                stream.Flush();
            }
        }
    }
}