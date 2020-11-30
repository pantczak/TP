using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using ConsoleSerializer.DataModel;
using ConsoleSerializer.Serializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleSerializerTests
{
    [TestClass]
    public class JsonSerializerTest
    {
        [TestMethod]
        public void TestWrite()
        {
            ISerializable objectToSerialize = new Class1("Text", DateTime.Now, 3.012, null, null);
            JsonSerializer serializer = new JsonSerializer();
            const string fileName = "testJson.json";
            JsonSerializer.Serialize(objectToSerialize,fileName);
            FileInfo info = new FileInfo(fileName);
            Assert.IsTrue(info.Exists);
            Assert.IsTrue(info.Length >=100);
            string fileContent = File.ReadAllText(fileName, Encoding.UTF8);
            Debug.Write(fileContent);
            File.Delete(fileName);
        }

        [TestMethod]
        public void TestRead()
        {
            ISerializable objectToSerialize = new Class1("Text", DateTime.Now, 3.012, null, null);
            JsonSerializer serializer = new JsonSerializer();
            const string fileName = "testJson.json";
            JsonSerializer.Serialize(objectToSerialize, fileName);
            ISerializable deserialized = JsonSerializer.Deserialize<Class1>(fileName);
            Assert.AreEqual(true,Equals(objectToSerialize, deserialized));
        }
    }
}