using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyValueStore.Logic.Interfaces
{
    public interface IDataStore
    {
        void SetValue(string key, string value);
        string GetValue(string key);

        void DeleteValue(string key);
        void BeginTransaction();
        void RollBackTransaction();
        void ComitTransaction();
    }
}
