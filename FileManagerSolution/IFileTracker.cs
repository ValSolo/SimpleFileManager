using System.Collections.Generic;

namespace FileManagerSolution
{
    public interface IFileTracker
    {
        void TrackFileAccess(string fileName);
        void DeleteFile(string fileName);
        List<string> GetUnusedFiles();
        void SaveStoredList();
    }
}
