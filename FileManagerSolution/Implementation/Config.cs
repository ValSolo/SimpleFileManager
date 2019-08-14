using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerSolution.Implementaion
{
    //imitation of real config than we could get, e.g. from server
    class Config : IConfig
    {
        public string StoragePath => throw new NotImplementedException();

        public string StorageFile => throw new NotImplementedException();

        public int StorageTimeout => throw new NotImplementedException();
    }
}
