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
        Class1 class1Empty;
        Class1 class1;
        Class2 class2;
        Class3 class3;
        Class1 class1Deserialized;
        Class2 class2Deserialized;
        Class3 class3Deserialized; 
        DocumentBinder documentBinder;
        DocumentBinder documentBinderDeserialized;

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
             
            ObservableCollection<Document> documents = new ObservableCollection<Document>{
            
                new Document(4,"t1"),
                new Document(45, "t2"),
                new Document(324,"t3")
            };

            documentBinder = new DocumentBinder(documents);
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
        public void TestGraphSerializationClass2()
        {
            using (FileStream fileStream = new FileStream("Class2Graph.json", FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, class2);
            }

            using (FileStream fileStream = new FileStream("Class2Graph.json", FileMode.Open))
            {
                class2Deserialized = JsonSerializer.Deserialize<Class2>(fileStream);
            }

            Assert.IsNotNull(class2Deserialized);
            Assert.AreNotSame(class2, class2Deserialized);

            Assert.AreEqual(class2.TextData, class2Deserialized.TextData);
            Assert.AreEqual(class2.DateTimeData, class2Deserialized.DateTimeData);

            Assert.AreEqual(class2.Class1.TextData, class2Deserialized.Class1.TextData);
            Assert.AreEqual(class2.Class1.DateTimeData, class2Deserialized.Class1.DateTimeData);
            Assert.AreEqual(class2.Class1.DoubleData, class2Deserialized.Class1.DoubleData);

            Assert.AreEqual(class2.Class3.TextData, class2Deserialized.Class3.TextData);
            Assert.AreEqual(class2.Class3.DateTimeData, class2Deserialized.Class3.DateTimeData);
        }


        [TestMethod]
        public void TestGraphSerializationClass3()
        {
            using (FileStream fileStream = new FileStream("Class3Graph.json", FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, class3);
            }

            using (FileStream fileStream = new FileStream("Class3Graph.json", FileMode.Open))
            {
                class3Deserialized = JsonSerializer.Deserialize<Class3>(fileStream);
            }

            Assert.IsNotNull(class3Deserialized);
            Assert.AreNotSame(class3, class3Deserialized);

            Assert.AreEqual(class3.TextData, class3Deserialized.TextData);
            Assert.AreEqual(class3.DateTimeData, class3Deserialized.DateTimeData);

            Assert.AreEqual(class3.Class1.TextData, class3Deserialized.Class1.TextData);
            Assert.AreEqual(class3.Class1.DateTimeData, class3Deserialized.Class1.DateTimeData);
            Assert.AreEqual(class3.Class1.DoubleData, class3Deserialized.Class1.DoubleData);

            Assert.AreEqual(class3.Class2.TextData, class3Deserialized.Class2.TextData);
            Assert.AreEqual(class3.Class2.DateTimeData, class3Deserialized.Class2.DateTimeData);
        }

        [TestMethod]
        public void ListDataTest()
        {
            using (FileStream fileStream = new FileStream("ListData.json", FileMode.Create))
            {
                JsonSerializer.Serialize(fileStream, documentBinder);
            }

            using (FileStream fileStream = new FileStream("ListData.json", FileMode.Open))
            {
                documentBinderDeserialized = JsonSerializer.Deserialize<DocumentBinder>(fileStream);
            }

            Assert.IsNotNull(documentBinderDeserialized);
            Assert.AreNotSame(documentBinder, documentBinderDeserialized);

            CollectionAssert.AreEqual(documentBinder.Documents, documentBinderDeserialized.Documents);
        }
    }
}