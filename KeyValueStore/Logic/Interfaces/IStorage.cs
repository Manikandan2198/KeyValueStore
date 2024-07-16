namespace KeyValueStore.Logic.Interfaces
{
    public interface IStorage<K,V>
    {
        void Set(K key, V value);
        V Get(K key);

        void Delete(K key);
    }
}