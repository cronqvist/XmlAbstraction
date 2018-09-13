﻿namespace XmlAbstraction.Test
{
    using System;
    using System.IO;
    using System.Text;
    using Xunit;

    public class XmlObjectUnitTest
    {
        private static void NoThrows(Action expression)
        {
            try
            {
                expression();
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Expression threw an exception.");
            }
        }

        [Fact]
        public void TestClassReopenFile()
        {
            var testXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<test>
</test>";
            var xmlObj = new XmlObject(testXml);
            Assert.ThrowsAny<Exception>(() => xmlObj.ReopenFile());
            var fstrm = File.Create(
                $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}test.xml");
            fstrm.Write(Encoding.UTF8.GetBytes(testXml), 0, testXml.Length);
            fstrm.Dispose();
            xmlObj.Dispose();
            xmlObj = new XmlObject(
                $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}test.xml",
                testXml);
            NoThrows(() => xmlObj.ReopenFile());
            xmlObj.Dispose();
            File.Delete(
                $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}test.xml");
        }

        [Fact]
        public void TestClassDoubleDispose()
        {
            var testXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<test>
</test>";
            var xmlObj = new XmlObject(testXml);
            xmlObj.Dispose();
            xmlObj.Dispose();
        }
    }
}
