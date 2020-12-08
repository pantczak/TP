using Newtonsoft.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace ConsoleSerializer.Serializer
{
    public class JsonSerializer
    {
        private static JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
        };
        public static void Serialize(Stream serializationStream, object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented, JsonSettings);
            serializationStream.Write(Encoding.UTF8.GetBytes(json),0, Encoding.UTF8.GetBytes(json).Length);
        }

        public static T Deserialize<T>(Stream serializationStream)
        {
            byte[] bytes = new byte[serializationStream.Length];
            serializationStream.Read(bytes,0,bytes.Length);
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes), JsonSettings);
        }

        public static bool Validate(JSchema schema, string json)
        {
            JObject o = JObject.Parse(json);
            return o.IsValid(schema);
        }
    }
}