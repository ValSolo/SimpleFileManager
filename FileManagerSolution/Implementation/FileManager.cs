using System;

namespace FileManagerSolution.Implementaion
{
    class FileManager : IFileManager
    {
        string _storagePath;
        IFileTracker _fileTracker;

        public FileManager(IConfig config, IFileTracker fileTracker)
        {
            _storagePath = config.StoragePath;
            _fileTracker = fileTracker;
        }

        public void WriteFile(string fileName, byte[] data)
        {
            throw new NotImplementedException();
        }

        public bool ReadFile(string fileName, ref byte[] data)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public void DeleteUnusedFiles()
        {
            throw new NotImplementedException();
        }
    }
}
