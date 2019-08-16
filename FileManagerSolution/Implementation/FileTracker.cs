using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManagerSolution.Implementaion
{
    class StoredFile
    {
        public string FileName { get; private set; }
        public long LastAccessTime { get; private set; }

        public StoredFile(string fileName, long lastAccessTime)
        {
            FileName = fileName;
            LastAccessTime = lastAccessTime;
        }

        public void UpdateAccessTime(long newAccessTime)
        {
            LastAccessTime = newAccessTime;
        }
    }

    public class FileTracker : IFileTracker
    {
        private int _storageTimeout;
        private string _storageFile;
        private IDateTimeProvider _dateTimeProvider;

        private List<StoredFile> _storedFiles;

        public FileTracker(IConfig config, IDateTimeProvider dateTimeProvider)
        {
            _storageTimeout = config.StorageTimeout;
            _storageFile = config.StorageFile;
            _dateTimeProvider = dateTimeProvider;

            _storedFiles = new List<StoredFile>();

            CheckAndLoadStorageFile();
        }

        public void TrackFileAccess(string fileName)
        {
            long accessTime = _dateTimeProvider.GetDateTime().Ticks;

            if (_storedFiles.Any(f => f.FileName == fileName))
            {
                _storedFiles.First(f => f.FileName == fileName).UpdateAccessTime(accessTime);
            }
            else
            {
                _storedFiles.Add(new StoredFile(fileName, accessTime));
            }
        }

        public void DeleteFile(string fileName)
        {
            if (_storedFiles.Any(f => f.FileName == fileName))
            {
                _storedFiles.RemoveAll(f => f.FileName == fileName);
            }
        }

        public List<string> GetUnusedFiles()
        {
            var thresholdTime = _dateTimeProvider.GetDateTime().AddSeconds(-1 * _storageTimeout).Ticks;
            var neglectedFiles = _storedFiles.FindAll(f => f.LastAccessTime < thresholdTime);

            var neglectedFileNames = new List<string>();

            foreach (var file in neglectedFiles)
            {
                neglectedFileNames.Add(file.FileName);
            }

            return neglectedFileNames;
        }

        private void LoadStoredList()
        {
            using (StreamReader reader = new StreamReader(_storageFile))
            {
                string json = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(json))
                {
                    _storedFiles = JsonConvert.DeserializeObject<List<StoredFile>>(json);                    
                }
                reader.Close();
            }
            
        }

        public void SaveStoredList()
        {
            string serializedFilesList = JsonConvert.SerializeObject(_storedFiles);
            using (StreamWriter writer = new StreamWriter(_storageFile))
            {
                writer.Write(serializedFilesList);
                writer.Close();
            }            
        }

        private void CheckAndLoadStorageFile()
        {
            if (!File.Exists(_storageFile))
            {
                File.Create(_storageFile);
            }
            else
            {
                LoadStoredList();
            }
        }
    }
}
