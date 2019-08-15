using FileManagerSolution;
using FileManagerSolution.Implementaion;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileManagerTests
{
    public class FileManagerTests
    {
        Mock<IConfig> _mockConfig;
        Mock<IDateTimeProvider> _mockDateTimeProvider;
        IFileManager _fileManager;

        string _testFileName = "testFile.test";

        [SetUp]
        public void Setup()
        {
            _mockConfig = new Mock<IConfig>();
            _mockConfig.SetupGet(c => c.StoragePath).Returns(@"./test_storage/");
            _mockConfig.SetupGet(c => c.StorageFile).Returns(@"./test_storage.json");
            _mockConfig.SetupGet(c => c.StorageTimeout).Returns(-1);

            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _mockDateTimeProvider.Setup(d => d.GetDateTime()).Returns(new System.DateTime(2019, 8, 1, 12, 0, 0));

            var fileTracker = new FileTracker(_mockConfig.Object, _mockDateTimeProvider.Object);
            _fileManager = new FileManager(_mockConfig.Object, fileTracker);
        }

        [Test]
        public void TestWriteFile()
        {
            var bytesToWrite = CreateRandomByteArray();
            _fileManager.WriteFile(_testFileName, bytesToWrite);

            var fullPath = Path.Combine(_mockConfig.Object.StoragePath, _testFileName);
            Assert.IsTrue(File.Exists(fullPath));
        }

        [Test]
        public void TestReadFile()
        {
            var bytesToWrite = CreateRandomByteArray();
            CreateFile(_testFileName, bytesToWrite);

            byte[] dataFromFile = new byte[0];
            bool result = _fileManager.ReadFile(_testFileName, ref dataFromFile);

            Assert.IsTrue(result);
            Assert.AreEqual(bytesToWrite, dataFromFile);
        }

        [Test]
        public void TestDeleteFile()
        {
            var bytesToWrite = CreateRandomByteArray();
            CreateFile(_testFileName, bytesToWrite);

            _fileManager.DeleteFile(_testFileName);

            var fullPath = Path.Combine(_mockConfig.Object.StoragePath, _testFileName);
            Assert.IsFalse(File.Exists(fullPath));
        }

        [Test]
        public void TestDeleteUnusedFiles()
        {
            var fileNames = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                fileNames.Add(_testFileName + i);
            }

            foreach (var fileName in fileNames)
            {
                var bytesToWrite = CreateRandomByteArray();
                _fileManager.WriteFile(fileName, bytesToWrite);
            }

            _fileManager.DeleteUnusedFiles();

            bool anyFiles = false;
            foreach (var fileName in fileNames)
            {
                var fullPath = Path.Combine(_mockConfig.Object.StoragePath, fileName);
                if (File.Exists(fullPath))
                {
                    anyFiles = true;
                }
            }

            Assert.IsFalse(anyFiles);
        }

        private byte[] CreateRandomByteArray()
        {
            Random rnd = new Random();
            Byte[] byteArray = new Byte[10];
            rnd.NextBytes(byteArray);

            return byteArray;
        }

        private void CreateFile(string fileName, byte[] data)
        {
            var fullPath = Path.Combine(_mockConfig.Object.StoragePath, fileName);
            File.WriteAllBytes(fullPath, data);
        }
    }
}