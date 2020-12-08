using ConsoleSerializer.DataModel;
using ConsoleSerializer.Serializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleSerializerTests
{
    [TestClass]
    public class MySerializerTest
    {
        Class1 _class1;
        Class2 _class2;
        Class3 _class3;
        Class1 _class1Deserialized;
        Class2 _class2Deserialized;
        Class3 _class3Deserialized;
        DocumentBinder _documentBinder;
        DocumentBinder _documentBinderDeserialized;
        MySerializer serializer = new MySerializer();

        [TestInitialize]
        public void TestInitialize()
        {
            _class1 = new Class1("Text", DateTime.Now, 3.012);
            _class2 = new Class2("Text2", DateTime.Now);
            _class3 = new Class3("Text3", DateTime.Now);

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

            _documentBinder = new DocumentBinder(documents, new string[] { "a1", "A2", "A3" });
        }

        [TestMethod]
        public void TestGraphSerializationClass1()
        {
            using (FileStream fileStream = new FileStream("Class1Graph.txt", FileMode.Create))
            {
                serializer.Serialize(fileStream, _class1);
            }

            using (FileStream fileStream = new FileStream("Class1Graph.txt", FileMode.Open))
            {
                _class1Deserialized = (Class1) serializer.Deserialize(fileStream);
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
            using (FileStream fileStream = new FileStream("Class2Graph.txt", FileMode.Create))
            {
                serializer.Serialize(fileStream, _class2);
            }

            using (FileStream fileStream = new FileStream("Class2Graph.txt", FileMode.Open))
            {
                _class2Deserialized = (Class2) serializer.Deserialize(fileStream);
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
            using (FileStream fileStream = new FileStream("Class3Graph.txt", FileMode.Create))
            {
                serializer.Serialize(fileStream, _class3);
            }

            using (FileStream fileStream = new FileStream("Class3Graph.txt", FileMode.Open))
            {
                _class3Deserialized = (Class3) serializer.Deserialize(fileStream);
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
        public void TestListSerialization()
        {
            Assert.Inconclusive();
            using (FileStream fileStream = new FileStream("ListData.txt", FileMode.Create))
            {
               serializer.Serialize(fileStream, _documentBinder);
            }

            using (FileStream fileStream = new FileStream("ListData.txt", FileMode.Open))
            {
                _documentBinderDeserialized = (DocumentBinder) serializer.Deserialize(fileStream);
            }

            Assert.IsNotNull(_documentBinderDeserialized);
            Assert.AreNotSame(_documentBinder, _documentBinderDeserialized);

            CollectionAssert.AreEqual(_documentBinder.Documents, _documentBinderDeserialized.Documents);
            CollectionAssert.AreEqual(_documentBinder.Aliases, _documentBinderDeserialized.Aliases);
        }
    }
}