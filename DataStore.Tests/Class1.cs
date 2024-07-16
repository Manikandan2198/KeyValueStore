using KeyValueStore.Logic;
using KeyValueStore.Logic.Interfaces;
using System;

namespace DataStore.Tests
{
    public class DataStoreTests
    {
        private readonly IDataStore dataStore;
        public void Setup()
        {
            dataStore = new DataStore(new DictionaryStorage<string, string>());
        }
    }
}
