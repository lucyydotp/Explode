using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Explode;
using ExplodePluginBase;


namespace ExplodeUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestYaz0Type()
        {
            FileStream test = new FileStream("testfile",FileMode.Create);
            test.Write(Encoding.UTF8.GetBytes("Yaz0"), 0, 4);

            IFileTypeBase tester = new BuiltinYaz0();
            Assert.AreEqual("Nintendo Yaz0 Archive", tester.CheckFileType(test));
            test.Close();
        }
    }
}
