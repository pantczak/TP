using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ConsoleSerializer.DataModel;
using Newtonsoft.Json;

namespace ConsoleSerializer.Serializer
{
    public class MySerializer : Formatter
    {
        List<DataStruct> _values = new List<DataStruct>();
        List<Object> _objects = new List<Object>();
        List<Object> _sobjects = new List<Object>();
        Stream serializationStream;
        ObjectIDGenerator objectIDGenerator = new ObjectIDGenerator();
        public override object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(Stream serializationStream, object graph)
        {
            ISerializable _data = (ISerializable) graph;
            this.serializationStream = serializationStream;
            SerializationInfo _info = new SerializationInfo(graph.GetType(), new FormatterConverter());
            StreamingContext _context = new StreamingContext(StreamingContextStates.File);
            _data.GetObjectData(_info, _context);
             foreach (SerializationEntry _item in _info)
             {
                 this.WriteMember(_item.Name, _item.Value);
            }
            StringBuilder fileContent = new StringBuilder("[");
            fileContent.Append(graph.GetType().Name+" ");
            fileContent.Append(objectIDGenerator.GetId(graph, out bool firstTime).ToString());
            fileContent.Append("\n");
            foreach (DataStruct dataStruct in _values)
            {
                fileContent.Append(dataStruct.ToString() + "\n");
            }
            fileContent.Append("]\n");
            using (StreamWriter writer = new StreamWriter(serializationStream, Encoding.UTF8,32, true))
            {
                writer.Write(fileContent.ToString());
            }
            fileContent.Clear();
            _values.Clear();
            _sobjects.Add(graph);
            foreach (Object obj in _objects)
            {
                if(!_sobjects.Contains(obj))
                    {
                    this.Serialize(serializationStream,obj);

                }
            }
            serializationStream.Close();






        }

        public override ISurrogateSelector SurrogateSelector { get; set; }
        public override SerializationBinder Binder { get; set; }
        public override StreamingContext Context { get; set; }

        protected override void WriteArray(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }

        protected override void WriteBoolean(bool val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteByte(byte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteChar(char val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDateTime(DateTime val, string name)
        {
            _values.Add(new DataStruct("DateTime", name, val.ToString()));
        }

        protected override void WriteDecimal(decimal val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDouble(double val, string name)
        {
            _values.Add(new DataStruct("double", name, val.ToString()));
        }

        protected override void WriteInt16(short val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt32(int val, string name)
        {
            _values.Add(new DataStruct("int32", name, val.ToString()));
        }

        protected override void WriteInt64(long val, string name)
        {
            _values.Add(new DataStruct("long", name, val.ToString()));
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {
            if (obj == null)
            {
                _values.Add(new DataStruct("null", name,"null"));
                return;
            }
            if (memberType == typeof(String))
            {
                _values.Add(new DataStruct(memberType.Name, name,(String)obj));
                return;

            }
            long id=objectIDGenerator.GetId(obj, out bool firstTime);
            if (firstTime)
            {
                _objects.Add(obj);
            }
            _values.Add(new DataStruct(memberType.Name, name, id.ToString()));
            return;

        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteTimeSpan(TimeSpan val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt16(ushort val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt32(uint val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt64(ulong val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteValueType(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }
    }
}