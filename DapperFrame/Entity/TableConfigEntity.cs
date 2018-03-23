using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperFrame.Entity
{
    public class TableConfiguration
    {
        public string Name { get; set; }
        public string SqlType { get; set; }
        public bool WithLock { get; set; }
        public string Author { get; set; }
        public string ConnectString { get; set; }
    }
}
