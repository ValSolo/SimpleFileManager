using FileManagerSolution;
using FileManagerSolution.Implementaion;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace FileManagerTests
{
    class FileTrackerTests
    {
        Mock<IConfig> _mockConfig;
        Mock<IDateTimeProvider> _mockDateTimeProvider;
        IFileTracker _fileTracker;

        string _testFileName = "testFile";

        [SetUp]
        public void Setup()
        {
            _mockConfig = new Mock<IConfig>();
            _mockConfig.SetupGet(c => c.StoragePath).Returns(@"./test_storage/");
            _mockConfig.SetupGet(c => c.StorageFile).Returns(@"./test_storage.json");
            _mockConfig.SetupGet(c => c.StorageTimeout).Returns(-1);

            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _mockDateTimeProvider.Setup(d => d.GetDateTime()).Returns(new System.DateTime(2019, 8, 1, 12, 0, 0));

            _fileTracker = new FileTracker(_mockConfig.Object, _mockDateTimeProvider.Object);
        }

        [Test]
        public void TestUpdateAccessTime()
        {
            DeleteExistingFile();

            _fileTracker.TrackFileAccess(_testFileName);
            _fileTracker.SaveStoredList();

            var properContents = "[{\"FileName\":\"" + _testFileName + "\",\"LastAccessTime\":" + _mockDateTimeProvider.Object.GetDateTime().Ticks + "}]";

            StreamReader reader = new StreamReader(_mockConfig.Object.StorageFile);
            var fileContents = reader.ReadToEnd();

            Assert.AreEqual(properContents, fileContents);
        }

        [Test]
        public void TestDeleteFile()
        {
            DeleteExistingFile();

            _fileTracker.TrackFileAccess(_testFileName);
            _fileTracker.DeleteFile(_testFileName);
            _fileTracker.SaveStoredList();

            var properContents = "[]";

            StreamReader reader = new StreamReader(_mockConfig.Object.StorageFile);
            var fileContents = reader.ReadToEnd();

            Assert.AreEqual(properContents, fileContents);
        }

        [Test]
        public void TestGetUnusedFiles()
        {
            var testList = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                testList.Add(_testFileName + i);
            }

            foreach (var fileName in testList)
            {
                _fileTracker.TrackFileAccess(fileName);
            }

            var unusedFiles = _fileTracker.GetUnusedFiles();

            Assert.AreEqual(testList, unusedFiles);            
        }

        [Test]
        public void TestSaveFeature()
        {
            DeleteExistingFile();

            _fileTracker.SaveStoredList();

            Assert.IsTrue(File.Exists(_mockConfig.Object.StorageFile));
        }

        private void DeleteExistingFile()
        {
            if (File.Exists(_mockConfig.Object.StorageFile))
            {
                File.Delete(_mockConfig.Object.StorageFile);
            }
        }
    }
}
