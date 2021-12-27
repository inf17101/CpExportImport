using CpImportExportLibrary.src.FileReader;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.IO;

namespace TestCpImportExportLibrary
{
    /// <summary>Class <c>TestParserApplicationSite</c> tests the PredefinedObjectsReader if he can read predefined CheckPoint objects from a file </summary>
    class TestPredefinedObjectsReader
    {
        [Test]
        /// <summary>method <c>ReadPredefinedObjects_Test</c> tests if predefined objects can be written from a file and saved into a dictionary. Dictionary contains the name of the predefined objects as key and the api type as value.</summary>
        public void ReadPredefinedObjects_Test()
        {
            Mock<IStreamReader> mockIStreamReader = new Mock<IStreamReader>();
            string predefinedObjectsFile = "1000memories;application-site\nDirect_Connect_TCP;service-tcp\nYoowalk;application-site";
            byte[] bytesOfFile = Encoding.UTF8.GetBytes(predefinedObjectsFile);
            MemoryStream fakeMemoryStream = new MemoryStream(bytesOfFile);
            mockIStreamReader.Setup(fileManager => fileManager.GetReader(It.IsAny<string>()))
               .Returns(() => new StreamReader(fakeMemoryStream));

            var predefinedObjectsReader = new PredefinedObjectsReader(mockIStreamReader.Object, "predefined_objects.csv", delimiter: ';');
            var result = predefinedObjectsReader.ReadPredefinedObjects();

            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.ContainsKey("1000memories"));
            Assert.IsTrue(result.ContainsKey("Direct_Connect_TCP"));
            Assert.IsTrue(result.ContainsKey("Yoowalk"));
            Assert.IsFalse(result.ContainsKey("application-site"));
            Assert.IsFalse(result.ContainsKey("service-tcp"));
            Assert.AreEqual("application-site", result.GetValueOrDefault("1000memories"));
            Assert.AreEqual("service-tcp", result.GetValueOrDefault("Direct_Connect_TCP"));
            Assert.AreEqual("application-site", result.GetValueOrDefault("Yoowalk"));
        }

        [Test]
        /// <summary>method <c>ReadPredefinedObjectsWithEmptyInputFile_Test</c> test if the returned dictionary is empty when input file with predefined objects is also empty.</summary>
        public void ReadPredefinedObjectsWithEmptyInputFile_Test()
        {
            Mock<IStreamReader> mockIStreamReader = new Mock<IStreamReader>();
            string predefinedObjectsFile = "";
            byte[] bytesOfFile = Encoding.UTF8.GetBytes(predefinedObjectsFile);
            MemoryStream fakeMemoryStream = new MemoryStream(bytesOfFile);
            mockIStreamReader.Setup(fileManager => fileManager.GetReader(It.IsAny<string>()))
               .Returns(() => new StreamReader(fakeMemoryStream));

            var predefinedObjectsReader = new PredefinedObjectsReader(mockIStreamReader.Object, "predefined_objects.csv", delimiter: ';');
            var result = predefinedObjectsReader.ReadPredefinedObjects();

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        /// <summary>method <c>ReadPredefinedObjectsWithCorruptedFileContent_Test</c> test if FormatException is thrown if file content is corrupted.</summary>
        public void ReadPredefinedObjectsWithCorruptedFileContent_Test()
        {
            Mock<IStreamReader> mockIStreamReader = new Mock<IStreamReader>();
            string predefinedObjectsFile = "1000memories\nDirect_Connect_TCP\n";
            byte[] bytesOfFile = Encoding.UTF8.GetBytes(predefinedObjectsFile);
            MemoryStream fakeMemoryStream = new MemoryStream(bytesOfFile);
            mockIStreamReader.Setup(fileManager => fileManager.GetReader(It.IsAny<string>()))
               .Returns(() => new StreamReader(fakeMemoryStream));

            var predefinedObjectsReader = new PredefinedObjectsReader(mockIStreamReader.Object, "predefined_objects.csv", delimiter: ';');
            Assert.Throws<FormatException>(() => predefinedObjectsReader.ReadPredefinedObjects());
        }
    }
}
