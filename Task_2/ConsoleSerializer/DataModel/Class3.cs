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

        protected Class3(SerializationInfo info, StreamingContext context)
        {
            TextData = info.GetString("TextData");
            DateTimeData = info.GetDateTime("DateTimeData");
            LongData = info.GetInt64("LongData");
            Class2 = (Class2) info.GetValue("Class2", typeof(Class2));
            Class1 = (Class1) info.GetValue("Class1", typeof(Class1));
        }

        protected bool Equals(Class3 other)
        {
            return TextData == other.TextData && DateTimeData.Equals(other.DateTimeData) &&
                   LongData.Equals(other.LongData) && Equals(Class2, other.Class2) && Equals(Class1, other.Class1);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Class3)obj);
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