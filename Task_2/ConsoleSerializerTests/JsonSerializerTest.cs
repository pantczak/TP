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
        Class1 class1Empty;
        Class1 class1;
        Class2 class2;
        Class3 class3;
        Class1 class1Deserialized;
        Class2 class2Deserialized;
        Class3 class3Deserialized;

        [TestInitialize]
        public void TestInitialize()
        {
          
            class1Empty = new Class1("Text", DateTime.Now, 3.012);
            class1 = new Class1("Text", DateTime.Now, 3.012);
            class2 = new Class2("Text2",DateTime.Now);
            class3 = new Class3("Text3",DateTime.Now);

            class1.Class2 = class2;
            class1.Class3 = class3;

            class2.Class1 = class1;
            class2.Class3 = class3;

            class3.Class1 = class1;
            class3.Class2 = class2;
        }



        [TestMethod]
        public void TestWrite()
        {
            const string fileName = "testJson.json";
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, class1Empty);
            }
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
            const string fileName = "testJson.json";
            Class1 deserialized;
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, class1Empty);
            }
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                 deserialized = JsonSerializer.Deserialize<Class1>(fileStream);
            }
            Assert.AreEqual(true,class1Empty.Equals(deserialized));
        }

        [TestMethod]
        public void TestGraphSerializationClass1()
        {
            using (FileStream fileStream = new FileStream("Class1Graph.json", FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, class1);
            }

            using (FileStream fileStream = new FileStream("Class1Graph.json", FileMode.Open))
            {
                class1Deserialized = JsonSerializer.Deserialize<Class1>(fileStream);
            }

            Assert.IsNotNull(class1Deserialized);
            Assert.AreNotSame(class1, class1Deserialized);

            Assert.AreEqual(class1.TextData, class1Deserialized.TextData);
            Assert.AreEqual(class1.DateTimeData, class1Deserialized.DateTimeData);
            Assert.AreEqual(class1.DoubleData, class1Deserialized.DoubleData);

            Assert.AreEqual(class1.Class2.TextData, class1Deserialized.Class2.TextData);
            Assert.AreEqual(class1.Class2.DateTimeData, class1Deserialized.Class2.DateTimeData);

            Assert.AreEqual(class1.Class3.TextData, class1Deserialized.Class3.TextData);
            Assert.AreEqual(class1.Class3.DateTimeData, class1Deserialized.Class3.DateTimeData);
        }

        [TestMethod]
        public void Class1With2and3TestWrite()
        {
            Class2 class2 = new Class2("Text", DateTime.Now, 17, null, null);
            Class3 class3 = new Class3("Text", DateTime.Now, 192834, null, null);
            ISerializable objectToSerialize = new Class1("Text", DateTime.Now, 3.012, class2, class3);
            JsonSerializer serializer = new JsonSerializer();
            const string fileName = "testJson.json";
            JsonSerializer.Serialize(objectToSerialize, fileName);
            FileInfo info = new FileInfo(fileName);
            Assert.IsTrue(info.Exists);
            Assert.IsTrue(info.Length >= 100);
            string fileContent = File.ReadAllText(fileName, Encoding.UTF8);
            Debug.Write(fileContent);
            File.Delete(fileName);
        }

        [TestMethod]
        public void Class1With2and3TestRead()
        {
            Class2 class2 = new Class2("Text", DateTime.Now, 17, null, null);
            Class3 class3 = new Class3("Text", DateTime.Now, 192834, null, null);
            ISerializable objectToSerialize = new Class1("Text", DateTime.Now, 3.012, class2, class3);
            JsonSerializer serializer = new JsonSerializer();
            const string fileName = "testJson.json";
            JsonSerializer.Serialize(objectToSerialize, fileName);
            ISerializable deserialized = JsonSerializer.Deserialize<Class1>(fileName);
            Assert.AreEqual(true, Equals(objectToSerialize, deserialized));
        }


    }
}