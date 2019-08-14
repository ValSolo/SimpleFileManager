using System;

namespace FileManagerSolution.Implementaion
{
    class FileManager : IFileManager
    {
        public FileManager(IConfig config)
        {

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
