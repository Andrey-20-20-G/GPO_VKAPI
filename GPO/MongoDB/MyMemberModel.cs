using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPO.MongoDB
{
    public class MyMemberModel
    {
        public long IdGroup { get; set; }
        public string FrstName { get; set; }
        public string LstName { get; set; }

        public long UserId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
