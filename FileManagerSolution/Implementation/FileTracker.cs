using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerSolution.Implementaion
{
    class FileTracker : IFileTracker
    {
        public FileTracker(IConfig config, IDateTimeProvider dateTimeProvider)
        {

        }

        public List<string> GetUnusedFiles()
        {
            throw new NotImplementedException();
        }

        public void TrackFileAccess(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
