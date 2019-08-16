using System.IO;

namespace FileManagerSolution.Implementaion
{
    public class FileManager : IFileManager
    {
        string _storagePath;
        IFileTracker _fileTracker;

        public FileManager(IConfig config, IFileTracker fileTracker)
        {
            _storagePath = config.StoragePath;
            _fileTracker = fileTracker;

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public void WriteFile(string fileName, byte[] data)
        {
            File.WriteAllBytes(GetFullPath(fileName), data);
            _fileTracker.TrackFileAccess(fileName);
        }

        public bool ReadFile(string fileName, ref byte[] data)
        {
            string fullPath = GetFullPath(fileName);
            if (File.Exists(fullPath))
            {
                data = File.ReadAllBytes(fullPath);
                _fileTracker.TrackFileAccess(fileName);
                return true;
            }
            else
            {
                return false;
            }            
        }

        public void DeleteFile(string fileName)
        {
            File.Delete(GetFullPath(fileName));
            _fileTracker.DeleteFile(fileName);
        }

        public void DeleteUnusedFiles()
        {
            var unusedFileNames = _fileTracker.GetUnusedFiles();
            foreach (var fileName in unusedFileNames)
            {
                DeleteFile(fileName);
            }
        }

        private string GetFullPath(string fileName)
        {
            return Path.Combine(_storagePath, fileName);
        }
    }
}
