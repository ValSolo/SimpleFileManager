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
        public string StoragePath { get; private set; }

        public string StorageFile { get; private set; }

        public int StorageTimeout { get; private set; }

        public Config()
        {
            StoragePath = @"./storage/";
            StorageFile = @"./storage.json";
            StorageTimeout = 60;
        }
    }
}
