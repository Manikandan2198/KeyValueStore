using KeyValueStore.Logic;
using KeyValueStore.Logic.Interfaces;
using NUnit.Framework;
using System;

namespace KeyValueStore.Tests
{
    public class Tests
    {
        private IDataStore dataStore;
        [SetUp]
        public void Setup()
        {
            this.dataStore = new DataStore(new DictionaryStorage<string, string>());
        }

        [Test]
        public void SetValueGetValue_Test()
        {
            this.dataStore.SetValue("mani", "keyvaluestore");
            var value = this.dataStore.GetValue("mani");
            Assert.AreEqual(value,"keyvaluestore");
        }

        [Test]
        public void DeleteValue_Test()
        {
            this.dataStore.SetValue("key7", "value7");
            this.dataStore.DeleteValue("key7");
            Assert.That(()=>this.dataStore.GetValue("key7"), Throws.TypeOf<Exception>());
        }

        [Test]
        public void Transactions_Test()
        {
            this.dataStore.BeginTransaction();
            this.dataStore.SetValue("key1", "value1");
            this.dataStore.SetValue("key2", "value2");
            this.dataStore.SetValue("key3", "value3");
            this.dataStore.BeginTransaction();
            this.dataStore.SetValue("key4", "value4");
            this.dataStore.SetValue("key5", "value5");
            this.dataStore.RollBackTransaction();
            this.dataStore.DeleteValue("key3");
            this.dataStore.ComitTransaction();
            Assert.AreEqual(this.dataStore.GetValue("key1"), "value1");
        }
    }
}