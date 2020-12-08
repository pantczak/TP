using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConsoleSerializer.DataModel
{
    [Serializable]
    [JsonObject]
    public class Document : ISerializable
    {
        public int NumberOfPages { get; set; }
        public string Title { get; set; }

        public Document(int numberOfPages, string title)
        {
            NumberOfPages = numberOfPages;
            Title = title;
        }

        public Document()
        {
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("NumberOfPages",NumberOfPages);
            info.AddValue("Title", Title);
        }

        public Document(SerializationInfo info, StreamingContext context)
        {
            Title = info.GetString("Title");
            NumberOfPages = info.GetInt32("NumberOfPages");
        }

        protected bool Equals(Document other)
        {
            return NumberOfPages == other.NumberOfPages && Title == other.Title;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Document) obj);
        }

    }
}