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
        public List<Document> Documents { get; set; }
        public Alias[] Aliases { get; set; }

         public DocumentBinder(List<Document> documents, Alias[] aliases)
         {
             Documents = documents;
             Aliases = aliases;
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
            for(int i=0;i<Documents.Count;i++)
            {
                info.AddValue("Documents"+i.ToString(), Documents[i]);
            }
            for (int i = 0; i < Aliases.Length; i++)
            {
                info.AddValue("Aliases" + i.ToString(), Aliases[i]);
            }
            info.AddValue("DocLen",Documents.Count);
            info.AddValue("AliasLen", Aliases.Length);
        }

        public DocumentBinder(SerializationInfo info, StreamingContext context)
        {
            int DocLen = (int)info.GetValue("DocLen", typeof(int));
            int AliasLen = (int)info.GetValue("AliasLen", typeof(int));
            Documents = new List<Document>();
            List<Alias> AliasesList = new List<Alias>();
            for (int i = 0; i < DocLen; i++)
            {
                Documents.Add((Document)info.GetValue("Documents" + i.ToString(), typeof(Document)));
            }
            for (int i = 0; i < AliasLen; i++)
            {
                AliasesList.Add((Alias)info.GetValue("Aliases" + i.ToString(), typeof(Alias)));
            }
            Aliases = AliasesList.ToArray();


        }
    }
}