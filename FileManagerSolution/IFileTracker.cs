using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerSolution
{
    interface IFileTracker
    {
        void TrackFileAccess(string fileName);
        void DeleteFile(string fileName);
        List<string> DeleteUnusedFiles();
    }
}
