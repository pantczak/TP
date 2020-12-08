using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConsoleSerializer.DataModel
{
    [Serializable]
    [JsonObject]
    public class DocumentBinder : ISerializable
    {
        public ObservableCollection<Document> Documents { get; set; }

         public DocumentBinder(ObservableCollection<Document> documents)
         {
             Documents = documents;
         }

         public DocumentBinder()
        {
        }

        protected bool Equals(DocumentBinder other)
        {
            return Equals(Documents, other.Documents);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DocumentBinder) obj);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Documents",Documents);
        }

        public DocumentBinder(SerializationInfo info, StreamingContext context)
        {
            Documents = (ObservableCollection<Document>) info.GetValue("Documents",typeof(ObservableCollection<Document>));
        }
    }
}