using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConsoleSerializer.DataModel
{
    [Serializable]
    [JsonObject]
    public class Class2 : ISerializable
    {
        public string TextData { get; set; }
        public DateTime DateTimeData { get; set; }
        public int IntegerData { get; set; }
        public Class1 Class1 { get; set; }
        public Class3 Class3 { get; set; }

        public Class2(string textData, DateTime dateTimeData, int integerData, Class1 class1, Class3 class3)
        {
            TextData = textData;
            DateTimeData = dateTimeData;
            IntegerData = integerData;
            Class1 = class1;
            Class3 = class3;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TextData", TextData);
            info.AddValue("DateTimeData", DateTimeData);
            info.AddValue("IntegerData", IntegerData);
            info.AddValue("Class1", Class1);
            info.AddValue("Class3", Class3);
        }
    }
}