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
        List<string> GetUnusedFiles();
    }
}
