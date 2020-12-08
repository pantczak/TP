using ConsoleSerializer.DataModel;
using ConsoleSerializer.Serializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ConsoleSerializerTests
{
    [TestClass]
    public class MySerializerTest
    {
        [TestMethod]
        public void TestWrite()
        {
            Class1 class1 = new Class1("Text", DateTime.Now, 3.012, null, null);
            Class2 class2 = new Class2("Text", DateTime.Now, null, null);
            Class3 class3 = new Class3("Text", DateTime.Now, null, null);
            class1.Class2 = class2;
            class1.Class3 = class3;
            class2.Class3 = class3;
            class3.Class1 = class1;


            MySerializer serializer = new MySerializer();
            const string filePath = "testsCustom.txt";

            Stream fileStream = new FileStream(filePath, FileMode.Create);
            serializer.Serialize( fileStream, class1);
            fileStream.Close();
        }
        [TestMethod]
        public void TestRead()
        {
            Class1 class1 = new Class1("Text", DateTime.Now, 3.012, null, null);
            Class2 class2 = new Class2("Text", DateTime.Now, null, null);
            Class3 class3 = new Class3("Text", DateTime.Now, null, null);
            class1.Class2 = class2;
            class1.Class3 = class3;
            class2.Class3 = class3;
            class3.Class1 = class1;


            MySerializer serializer = new MySerializer();
            const string filePath = "testsCustom.txt";

            Stream fileStream = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(fileStream, class1);
            fileStream.Close();

            fileStream = new FileStream(filePath, FileMode.Open);
            Class1 obj = (Class1)serializer.Deserialize(fileStream);
            fileStream.Close();
            Assert.AreEqual(class1, obj);
            Assert.AreEqual(true, Equals(class1, obj));
        }
    }
}
