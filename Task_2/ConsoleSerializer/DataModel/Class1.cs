using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConsoleSerializer.DataModel
{
    [Serializable]
    [JsonObject]
    public class Class1 : ISerializable
    {
        public string TextData { get; set; }
        public DateTime DateTimeData { get; set; }
        public double DoubleData { get; set; }
        public Class2 Class2 { get; set; }
        public Class3 Class3 { get; set; }

        public Class1(string textData, DateTime dateTimeData, double doubleData, Class2 class2, Class3 class3)
        {
            TextData = textData;
            DateTimeData = dateTimeData;
            DoubleData = doubleData;
            Class2 = class2;
            Class3 = class3;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TextData", TextData);
            info.AddValue("DateTimeData", DateTimeData);
            info.AddValue("DoubleData", DoubleData);
            info.AddValue("Class2", Class2);
            info.AddValue("Class3", Class3);
        }
    }
}