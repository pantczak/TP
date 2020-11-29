using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConsoleSerializer.DataModel
{
    [Serializable]
    [JsonObject]
    public class Class3 : ISerializable
    {
        public string TextData { get; set; }
        public DateTime DateTimeData { get; set; }
        public long LongData { get; set; }
        public Class1 Class1 { get; set; }
        public Class2 Class2 { get; set; }

        public Class3(string textData, DateTime dateTimeData, long longData, Class1 class1, Class2 class2)
        {
            TextData = textData;
            DateTimeData = dateTimeData;
            LongData = longData;
            Class1 = class1;
            Class2 = class2;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TextData", TextData);
            info.AddValue("DateTimeData", DateTimeData);
            info.AddValue("LongData", LongData);
            info.AddValue("Class1", Class1);
            info.AddValue("Class2", Class2);
        }
    }
}