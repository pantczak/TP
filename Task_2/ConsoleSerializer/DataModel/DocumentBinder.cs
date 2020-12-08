using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConsoleSerializer.DataModel
{
    [Serializable]
    [JsonObject]
    public class DocumentBinder : ISerializable
    {
        private LinkedList<Document> _documents = new  LinkedList<Document>();

        public DocumentBinder()
        {
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Documents",_documents);
        }

        public DocumentBinder(SerializationInfo info, StreamingContext context)
        {
            _documents = (LinkedList<Document>) info.GetValue("Documents",typeof(LinkedList<Document>));
        }
    }
}