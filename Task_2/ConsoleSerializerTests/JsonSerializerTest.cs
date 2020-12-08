using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Class1 _class1Empty;
        Class1 _class1;
        Class2 _class2;
        Class3 _class3;
        Class1 _class1Deserialized;
        Class2 _class2Deserialized;
        Class3 _class3Deserialized; 
        DocumentBinder _documentBinder;
        DocumentBinder _documentBinderDeserialized;

        [TestInitialize]
        public void TestInitialize()
        {
          
            _class1Empty = new Class1("Text", DateTime.Now, 3.012);
            _class1 = new Class1("Text", DateTime.Now, 3.012);
            _class2 = new Class2("Text2",DateTime.Now);
            _class3 = new Class3("Text3",DateTime.Now);

            _class1.Class2 = _class2;
            _class1.Class3 = _class3;

            _class2.Class1 = _class1;
            _class2.Class3 = _class3;

            _class3.Class1 = _class1;
            _class3.Class2 = _class2;

            List<Document> documents = new List<Document>{

                new Document(123, "342"),
                new Document(23, "sdsd")
            };

            _documentBinder = new DocumentBinder(documents, new string[]{"a1","A2","A3"});
        }



        [TestMethod]
        public void TestWrite()
        {
            const string fileName = "testJson.json";
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, _class1Empty);
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
                JsonSerializer.Serialize(fileStream, _class1Empty);
            }
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                 deserialized = JsonSerializer.Deserialize<Class1>(fileStream);
            }
            Assert.AreEqual(true,_class1Empty.Equals(deserialized));
        }

        [TestMethod]
        public void TestGraphSerializationClass1()
        {
            using (FileStream fileStream = new FileStream("Class1Graph.json", FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, _class1);
            }

            using (FileStream fileStream = new FileStream("Class1Graph.json", FileMode.Open))
            {
                _class1Deserialized = JsonSerializer.Deserialize<Class1>(fileStream);
            }

            Assert.IsNotNull(_class1Deserialized);
            Assert.AreNotSame(_class1, _class1Deserialized);

            Assert.AreEqual(_class1.TextData, _class1Deserialized.TextData);
            Assert.AreEqual(_class1.DateTimeData, _class1Deserialized.DateTimeData);
            Assert.AreEqual(_class1.DoubleData, _class1Deserialized.DoubleData);

            Assert.AreEqual(_class1.Class2.TextData, _class1Deserialized.Class2.TextData);
            Assert.AreEqual(_class1.Class2.DateTimeData, _class1Deserialized.Class2.DateTimeData);

            Assert.AreEqual(_class1.Class3.TextData, _class1Deserialized.Class3.TextData);
            Assert.AreEqual(_class1.Class3.DateTimeData, _class1Deserialized.Class3.DateTimeData);
        }

        [TestMethod]
        public void TestGraphSerializationClass2()
        {
            using (FileStream fileStream = new FileStream("Class2Graph.json", FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, _class2);
            }

            using (FileStream fileStream = new FileStream("Class2Graph.json", FileMode.Open))
            {
                _class2Deserialized = JsonSerializer.Deserialize<Class2>(fileStream);
            }

            Assert.IsNotNull(_class2Deserialized);
            Assert.AreNotSame(_class2, _class2Deserialized);

            Assert.AreEqual(_class2.TextData, _class2Deserialized.TextData);
            Assert.AreEqual(_class2.DateTimeData, _class2Deserialized.DateTimeData);

            Assert.AreEqual(_class2.Class1.TextData, _class2Deserialized.Class1.TextData);
            Assert.AreEqual(_class2.Class1.DateTimeData, _class2Deserialized.Class1.DateTimeData);
            Assert.AreEqual(_class2.Class1.DoubleData, _class2Deserialized.Class1.DoubleData);

            Assert.AreEqual(_class2.Class3.TextData, _class2Deserialized.Class3.TextData);
            Assert.AreEqual(_class2.Class3.DateTimeData, _class2Deserialized.Class3.DateTimeData);
        }


        [TestMethod]
        public void TestGraphSerializationClass3()
        {
            using (FileStream fileStream = new FileStream("Class3Graph.json", FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, _class3);
            }

            using (FileStream fileStream = new FileStream("Class3Graph.json", FileMode.Open))
            {
                _class3Deserialized = JsonSerializer.Deserialize<Class3>(fileStream);
            }

            Assert.IsNotNull(_class3Deserialized);
            Assert.AreNotSame(_class3, _class3Deserialized);

            Assert.AreEqual(_class3.TextData, _class3Deserialized.TextData);
            Assert.AreEqual(_class3.DateTimeData, _class3Deserialized.DateTimeData);

            Assert.AreEqual(_class3.Class1.TextData, _class3Deserialized.Class1.TextData);
            Assert.AreEqual(_class3.Class1.DateTimeData, _class3Deserialized.Class1.DateTimeData);
            Assert.AreEqual(_class3.Class1.DoubleData, _class3Deserialized.Class1.DoubleData);

            Assert.AreEqual(_class3.Class2.TextData, _class3Deserialized.Class2.TextData);
            Assert.AreEqual(_class3.Class2.DateTimeData, _class3Deserialized.Class2.DateTimeData);
        }

        [TestMethod]
        public void ListDataTest()
        {
            using (FileStream fileStream = new FileStream("ListData.json", FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, _documentBinder);
            }

            using (FileStream fileStream = new FileStream("ListData.json", FileMode.Open))
            {
                _documentBinderDeserialized = JsonSerializer.Deserialize<DocumentBinder>(fileStream);
            }

            Assert.IsNotNull(_documentBinderDeserialized);
            Assert.AreNotSame(_documentBinder, _documentBinderDeserialized);

            CollectionAssert.AreEqual(_documentBinder.Documents, _documentBinderDeserialized.Documents);
            CollectionAssert.AreEqual(_documentBinder.Aliases, _documentBinderDeserialized.Aliases);
        }


    }
}