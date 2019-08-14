using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerSolution
{
    interface IConfig
    {
        string StoragePath { get; }
        string StorageFile { get; }
        int StorageTimeout { get; }
    }
}
