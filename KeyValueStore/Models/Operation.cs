using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyValueStore.Models
{
    public class Operation
    {
        public string Name { get; set; }
        public List<string> Arguments { get; set; }
    }
}
