using System;

namespace SimpleFileManager
{
    class FileManager: IFileManager
    {
        private const string storagePath = @"Storage/";

        private IDateTimeProvider _dateTimeProvider;

        public FileManager(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
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
    }
}
