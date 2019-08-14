using FileManagerSolution;
using FileManagerSolution.Implementaion;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace FileManagerTests
{
    class FileTrackerTests
    {
        Mock<IConfig> _mockConfig;
        Mock<IDateTimeProvider> _mockDateTimeProvider;
        IFileTracker _fileTracker;

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
            _fileTracker.TrackFileAccess("testFile");
            _fileTracker.SaveStoredList();
            // Check for file contents?
        }

        [Test]
        public void TestDeleteFile()
        {
            _fileTracker.TrackFileAccess("testFile");
            _fileTracker.DeleteFile("testFile");
            _fileTracker.SaveStoredList();
            // Check for file contents?
        }

        [Test]
        public void TestGetUnusedFiles()
        {
            var testList = new List<string>();
            testList.Add("testFile");

            _fileTracker.TrackFileAccess("testFile");
            var unusedFiles = _fileTracker.GetUnusedFiles();

            Assert.AreEqual(testList, unusedFiles);            
        }
    }
}
