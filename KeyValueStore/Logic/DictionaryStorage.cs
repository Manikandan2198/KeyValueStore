using KeyValueStore.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyValueStore.Logic
{
    public class DictionaryStorage<K, V> : IStorage<K, V>
    {
        public IDictionary<K,V> Store { get; set; }

        public DictionaryStorage()
        {
            this.Store = new Dictionary<K, V>();
        }
        public void Delete(K key)
        {
            if (!Store.ContainsKey(key))
                throw new Exception("Key not found");
            Store.Remove(key);
        }

        public V Get(K key)
        {
            if (!Store.ContainsKey(key))
                throw new Exception("Key not found");
            return Store[key];
        }

        public void Set(K key, V value)
        {
            if (Store.ContainsKey(key))
                Store[key] = value;
            Store.Add(key, value);
        }
    }
}
