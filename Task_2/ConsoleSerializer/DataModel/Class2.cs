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
        public Class1 Class1 { get; set; }
        public Class3 Class3 { get; set; }

        public Class2(string textData, DateTime dateTimeData, Class1 class1, Class3 class3)
        {
            TextData = textData;
            DateTimeData = dateTimeData;
            Class1 = class1;
            Class3 = class3;
        }

        public Class2(string textData, DateTime dateTimeData)
        {
            TextData = textData;
            DateTimeData = dateTimeData;
        }



        protected Class2(SerializationInfo info, StreamingContext context)
        {
            TextData = info.GetString("TextData");
            DateTimeData = info.GetDateTime("DateTimeData");
            Class1 = (Class1) info.GetValue("Class1", typeof(Class1));
            Class3 = (Class3) info.GetValue("Class3", typeof(Class3));
        }

        protected bool Equals(Class2 other)
        {
            return TextData == other.TextData && DateTimeData.Equals(other.DateTimeData) &&
                   Equals(Class1, other.Class1) && Equals(Class3, other.Class3);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Class2)obj);
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TextData", TextData);
            info.AddValue("DateTimeData", DateTimeData);
            info.AddValue("Class1", Class1);
            info.AddValue("Class3", Class3);
        }
    }
}