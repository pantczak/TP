using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using ConsoleSerializer.DataModel;

namespace ConsoleSerializer.Serializer
{
    public class MySerializer : Formatter
    {
        List<DataStruct> _values = new List<DataStruct>();
        List<ObjStruct> _objects = new List<ObjStruct>();
        List<Object> _sobjects = new List<Object>();
        Stream serializationStream;

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

            ObjStruct graphStruct = new ObjStruct(graph);
            foreach (ObjStruct ost in _objects)
            {
                if (ost.value == graph)
                {
                    graphStruct = ost;
                    break;
                }
            }

            if (!_objects.Contains(graphStruct))
            {
                _objects.Add(graphStruct);
            }

            fileContent.Append(graphStruct.guid.ToString() + "\n");
            foreach (DataStruct dataStruct in _values)
            {
                fileContent.Append(dataStruct.ToString() + "\n");
            }

            fileContent.Append("]\n");
            using (StreamWriter writer = new StreamWriter(serializationStream, Encoding.UTF8, 32, true))
            {
                writer.Write(fileContent.ToString());
            }

            fileContent.Clear();
            _values.Clear();
            _sobjects.Add(graph);
            foreach (ObjStruct objStruct in _objects)
            {
                if (!_sobjects.Contains(objStruct.value))
                {
                    this.Serialize(serializationStream, objStruct.value);
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
                _values.Add(new DataStruct("null", name, "null"));
                return;
            }

            if (memberType == typeof(String))
            {
                _values.Add(new DataStruct(memberType.Name, name, (String) obj));
                return;
            }

            bool isOnList = false;
            Guid onListGuid = new Guid();
            foreach (ObjStruct objStruct in _objects)
            {
                if (objStruct.value == obj)
                {
                    isOnList = true;
                    onListGuid = objStruct.guid;
                }
            }

            if (!isOnList)
            {
                ObjStruct objStruct = new ObjStruct(obj);
                _objects.Add(objStruct);
                onListGuid = objStruct.guid;
            }

            _values.Add(new DataStruct(memberType.Name, name, onListGuid.ToString()));
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