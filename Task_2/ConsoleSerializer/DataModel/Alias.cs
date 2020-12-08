using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConsoleSerializer.DataModel
{
    [Serializable]
    [JsonObject]
    public class Alias : ISerializable
    {
        public string Name { get; set; }

        public Alias(string name)
        {
            Name = name;
        }
        public Alias()
        {
        }

        protected bool Equals(Alias other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Alias) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name",Name);
        }

        public Alias(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
        }
    }
}