using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ConsoleSerializer.DataModel;

namespace ConsoleSerializer.Serializer
{
    public class MySerializer : Formatter
    {

        List<DataStruct> _values = new List<DataStruct>();
        List<Object> _objects = new List<Object>();
        List<Object> _sobjects = new List<Object>();
        List<string> readObjectsList = new List<string>();
        Stream serializationStream;
        ObjectIDGenerator objectIDGenerator = new ObjectIDGenerator();
        CustomBinder customBinder = new CustomBinder();
        Dictionary<string, object> ReferencesDictionary = new Dictionary<string, object>();

        public override object Deserialize(Stream serializationStream)
        {
            List<Object> returnObjects = new List<object>();
            readObjectsList = new List<string>();
            if (serializationStream != null)
            {
                string readData;
                using (StreamReader reader = new StreamReader(serializationStream, Encoding.UTF8, false, 32, true))
                {
                    // Wczytuję wszystkie dane
                    readData = reader.ReadToEnd();
                    //Dzielę na zbiory właściwości poszczegolnych obiektów
                    string[] objectsData = readData.Split(';');
                    foreach (string objectData in objectsData)
                    {
                        readObjectsList.Add(objectData);
                    }
                }

                foreach (string data in readObjectsList)
                {
                    //Rozdzielam na poszczególne właściwości obiektu
                    String[] objectStrings = data.Split('\n');
                    if (objectStrings[0] == "")
                    {
                        List<String> buff = objectStrings.OfType<String>().ToList();
                        buff.RemoveAt(0);
                        objectStrings = buff.ToArray();
                    }

                    //Rozdzielam na informacje o poszczególnej właściwości
                    string[] objAtr = objectStrings[0].Split('|');
                    if (objAtr.Length != 3)
                    {
                        continue;
                    }

                    //Zapisuję do Słownika obiekt pod referencją do niego
                    ReferencesDictionary.Add(objAtr[2],
                        FormatterServices.GetSafeUninitializedObject(customBinder.BindToType(objAtr[0], objAtr[1])));
                }

                foreach (string data in readObjectsList)
                {
                    //Rozdzielam na poszczególne właściwości obiektu
                    String[] objectStrings = data.Split('\n');
                    if (objectStrings[0] == "")
                    {
                        List<String> buff = objectStrings.OfType<String>().ToList();
                        buff.RemoveAt(0);
                        objectStrings = buff.ToArray();
                    }

                    //Rozdzielam na informacje o poszczególnej właściwości
                    string[] objAtr = objectStrings[0].Split('|');
                    if (objAtr.Length != 3)
                    {
                        continue;
                    }

                    // Tworzę obiekt przechowujący typ danego obiektu
                    Type deserializedObjType = customBinder.BindToType(objAtr[0], objAtr[1]);
                    // Tworzę nowe SerializationInfo i contex
                    SerializationInfo info = new SerializationInfo(deserializedObjType, new FormatterConverter());
                    StreamingContext _context = new StreamingContext(StreamingContextStates.File);

                    // Wczytuję informacje do SerializationInfo
                    for (int i = 1; i < objectStrings.Length; i++)
                    {
                        string[] objAtrs = objectStrings[i].Split('|');
                        //zwracam typ danego atrybutu obiektu
                        Type atrType = customBinder.BindToType(objAtr[0], objAtrs[0]);
                        // sprawdzam czy typ obiektu nie jest nullem
                        if (atrType == null)
                        {
                            //sprawdzam czy powinien być nullem
                            if (!objAtrs[0].Equals("null"))
                            {
                                SaveValueToSerialInfo(info, Type.GetType(objAtrs[0]), objAtrs[1], objAtrs[2]);
                            }
                            else
                            {
                                info.AddValue(objAtrs[1], null);
                            }
                        }
                        // jak tu doszło to jest referencją
                        else
                        {
                            info.AddValue(objAtrs[1], ReferencesDictionary[objAtrs[2]], atrType);
                        }
                    }

                    // Tworzę listę typów parametrów konstruktora obiektu
                    Type[] constructorTypes = {info.GetType(), _context.GetType()};
                    // tworzę listę argumentów konstruktora
                    object[] constructorArguments = {info, _context};
                    // tworzę obiekt
                    ReferencesDictionary[objAtr[2]].GetType().GetConstructor(constructorTypes)
                        .Invoke(ReferencesDictionary[objAtr[2]], constructorArguments);
                    returnObjects.Add(ReferencesDictionary[objAtr[2]]);
                }
            }

            return returnObjects[0];
        }

