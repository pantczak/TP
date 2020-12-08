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

        public Class1(string textData, DateTime dateTimeData, double doubleData)
        {
            TextData = textData;
            DateTimeData = dateTimeData;
            DoubleData = doubleData;
        }

        public Class1()
        {
        }

        protected bool Equals(Class1 other)
        {
            return TextData == other.TextData && DateTimeData.Equals(other.DateTimeData) &&
                   DoubleData.Equals(other.DoubleData) && Equals(Class2, other.Class2) && Equals(Class3, other.Class3);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Class1) obj);
        }

        public Class1(SerializationInfo info, StreamingContext context)
        {
            TextData = info.GetString("TextData");
            DateTimeData = info.GetDateTime("DateTimeData");
            DoubleData = info.GetDouble("DoubleData");
            Class2 = (Class2) info.GetValue("Class2", typeof(Class2));
            Class3 = (Class3) info.GetValue("Class3", typeof(Class3));
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