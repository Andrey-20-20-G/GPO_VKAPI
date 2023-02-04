using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPO.MongoDB
{
    internal class MyMongoItems
    {
        public long Id { get; set; }
        public int CountofMembers { get; set; }
        public string GroupName { get; set; }

        public DateTime DateTime { get; set; }
    }
}
