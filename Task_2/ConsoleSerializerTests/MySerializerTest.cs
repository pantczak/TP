using System;
using System.IO;
using System.Runtime.Serialization;
using ConsoleSerializer.DataModel;
using ConsoleSerializer.Serializer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleSerializerTests
{
    [TestClass]
    public class MySerializerTest
    {
        [TestMethod]
        public void TestWrite()
        {
            Class1 class1 = new Class1("Text", DateTime.Now, 3.012, null, null);
            Class2 class2 = new Class2("Text", DateTime.Now, 17, null, null);
            Class3 class3 = new Class3("Text", DateTime.Now, 192834, null, null);
            class1.Class2 = class2;
            class1.Class3 = class3;
            class2.Class3 = class3;
            class3.Class1 = class1;
            //class2.Class1 = objectToSerialize;
            //class2.Class3 = class3;

            MySerializer serializer = new MySerializer();
            const string filePath = "testsCustom.txt";

            Stream fileStream = new FileStream(filePath, FileMode.Create);
            serializer.Serialize( fileStream, class1);
            fileStream.Close();
            //FileInfo info = new FileInfo(filePath);
            //Assert.IsTrue(info.Exists);
            //Assert.IsTrue(info.Length >= 100);
            //string fileContent = File.ReadAllText(fileName, Encoding.UTF8);
            //Debug.Write(fileContent);
            //File.Delete(fileName);
        }
    }
}
