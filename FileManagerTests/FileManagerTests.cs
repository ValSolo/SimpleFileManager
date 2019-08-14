using FileManagerSolution;
using FileManagerSolution.Implementaion;
using Moq;
using NUnit.Framework;

namespace FileManagerTests
{
    public class FileManagerTests
    {
        Mock<Config> _mockConfig;
        Mock<DateTimeProvider> _mockDateTimeProvider;
        IFileManager fileManager;

        [SetUp]
        public void Setup()
        {
            _mockConfig = new Mock<Config>();
            _mockConfig.SetupGet(c => c.StoragePath).Returns(@"./test_storage/");
            _mockConfig.SetupGet(c => c.StorageFile).Returns(@"./test_storage.json");
            _mockConfig.SetupGet(c => c.StorageTimeout).Returns(-1);

            _mockDateTimeProvider = new Mock<DateTimeProvider>();
            _mockDateTimeProvider.SetupGet(d => d.GetDateTime()).Returns(new System.DateTime(2019, 8, 1, 12, 0, 0));
        }

        [Test]
        public void TestWriteFile()
        {

        }

        [Test]
        public void TestReadFile()
        {

        }

        [Test]
        public void TestDeleteFile()
        {

        }

        [Test]
        public void TestDeleteUnusedFiles()
        {

        }
    }
}