        private void SaveValueToSerialInfo(SerializationInfo info, Type type, string name, string val)
        {
            if (type == null)
            {
                info.AddValue(name, null);
                return;
            }

            switch (type.ToString())
            {
                case "System.DateTime":
                    info.AddValue(name, DateTime.Parse(val, null, DateTimeStyles.AssumeLocal));
                    break;
                case "System.String":
                    info.AddValue(name, val);
                    break;
                case "System.Double":
                    info.AddValue(name, Double.Parse(val, CultureInfo.InvariantCulture));
                    break;
                case "System.Int32":
                    info.AddValue(name, Int32.Parse(val, CultureInfo.InvariantCulture));
                    break;
            }
        }

        public override void Serialize(Stream serializationStream, object graph)
        {
            ISerializable _data = (ISerializable) graph;
            this.serializationStream = serializationStream;
            SerializationInfo _info = new SerializationInfo(graph.GetType(), new FormatterConverter());
            customBinder.BindToName(graph.GetType(), out string assemblyName, out string typeName);
            StreamingContext _context = new StreamingContext(StreamingContextStates.File);
            _data.GetObjectData(_info, _context);

            foreach (SerializationEntry _item in _info)
            {
                this.WriteMember(_item.Name, _item.Value);
            }


            StringBuilder fileContent = new StringBuilder(assemblyName + "|" + typeName + "|" +
                                                          objectIDGenerator.GetId(graph, out bool firstTime)
                                                              .ToString());
            foreach (DataStruct dataStruct in _values)
            {
                fileContent.Append("\n" + dataStruct.ToString());
            }

            fileContent.Append(";\n");
            using (StreamWriter writer = new StreamWriter(serializationStream, Encoding.UTF8, 32, true))
            {
                writer.Write(fileContent.ToString());
            }

            fileContent.Clear();
            _values.Clear();
            _sobjects.Add(graph);
            foreach (Object obj in _objects)
            {
                if (!_sobjects.Contains(obj))
                {
                    this.Serialize(serializationStream, obj);
                }
            }

            //serializationStream.Close();
            // _sobjects.Clear();
        }

        public void Serialize(Stream serializationStream, DocumentBinder documentBinder)
        {
            ObjectIDGenerator generator = new ObjectIDGenerator();
            StringBuilder fileContent = new StringBuilder();
            foreach (Document doc in documentBinder.Documents)
            {
                Serialize(serializationStream, doc);
            }

            foreach (Alias alias in documentBinder.Aliases)
            {
                Serialize(serializationStream, alias);
            }
        }
        public override ISurrogateSelector SurrogateSelector { get; set; }
        public override SerializationBinder Binder { get; set; }
        public override StreamingContext Context { get; set; }

        protected override void WriteArray(object obj, string name, Type memberType)
        {
            long id = objectIDGenerator.GetId(obj, out bool firstTime);
            if (firstTime)
            {
                _objects.Add(obj);
            }

            _values.Add(new DataStruct(memberType.Name, name, id.ToString()));
            return;
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
            _values.Add(new DataStruct(val.GetType().ToString(), name, val.ToLocalTime().ToString("O")));
        }

        protected override void WriteDecimal(decimal val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDouble(double val, string name)
        {
            _values.Add(new DataStruct(val.GetType().ToString(), name, val.ToString(CultureInfo.InvariantCulture)));
        }

        protected override void WriteInt16(short val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt32(int val, string name)
        {
            _values.Add(new DataStruct(val.GetType().ToString(), name, val.ToString()));
        }

        protected override void WriteInt64(long val, string name)
        {
            _values.Add(new DataStruct(val.GetType().ToString(), name, val.ToString()));
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
                _values.Add(new DataStruct(obj.GetType().ToString(), name, (String) obj));
                return;
            }

            long id = objectIDGenerator.GetId(obj, out bool firstTime);
            if (firstTime)
            {
                _objects.Add(obj);
            }

            _values.Add(new DataStruct(memberType.FullName, name, id.ToString()));
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