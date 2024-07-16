using KeyValueStore.Logic.Interfaces;
using KeyValueStore.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace KeyValueStore.Logic
{
    public class DataStore:IDataStore
    {
        public IStorage<string,string> Storage { get; set; }

        public Stack<Queue<Operation>> Transactions { get; set; }

        public DataStore(IStorage<string,string> storage)
        {
            this.Storage = storage;
            this.Transactions = new Stack<Queue<Operation>>();
        }

        public string GetValue(string key)
        {
            if(Transactions.Count > 0)
            {
                Transactions.Peek().Enqueue(new Operation() { Name = "get", Arguments = new List<string>() { key } });
                return string.Empty;
            }
            var value = Storage.Get(key);
            return value;
        }

        public void SetValue(string key, string value)
        {
            if (Transactions.Count > 0)
            {
                Transactions.Peek().Enqueue(new Operation() { Name = "set", Arguments = new List<string>() { key,value } });
                return;
            }
            Storage.Set(key, value);
        }

        public void DeleteValue(string key)
        {
            if (Transactions.Count > 0)
            {
                Transactions.Peek().Enqueue(new Operation() { Name = "delete", Arguments = new List<string>() { key } });
                return;
            }
            Storage.Delete(key);
        }

        public void BeginTransaction()
        {
            this.Transactions.Push(new Queue<Operation>());   
        }

        public void RollBackTransaction()
        {
            this.Transactions.Pop();
        }

        public void ComitTransaction()
        {
            var operationsQueue = Transactions.Pop();

            while(operationsQueue.Count > 0)
            {
                var operation = operationsQueue.Dequeue();
                var operationName = operation.Name;
                switch (operationName)
                {
                    case "set":
                        this.Storage.Set(operation.Arguments[0], operation.Arguments[1]);
                        break;
                    case "get":
                        this.Storage.Get(operation.Arguments[0]);
                        break;
                    case "delete":
                        this.Storage.Delete(operation.Arguments[0]);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
